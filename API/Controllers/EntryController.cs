using API.Dtos;
using API.Erros;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class EntryController : BaseApiController
    {

        private readonly IGenericRepository<WorkDay> _workDayRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EntryController(
                                IUnitOfWork unitOfWork,
                                IGenericRepository<WorkDay> workDayRepo
                               )
        {
            _unitOfWork = unitOfWork;
            _workDayRepo=workDayRepo;
            
        }

        [HttpPost]
        public async Task<ActionResult<WorkDay>> CreateProduct(RegisterEntryDto createEntry)
        {
            
            var entry = new WorkDay();
             _unitOfWork.Repository<WorkDay>().Add(entry);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem on register the entry"));

            return Ok(result);
        }

    }
}