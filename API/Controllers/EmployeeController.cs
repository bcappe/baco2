using API.Erros;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class EmployeeController : BaseApiController
    {

        private readonly IGenericRepository<Employee> _employeeRepo;


        public EmployeeController(
                                IGenericRepository<Employee> employeeRepo
                               )
        {

            _employeeRepo=employeeRepo;

        }

         [HttpGet]
        public async Task<ActionResult<Employee>> GetEmployees(
           [FromQuery] EmployeeSpecParams employeeParams)
        //from query diz para procurar esses parametros na query
        {
            //TODO: dá pra pensar em utilizar automapper
            var spec = new EmployeesWithWorkScheduleSpecification(employeeParams);
            var employee = await _employeeRepo.ListAsync(spec);
            
            return Ok(employee);
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetProduct(int id)
        {
            var spec = new EmployeesWithWorkScheduleSpecification(id);

            var product = await _employeeRepo.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new ApiResponse(404));
            return product;
        }

    }
}