using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Interfaces.SeviceInterfaces
{
    public interface IAuthService
    {
        string DecodeJWTTOken(string token);
    }
}
