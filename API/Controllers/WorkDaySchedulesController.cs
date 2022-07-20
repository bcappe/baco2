using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private readonly IGenericRepository<WorkDaySchedule> _workDayScheduleRepo;


        public WorkDaySchedulesController(
                                IGenericRepository<WorkDaySchedule> workDayScheduleRepo
                               )
        {

            _workDayScheduleRepo=workDayScheduleRepo;

        }

        [HttpGet("workDaySchedules")]
        public async Task<ActionResult<IReadOnlyList<WorkDaySchedule>>> GetWorkDaySchedules()
        {
            return Ok(await _workDayScheduleRepo.ListAllAsync());
        }
    }
}