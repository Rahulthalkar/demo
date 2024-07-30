using GridProjectPortals.DAL.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.DAL
{
    public class TokenRespository : ITokenRespository
    {
        private readonly string connectionString;
        public TokenRespository(IConfiguration configuration)
        {
            connectionString = Convert.ToString(configuration.GetSection("ConnectionStrings:GRIDDb").Value);
        }
        public bool GenerateToken(string username, string password)
        {
            try
            {
                using(var dbcontext=new GridDbContext(connectionString))
                {
                    var token=(from tk in dbcontext.tblUsers 
                               where tk.Email==username && tk.Password==password 
                               select tk).FirstOrDefault();
                    if (token!=null)
                    {
                        return true;
                    }else
                    {
                        return false;
                    }
                }
            }catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
    }
}
