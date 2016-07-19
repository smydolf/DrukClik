using System.ComponentModel.DataAnnotations;

namespace DrukClik.Data
{
    public class GmailSmtpAuth
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }

    }
}
