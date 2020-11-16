using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Academic.Entities;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<String> _roles;

    public AuthorizeAttribute(string rol)
    {
        _roles = new String[] {rol}; 
        
    }
    public void OnAuthorization(AuthorizationFilterContext context) 
    { 
        var user = (Users) context.HttpContext.Items["Users"];
        if ((user == null) || (_roles.Any() && !_roles.Contains((user.TipUtilizator))))
        { 
            context.Result = new JsonResult(new {message = "Neautorizat"})
                {StatusCode = StatusCodes.Status401Unauthorized};
        }
    }
}