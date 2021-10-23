namespace Api.Utilities
{
	[Microsoft.AspNetCore.Mvc.ApiController]

	[Microsoft.AspNetCore.Mvc.Route
		(template: Constants.Constant.Routing.Controller)]
	public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
	{
		public ControllerBase() : base()
		{
		}
	}
}
