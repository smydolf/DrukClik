using System;
using System.ComponentModel.DataAnnotations;

namespace DrukClik.Data
{
    public class SendCodeLog
    {
        [Key]
        public int Id { get; set; }
        public DateTime SendCodeDateTime { get; set; }
        public string Email { get; set; }
        public PizzaPortalCode PizzaPortalCode { get; set; }
        public FormEntity FormEntity { get; set; }
        public bool IsDouble { get; set; }
    }
}
