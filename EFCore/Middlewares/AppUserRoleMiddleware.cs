using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Middlewares
{
    public static class AppUserRoleMiddleware
    {
        public static IApplicationBuilder UserRoleSettingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserRoleMiddleware>();
        }
    }
    

}
