//TODO: FaltaImplementar Pagination. Pode-se alterar o switchCase de Search. Não tá otimizado ainda
//TODO Isso aqui ta uma merda, necessario refatorar tudo, quebra galho para a API do Alex
using Core.Entities;

namespace Core.Specifications
{
    public class WorkDaySpecification : BaseSpecification<WorkDay>
    {
        public WorkDaySpecification(WorkDaySpecParams parms)
        : base(x => 
            (!parms.Date.HasValue || x.Date == parms.Date) &&
            (!parms.EmployeeID.HasValue || x.EmployeeId == parms.EmployeeID)) 
        {

            
        }

    }
}