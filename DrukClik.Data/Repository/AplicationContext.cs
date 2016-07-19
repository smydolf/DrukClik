using System.Data.Entity;

namespace DrukClik.Data.Repository
{
    public  class AplicationContext : DbContext
    {
        public DbSet<FormEntity> FormEntity { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<PizzaPortalCode> PizzaPortalCode { get; set; }
        public DbSet<SendCodeLog> SendCodeLog { get; set; }
        public DbSet<GmailSmtpAuth> GmailSmtpAuth { get; set; }
        public DbSet<SendEmailError> SendEmailError { get; set; }

    }
}
