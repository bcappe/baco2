//TODO: FaltaImplementar Pagination. Pode-se alterar o switchCase de Search. Não tá otimizado ainda
//TODO Isso aqui ta uma merda, necessario refatorar tudo, quebra galho para a API do Alex
using Core.Entities;

namespace Core.Specifications
{
    public class EmployeesWithWorkScheduleSpecification : BaseSpecification<Employee>
    {
        public EmployeesWithWorkScheduleSpecification(EmployeeSpecParams employeeParams)
        : base(x => 
            (!employeeParams.WorkDayScheduleId.HasValue || x.WorkDayScheduleId == employeeParams.WorkDayScheduleId)&&
            (String.IsNullOrEmpty(employeeParams.Rfid) || x.RfidCode == employeeParams.Rfid))
        {
            
            AddInclude(x => x.WorkDaySchedule);
            AddOrderBy(x => x.Name);
            

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