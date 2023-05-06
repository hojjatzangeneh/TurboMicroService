using Ordering.Application.Contract.Infrastructure;
using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public Task<bool> SendEmailAsync(Email email)
        {
            throw new NotImplementedException();
        }
    }
}
