using System;
using System.ComponentModel.DataAnnotations;

namespace DrukClik.Data
{
   public class SendEmailError
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public FormEntity FormEntity { get; set; }    
        public DateTime DateTime { get; set; }
    }
}
