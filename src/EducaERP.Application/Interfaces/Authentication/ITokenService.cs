using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducaERP.Core.Domain.Authentication;

namespace EducaERP.Application.Interfaces.Authentication
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
