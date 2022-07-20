//TODO: FaltaImplementar Pagination. Pode-se alterar o switchCase de Search. Não tá otimizado ainda
using Core.Entities;

namespace Core.Specifications
{
    public class EmployeesWithWorkScheduleSpecification : BaseSpecification<Employee>
    {
        public EmployeesWithWorkScheduleSpecification(EmployeeSpecParams employeeParams)
        {
            
            AddInclude(x => x.WorkDaySchedule);
            AddOrderBy(x => x.Name);
            
            //ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(employeeParams.Search))
            {
                switch (employeeParams.TypeOfSearch)
                {
                    case "jobTitle":
                        AddCriteria(p => p.JobTitle.ToLower().Contains(employeeParams.Search)&& 
                                    (!employeeParams.WorkDayScheduleId.HasValue || p.WorkDayScheduleId == employeeParams.WorkDayScheduleId));
                        break;
                    case "departament":
                        AddCriteria(p => p.Department.ToLower().Contains(employeeParams.Search)&& 
                                    (!employeeParams.WorkDayScheduleId.HasValue || p.WorkDayScheduleId == employeeParams.WorkDayScheduleId));
                        break;
                    default:
                        AddCriteria(p => p.Name.ToLower().Contains(employeeParams.Search)&& 
                                    (!employeeParams.WorkDayScheduleId.HasValue || p.WorkDayScheduleId == employeeParams.WorkDayScheduleId));
                        break;
                }
            }
            if (!string.IsNullOrEmpty(employeeParams.Sort))
            {
                switch (employeeParams.Sort)
                {
                    case "startedInAsc":
                        AddOrderBy(p => p.StartedIn);
                        break;
                    case "startedInDesc":
                        AddOrderByDescending(p => p.StartedIn);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public EmployeesWithWorkScheduleSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.WorkDaySchedule);
        }
    }
}