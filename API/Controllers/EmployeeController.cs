using API.Dtos;
using API.Erros;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class EmployeeController : BaseApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public EmployeeController(
                                IUnitOfWork unitOfWork,
                                IMapper mapper
                               )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<Employee>>> GetEmployees(
          [FromQuery] EmployeeSpecParams employeeParams)
        {
            var spec = new EmployeesWithWorkScheduleSpecification(employeeParams);
            var employees = await _unitOfWork.Repository<Employee>().ListAsync(spec);
            var totalEmployees = await _unitOfWork.Repository<Employee>().CountAsync(spec);
            return Ok(new Pagination<Employee>(employeeParams.PageIndex,
                                                       employeeParams.PageSize,
                                                       totalEmployees,
                                                       employees));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var spec = new EmployeesWithWorkScheduleSpecification(id);
            var employee = await _unitOfWork.Repository<Employee>().GetEntityWithSpec(spec);
            if (employee == null) return NotFound(new ApiResponse(404));
            return employee;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);

            _unitOfWork.Repository<Employee>().Delete(employee);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem deleting workDay"));

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);
            _mapper.Map(employeeDto, employee);

            _unitOfWork.Repository<Employee>().Update(employee);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem updating workDay"));

            return Ok(employee);
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> AddWorkSchedule(EmployeeDto createEmployee)
        {
            var employee = _mapper.Map<EmployeeDto, Employee>(createEmployee);
            _unitOfWork.Repository<Employee>().Add(employee);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem creating entry"));

            return Ok(employee);
        }

    }
}