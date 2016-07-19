using System;
using System.ComponentModel.DataAnnotations;

namespace DrukClik.Data
{
    public class FormEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Q1FilledDateTime { get; set; }
        public string Q2AgeRange { get; set; }
        public string Q3Sex { get; set; }
        public string Q4City { get; set; }     
        public string Q5Profession { get; set; }
        public string Q6FrequencyOfUse { get; set; }
        public string Q7PrintedPage { get; set; }
        public string Q8DataSafety { get; set; }
        public string Q9PayAttentionFor { get; set; }
        public string Q10KnowOrNotAnnonymusPrintingHouse { get; set; }
        public string Q11SpentTime { get; set; }
        public string Q12KeepingDateInCloud { get; set; }
        public string Q13PercentKeepingDateInCloud { get; set; }
        public string Q14SendDataViaEmail { get; set; }
        public string Q15PercentSendDataViaEmail { get; set; }
        public string Q16SatisfactionPercentSendDataViaEmail { get; set; }
        public string Q17SpentMoneyForPrinting { get; set; }
        public string Q18Email { get; set; }

    }
}
