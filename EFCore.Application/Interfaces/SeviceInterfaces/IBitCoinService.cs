using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EFCore.Application.Services.BitCoinService;

namespace EFCore.Application.Interfaces.SeviceInterfaces
{
    interface IBitCoinService
    {
        Task<double> GetExchangeRate(Currency currency);
    }
}
