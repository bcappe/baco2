using API.Dtos;
using API.Erros;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class WorkDayController : BaseApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public WorkDayController(
                                IUnitOfWork unitOfWork,
                                IMapper mapper
                               )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<WorkDay>> AddEntry(RegisterEntryDto createEntry)
        {
            var parmsEmployee = new EmployeeSpecParams();
            parmsEmployee.Rfid=createEntry.Rfid;
            var specEmplo = new EmployeesWithWorkScheduleSpecification(parmsEmployee);
            var employee = await _unitOfWork.Repository<Employee>().GetEntityWithSpec(specEmplo);
            if(employee is null)
                return BadRequest();
            var parmsWorkDay= new WorkDaySpecParams();
            parmsWorkDay.Date = createEntry.TimeStamp.Date;
            parmsWorkDay.EmployeeID = employee.Id;

            var specDay = new WorkDaySpecification(parmsWorkDay);
            var workDay  = await _unitOfWork.Repository<WorkDay>().GetEntityWithSpec(specDay);

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

        [HttpGet]
        public async Task<ActionResult<WorkDay>> GetWorkDays(
           [FromQuery] WorkDaySpecParams workDayParams)
        {
            var spec = new WorkDaySpecification(workDayParams);
            var workDays = await _unitOfWork.Repository<WorkDay>().ListAsync(spec);
            return Ok(workDays);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WorkDay>> GetWorkDay(int id)
        {
            var workDay = await _unitOfWork.Repository<WorkDay>().GetByIdAsync(id);
            if (workDay == null) return NotFound(new ApiResponse(404));
            return (workDay);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorkDay(int id)
        {
            var workDay = await _unitOfWork.Repository<WorkDay>().GetByIdAsync(id);
            
            _unitOfWork.Repository<WorkDay>().Delete(workDay);

            var result = await _unitOfWork.Complete();
            
            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem deleting workDay"));

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<WorkDay>> UpdateProduct(int id, WorkDayDto workDayDto)
        {
            var workDay = await _unitOfWork.Repository<WorkDay>().GetByIdAsync(id);     
            _mapper.Map(workDayDto, workDay);  
                        
            _unitOfWork.Repository<WorkDay>().Update(workDay);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem updating workDay"));

            return Ok(workDay);
        }
    }
}