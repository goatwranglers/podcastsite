using System;
using System.Threading.Tasks;
using GW.Site.Services;

namespace GW.Site.Tests.Fakes
{
    class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
