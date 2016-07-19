using System;
using System.Collections.Generic;
using System.Linq;
using DrukClik.Data;

namespace DrukClik
{
    public class Comparator
    {
        private TimeSpan DownloadingTimeSpan { get; set; }
        private DateTime DownloadingTime { get; set; }
        private List<FormEntity> FormEntitiesList { get; set; }
        private List<FormEntity> formEntitiesList { get; set; }
        private int NewForms { get; set; }
        private int NewFormsWithEmail { get; set; }
        public Comparator(List<FormEntity> formEntitiesList, TimeSpan downloadingTimeSpan, DateTime downloadingTime)
        {
            FormEntitiesList = formEntitiesList;
            DownloadingTimeSpan = downloadingTimeSpan;
            DownloadingTime = downloadingTime;
            Compare();
        }
        public int Compare()
        {
            try
            {
                formEntitiesList = RepositoryServices<FormEntity>.Instance.GetList().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot take formEntities from base, bot stop! error message {0}", ex);
                return 401;
            }

            if (formEntitiesList.Count == FormEntitiesList.Count) return 0;
            else
            {
                NewForms = FormEntitiesList.Count - formEntitiesList.Count;

                for (int i = formEntitiesList.Count; i < FormEntitiesList.Count; i++)
                {
                    EmailService emailService = new EmailService();
                    try
                    {
                        RepositoryServices<FormEntity>.Instance.AddEntity(FormEntitiesList[i]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Cannot save new form to base, bot stop! error message {0}", ex);
                        return 101;
                    }
                    if (!String.IsNullOrWhiteSpace(FormEntitiesList[i].Q18Email))
                    {
                        NewFormsWithEmail++;
                        if (RepositoryServices<SendCodeLog>.Instance.GetList().FirstOrDefault
                            (x => x.Email == FormEntitiesList[i].Q18Email) == null)
                        {
                            try
                            {
                                PizzaPortalCode pizzaPortalCode =
                           RepositoryServices<PizzaPortalCode>.Instance.GetList().First(x => x.active);
                                if (emailService.SendEmail(FormEntitiesList[i], true, pizzaPortalCode.Code))
                                {
                                    SendCodeLog sendCodeLog = new SendCodeLog()
                                    {
                                        Email = FormEntitiesList[i].Q18Email,
                                        FormEntity = FormEntitiesList[i],
                                        IsDouble = false,
                                        PizzaPortalCode = pizzaPortalCode,
                                        SendCodeDateTime = DateTime.Now,
                                    };
                                    RepositoryServices<SendCodeLog>.Instance.AddEntity(sendCodeLog);
                                }
                                else
                                {
                                    try
                                    {
                                        SendEmailError sendEmailError = new SendEmailError()
                                        {
                                            DateTime = DateTime.Now,
                                            Email = FormEntitiesList[i].Q18Email,
                                            FormEntity = FormEntitiesList[i],
                                        };
                                        RepositoryServices<SendEmailError>.Instance.AddEntity(sendEmailError);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Save SendEmailError Error: {0}", ex);
                                        return 102;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine("Error code: {0}", ex);
                                return 201;
                            }
                        }
                        else
                        {
                            try
                            {
                                if (emailService.SendEmail(FormEntitiesList[i], false, null))
                                {
                                    SendCodeLog sendCodeLog = new SendCodeLog()
                                    {
                                        Email = FormEntitiesList[i].Q18Email,
                                        FormEntity = FormEntitiesList[i],
                                        IsDouble = true,
                                        PizzaPortalCode = null,
                                        SendCodeDateTime = DateTime.Now,
                                    };
                                    RepositoryServices<SendCodeLog>.Instance.AddEntity(sendCodeLog);
                                }
                                else
                                {
                                    try
                                    {
                                        SendEmailError sendEmailError = new SendEmailError()
                                        {
                                            DateTime = DateTime.Now,
                                            Email = FormEntitiesList[i].Q18Email,
                                            FormEntity = FormEntitiesList[i],
                                        };
                                        RepositoryServices<SendEmailError>.Instance.AddEntity(sendEmailError);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Save SendEmailError Error: {0}", ex);
                                        return 102;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error code: {0}", ex);
                                return 202;
                            }
                        }
                    }
                }
            }
            try
            {
                Log log = new Log()
                {
                    DownloadingDateTime = DownloadingTime,
                    DownloadingTimeSpan = DownloadingTimeSpan,
                    NewPosts = NewForms,
                    PostsWithEmail = NewFormsWithEmail,
                };
                RepositoryServices<Log>.Instance.AddEntity(log);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot save new dubled SendCodeLog to base, bot stop! error message {0}", ex);
                return 301;
            }
            return 1;
        }
    }
}
