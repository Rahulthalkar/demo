using GridProjectPortals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.DAL.Interface
{
    public interface ITokenRespository
    {
        public bool GenerateToken(string username, string password);
    }
}
