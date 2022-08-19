using API.Dtos;
using API.Erros;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class WorkDayController : BaseApiController
    {

        private readonly IGenericRepository<WorkDay> _workDayRepo;
        private readonly IGenericRepository<Employee> _employeeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public WorkDayController(
                                IUnitOfWork unitOfWork,
                                IGenericRepository<WorkDay> workDayRepo,
                                IGenericRepository<Employee> employeeRepo
                               )
        {
            _unitOfWork = unitOfWork;
            _workDayRepo=workDayRepo;
            _employeeRepo=employeeRepo;
            
        }

        [HttpPost]
        public async Task<ActionResult<WorkDay>> AddEntry(RegisterEntryDto createEntry)
        {
            var parms = new EmployeeSpecParams();
            parms.Rfid=createEntry.Rfid;
            var specEmplo = new EmployeesWithWorkScheduleSpecification(parms);
            var employee = await _employeeRepo.GetEntityWithSpec(specEmplo);
            if(employee is null)
                return BadRequest();
            
            var specDay = new DateWorkDaySpecification(date: createEntry.TimeStamp.Date,employeeId: employee.Id);
            var workDay = await _workDayRepo.GetEntityWithSpec(specDay);

            if(workDay is null)
            {
                workDay=new WorkDay();
                workDay.EmployeeId=employee.Id;
                workDay.Date=createEntry.TimeStamp.Date;
                workDay.CheckIn=createEntry.TimeStamp;
                _unitOfWork.Repository<WorkDay>().Add(workDay);
            }
            else if(!createEntry.IsIn)
            {
                if(!workDay.LunchTimeOut.HasValue)
                {
                    workDay.LunchTimeOut=createEntry.TimeStamp;
                    _unitOfWork.Repository<WorkDay>().Update(workDay);    
                }
                else
                {
                    workDay.CheckOut=createEntry.TimeStamp;
                    _unitOfWork.Repository<WorkDay>().Update(workDay);    
                }
            }
            else
            {
                workDay.LunchTimeIn=createEntry.TimeStamp;
                _unitOfWork.Repository<WorkDay>().Update(workDay);
                
            }
            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem creating entry"));

            return Ok(workDay);
        }

    }
}