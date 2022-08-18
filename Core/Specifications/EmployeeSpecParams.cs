//Classe contendo os parametros da especificacao Employee
// TODO implementar uma spec apenas para o
namespace Core.Specifications
{
    public class EmployeeSpecParams
    {
        //TODO: falta implementar pagination
        // private const int MaxPageSize = 50;
        // public int PageIndex { get; set; } = 1;
        // private int _pageSize = 6;
        // public int PageSize
        // {
        //     get => _pageSize;
        //     set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        // }
        public EmployeeSpecParams()
        {
        }
        public EmployeeSpecParams(string rfid)
        {
            Rfid = rfid;
        }
        public int? WorkDayScheduleId { get; set; }

        public string? Rfid { get; set; }

        public string? Sort { get; set; }
        
        private string? _search;
        public string? Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
        // public string? TypeOfSearch { get; set; }
    }
}