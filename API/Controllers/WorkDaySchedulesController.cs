using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Erros;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class WorkDaySchedulesController : BaseApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkDaySchedulesController(
                                IUnitOfWork unitOfWork,
                                IMapper mapper
                               )
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("workDaySchedules")]
        public async Task<ActionResult<IReadOnlyList<WorkDaySchedule>>> GetWorkDaySchedules()
        {
            
            return Ok(await _unitOfWork.Repository<WorkDaySchedule>().ListAllAsync());
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WorkDaySchedule>> GetWorkDaySchedule(int id)
        {
            var workDaySchedule = await _unitOfWork.Repository<WorkDaySchedule>().GetByIdAsync(id);
            if (workDaySchedule == null) return NotFound(new ApiResponse(404));
            return (workDaySchedule);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorkDaySchedule(int id)
        {
            var workDaySchedule = await _unitOfWork.Repository<WorkDaySchedule>().GetByIdAsync(id);
            
            _unitOfWork.Repository<WorkDaySchedule>().Delete(workDaySchedule);

            var result = await _unitOfWork.Complete();
            
            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem deleting workDay"));

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<WorkDaySchedule>> UpdateWorkSchedule(int id, WorkDayScheduleDto workDaySchedule)
        {
            var workDayScheduleUpdate = await _unitOfWork.Repository<WorkDaySchedule>().GetByIdAsync(id);
            if (workDayScheduleUpdate == null) return NotFound(new ApiResponse(404));
            _mapper.Map(workDaySchedule, workDayScheduleUpdate);     
            _unitOfWork.Repository<WorkDaySchedule>().Update(workDayScheduleUpdate);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem updating workDay"));

            return Ok(workDayScheduleUpdate);
        }
         [HttpPost]
        public async Task<ActionResult<WorkDaySchedule>> AddWorkSchedule(WorkDayScheduleDto createWorkDaySchedule)
        {
            

            var workDaySchedule = _mapper.Map<WorkDayScheduleDto,WorkDaySchedule>(createWorkDaySchedule);
            _unitOfWork.Repository<WorkDaySchedule>().Add(workDaySchedule);
            
            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem creating entry"));

            return Ok(createWorkDaySchedule);
        }
    }
}