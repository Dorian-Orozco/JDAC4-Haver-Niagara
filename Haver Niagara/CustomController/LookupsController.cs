﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Haver_Niagara.CustomController
{
    public class LookupsController : CognizantController
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["returnURL"] = "/Lookup?Tab=" + ControllerName() + "-Tab";
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            ViewData["returnURL"] = "/Lookup?Tab=" + ControllerName() + "-Tab";
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
