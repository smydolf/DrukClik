using System.ComponentModel.DataAnnotations;

namespace DrukClik.Data
{
    public class PizzaPortalCode
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public bool active { get; set; }
    }
}
