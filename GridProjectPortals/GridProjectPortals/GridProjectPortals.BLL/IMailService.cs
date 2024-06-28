using GridProjectPortals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.BLL
{
    public interface IMailServices
    {
        Task SendEmailAsync(sendMail sendMail);
    }
}
