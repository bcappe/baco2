namespace Core.Entities
{
    public class Company : BaseEntity
    {
        public string? Name { get; set; }
        List<Employee>? Employees { get; set; }

    }
}