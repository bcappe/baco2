using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<WorkDayScheduleDto,WorkDaySchedule>().ForMember(x => x.Id, x => x.Ignore());
            CreateMap<WorkDayDto,WorkDay>().ForMember(x => x.Id, x => x.Ignore());
            CreateMap<EmployeeDto,Employee>().ForMember(x => x.Id, x => x.Ignore());
        }
    }
}