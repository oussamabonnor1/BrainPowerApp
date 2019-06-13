using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using leaderboardAPI.Models;

namespace leaderboardAPI
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
            services.AddDbContext<PlayerContext>(opt => {
                opt.UseInMemoryDatabase("LeaderBoard");
                opt.EnableSensitiveDataLogging(true);
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            }
            //app.UseStaticFiles();
            app.UseMvc();

            app.Run(async (context) => {
                await context.Response.WriteAsync("No valid endpoint was submitted");
            });
        }
    }
}
