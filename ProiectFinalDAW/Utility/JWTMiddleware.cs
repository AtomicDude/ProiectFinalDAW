using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.UserRepository;
using ProiectFinalDAW.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;


namespace ProiectFinalDAW.Utility
{
    public class JWTmiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JWTmiddleware(IOptions<AppSettings> appSettings, RequestDelegate next)
        {
            _appSettings = appSettings.Value;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IUserRepository userRepository, IJWTutils jWTutils)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var userId = jWTutils.ValidateToken(token);

            if (userId != Guid.Empty)
            {
                httpContext.Items["User"] = userRepository.Get(userId);
            }

            await _next(httpContext);
        }
    }
}
