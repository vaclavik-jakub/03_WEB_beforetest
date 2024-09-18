using System.ComponentModel.DataAnnotations;

namespace _03_WEB_beforetest.Modules
{
    public class PCComponents
    {
        [Key] 
        public string? ComponentsName {  get; set; }
        public string? ComponentUsage { get; set; }
        public string? Description { get; set; }
        public Distributors? Distributors { get; set; }
    }
}
