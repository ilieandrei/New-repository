using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Auth
{
    public class AuthActionFilter : IFilterMetadata
    {
        private IAuthHelper _authHelper;
        public AuthActionFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var viewBag = ((Controller)context.Controller).ViewBag;
            var user = _authHelper.GetUser();
            viewBag.UserId = user.Id;
            viewBag.Username = user.Username;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
