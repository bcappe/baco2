//TODO: FaltaImplementar Pagination. Pode-se alterar o switchCase de Search. Não tá otimizado ainda
//TODO Isso aqui ta uma merda, necessario refatorar tudo, quebra galho para a API do Alex
using Core.Entities;

namespace Core.Specifications
{
    public class DateWorkDaySpecification : BaseSpecification<WorkDay>
    {
        public DateWorkDaySpecification(DateTime ?date=null, int? employeeId=null)
        : base(x => 
            (!date.HasValue || x.Date == date.Value.Date) &&
            (!employeeId.HasValue || x.EmployeeId == employeeId)) 
        {

            
        }

    }
}