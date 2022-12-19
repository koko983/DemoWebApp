using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}
			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/testing", ctx =>
				{
					text = string.Empty;
					append($"environment is {env.EnvironmentName}");
					append($"app is {env.ApplicationName}");
					append(
						"AppContext.BaseDirectory",
						AppContext.BaseDirectory,
						"AppDomain.CurrentDomain.BaseDirectory",
						AppDomain.CurrentDomain.BaseDirectory,
						"env.ContentRootPath",
						env.ContentRootPath,
						"System.IO.Directory.GetCurrentDirectory()",
						System.IO.Directory.GetCurrentDirectory());
					return ctx.Response.WriteAsync(text);
				});
				endpoints.MapRazorPages();
			});
		}

		private string text = string.Empty;
		private void append(params string[] msgs)
		{
			foreach (var msg in msgs)
			{
				text += msg + "\n";
			}
		}

	}
}
