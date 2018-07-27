using Microsoft.AspNetCore.Mvc.Filters;
using Miki.AspNetCore.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Miki.WebAPI.Discord
{
	internal class BasicAuthenticationAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext actionContext)
		{
			if (actionContext.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
			{
				if (token.ToString() != Startup.AccessKey)
				{
					actionContext.Result = new ErrorResult(400, "Unauthorized");
				}
			}
			else
			{
				actionContext.Result = new ErrorResult(400, "Unauthorized");
			}
		}
	}
}
