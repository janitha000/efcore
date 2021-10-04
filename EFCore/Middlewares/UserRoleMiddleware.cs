using EFCore.Application.Interfaces.SeviceInterfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Middlewares
{
    public class UserRoleMiddleware : IMiddleware
    {
        private IAuthService _authService;

        public UserRoleMiddleware(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var role = "";
            var token = context.Request.Headers["Authorization"];
            if(token[0] is not null)
            {
                role = _authService.DecodeJWTTOken(token);
            }

            context.Items["Role"] = role;
            await next(context);


        }
    }
}
