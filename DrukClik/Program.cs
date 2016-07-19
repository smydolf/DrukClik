using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using DrukClik.Credential;
using DrukClik.Data;

namespace DrukClik
{
    public class Program
    {
        private static string _copiedFilePath;
        private static string _orginalDestination;
        private static Dictionary<string, List<string>> ExcelValue { get; set; } 
        public static string CopiedFilePath
        {
            get
            {
                return
                    _copiedFilePath =
                        "C:\\Users\\smydolf\\Desktop\\SMYDAEXEL.xlsx";
            }
            set { _copiedFilePath = value; }
        }
        public static string OrginalDestination
        {
            get
            {
                return
                     _orginalDestination =
                    "Y:\\Projekty C#\\Projekty\\DrukClickBot\\DrukClickBot\\DrukClik\\DrukClik\\bin\\Debug\\SMYDAEXEL.xlsx";
            }
            set { _orginalDestination = value; }
        }

        public static Timer Timer;


        static void Main(string[] args)
        {

            lock (new object())
            {
                DateTime start = DateTime.Now;
                GoogleDriveAccess.GetAccess();
                DateTime stop = DateTime.Now;
                Console.Write("Downloading time: {0} s", (stop - start).TotalSeconds);
                ExcelComponent excelComponent = new ExcelComponent();
                bool result = excelComponent.OpenConnection(CopiedFilePath);
                if (result) ExcelValue = excelComponent.GetValueFromExcel();
                excelComponent = null;
                ExcelValue = Mapper.Mapp(ExcelValue);
                List<FormEntity> formEntities = new List<FormEntity>();
                int s = ExcelValue.Values.ToArray()[0].Count;
                for (int i = 0; i < ExcelValue.Values.ToArray()[0].Count; i++)
                    formEntities.Add(ExcelValue.ToObject<FormEntity>(i));
                EmailService emailService = new EmailService();
                TimeSpan ts = stop - start;
                Comparator comparator = new Comparator(formEntities, ts, stop);
            }
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Timer.Stop();

            lock (new object())
            {
                DateTime start = DateTime.Now;
                GoogleDriveAccess.GetAccess();
                DateTime stop = DateTime.Now;
                Console.Write("Downloading time: {0} s", (stop - start).TotalSeconds);
                ExcelComponent excelComponent = new ExcelComponent();
                bool result = excelComponent.OpenConnection(CopiedFilePath);
                if (result) ExcelValue = excelComponent.GetValueFromExcel();
                excelComponent = null;
                ExcelValue = Mapper.Mapp(ExcelValue);
                List<FormEntity> formEntities = new List<FormEntity>();
                int s = ExcelValue.Values.ToArray()[0].Count;
                for (int i = 0; i < ExcelValue.Values.ToArray()[0].Count; i++)
                    formEntities.Add(ExcelValue.ToObject<FormEntity>(i));
                EmailService emailService = new EmailService();
                TimeSpan ts = stop - start;
                Comparator comparator = new Comparator(formEntities, ts, stop);
            }
            Timer.Start();
        }

       
           
        
    }
}


