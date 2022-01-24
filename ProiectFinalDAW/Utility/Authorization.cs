using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using ProiectFinalDAW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ProiectFinalDAW.Utility
{
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly ICollection<role> _roles;
        public AuthorizationAttribute(params role[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var unauthorizedStatusCodeObject = new JsonResult(new { Message = "Unauthorized" })
            { StatusCode = StatusCodes.Status401Unauthorized };

            if (_roles == null)
            {
                context.Result = unauthorizedStatusCodeObject;
            }

            var user = (User)context.HttpContext.Items["User"];
            if (user == null || !_roles.Contains(user.Role))
            {
                context.Result = unauthorizedStatusCodeObject;
            }

        }
    }
}
