using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EG.Walks.Application.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, string role);
    }
}
