using Lab_03.DAL;
using Lab_03.Handlers;
using Lab_03.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_03
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

            //add jwt token auth

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "Gakko",
                    ValidAudience = "Students",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                };
            });

           // services.AddAuthentication("AuthenticationBasic") //name of default policy
          //      .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("AuthenticationBasic", null);

            services.AddSingleton<IDbService, MockDbService>(); //injection of db service
            services.AddControllers();

           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbService service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseMiddleware<LoggingMiddleware>();

            //order of middlewares is important
            

            app.Use(async (context, next) =>
            {
                if (!context.Request.Headers.ContainsKey("Index")) //does not contain index number in url
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Index number required");
                    return;
                }

                string index = context.Request.Headers["Index"].ToString(); //get string from url to check if this student is in db
                
                var st = service.GetStudentByIndex(index); //finish the method
                if (st == null) // there is no student with this student number
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Incorrect Index number");
                    return;
                }

                await next(); //calls the next middleware
            });


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
