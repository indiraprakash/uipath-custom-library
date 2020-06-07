using Microsoft.Exchange.WebServices.Data;
using System;
using System.Activities;
using System.ComponentModel;

namespace ExchangeMailService
{
    public class ExchangeMailService:CodeActivity
    {

        [Category("Output"), Description("Get the Exchange Connection")]
        public OutArgument<string> ExchangeConnection { get; set; }
        public InArgument<string> UserName { get; set; }
        public InArgument<string> Password { get; set; }
        public InArgument<string> EmailAddress { get; set; }





        #region Create connection 
        /// <summary>
        /// Create a connection to exchage server using Exchange webservice
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// 
        public ExchangeService ConnectToExchangeService(String user,String password,string email)
        {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
            service.Credentials = new WebCredentials(user,password);
            service.AutodiscoverUrl(email);
            return service;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="receiver"></param>
        public void SendExchangeEmail(ExchangeService service,string subject,string body,string receiver) 
        {
            EmailMessage message = new EmailMessage(service);
            message.Subject = subject;
            message.Body = subject;
            message.ToRecipients.Add(receiver);
            message.Save();

            message.SendAndSaveCopy();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(CodeActivityContext context)
        {
            

            ExchangeConnection.Set(context, ConnectToExchangeService(UserName.Get(context), Password.Get(context), EmailAddress.Get(context)));
        }
    }
}
