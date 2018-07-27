using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.AspNetCore.Results
{
	public class ErrorResult : JsonResult
	{
		public ErrorResult(int code, string message) 
			: base(new {
			ErrorCode = code,
			Message = message
		})
		{ }
	}
}
