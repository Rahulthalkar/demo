using GridProjectPortals.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.DAL.Interface
{
    public interface ISendIMailRepository
    {
        Task SendEmailAsync(sendMail sendMail);
    }
}
