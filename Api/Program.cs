using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace Api
{
	public class Program : object
	{
		static Program()
		{
		}

		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static Microsoft.Extensions.Hosting.IHostBuilder CreateHostBuilder(string[] args)
			=> Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args: args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});

		public Program() : base()
		{
		}
	}
}
