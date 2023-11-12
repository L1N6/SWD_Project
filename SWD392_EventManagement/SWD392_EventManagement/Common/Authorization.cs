using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SWD392_EventManagement.Models;
using System.Text;

namespace SWD392_EventManagement.Common
{
    public class Authorization
    {
        public class AdminPermissionAttribute : Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var session = context.HttpContext.Session;
                if (session != null && session.TryGetValue("Role", out byte[] roleBytes))
                {
                    var role = int.Parse(Encoding.UTF8.GetString(roleBytes));
                    if (role != 1)
                    {
                        // get query string from url
                        var query = context.HttpContext.Request.QueryString;
                        context.Result = new RedirectToPageResult("/Login", new { returnUrl = context.HttpContext.Request.Path + query });
                    }
                }
                else
                {
                    var query = context.HttpContext.Request.QueryString;
                    context.Result = new RedirectToPageResult("/Login", new { returnUrl = context.HttpContext.Request.Path + query });
                }
            }
        }


        public class LoggedPermissionAttribute : Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var session = context.HttpContext.Session;
                if (session.TryGetValue("Role", out byte[] roleBytes) && int.TryParse(Encoding.UTF8.GetString(roleBytes), out int role))
                {
                    if (role != null || role <= 0)
                    {
                        var query = context.HttpContext.Request.QueryString;
                        context.Result = new RedirectToPageResult("/Login", new { returnUrl = context.HttpContext.Request.Path + query });
                    }
                }
                else
                {
                    var query = context.HttpContext.Request.QueryString;
                    context.Result = new RedirectToPageResult("/Login", new { returnUrl = context.HttpContext.Request.Path + query });
                }
            }
        }


        public class MemberPermissionAttribute : Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var session = context.HttpContext.Session;
                if (session.TryGetValue("Role", out byte[] roleBytes) && int.TryParse(Encoding.UTF8.GetString(roleBytes), out int role))
                {
                    if (role != 2)
                    {
                        var query = context.HttpContext.Request.QueryString;
                        context.Result = new RedirectToPageResult("/Login", new { returnUrl = context.HttpContext.Request.Path + query });
                    }
                }
                else
                {
                    var query = context.HttpContext.Request.QueryString;
                    context.Result = new RedirectToPageResult("/Login", new { returnUrl = context.HttpContext.Request.Path + query });
                }
            }
        }

        public class M7xks96q3Jg5zU8DHmuBVAcoKCypQN8yke : Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var session = context.HttpContext.Session;
                if (session.TryGetValue("Role", out byte[] roleBytes) && int.TryParse(Encoding.UTF8.GetString(roleBytes), out int role))
                {
                    if (role != 3)
                    {
                        var query = context.HttpContext.Request.QueryString;
                        context.Result = new RedirectToPageResult("/Login", new { returnUrl = context.HttpContext.Request.Path + query });
                    }
                }
                else
                {
                    var query = context.HttpContext.Request.QueryString;
                    context.Result = new RedirectToPageResult("/Login", new { returnUrl = context.HttpContext.Request.Path + query });
                }
            }
        }

    }
}