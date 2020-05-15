using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cw4.Middlewares;
using Cw4.Models;
using Cw4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Cw4
{
    public class Startup
    {
     

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Cw10DbContext>(options =>
            { 
                options.UseSqlServer("Data Source=db-mssql;Initial Catalog=s15131;Integrated Security=True");
            });
            /* services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
              {

                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidIssuer = "s15131",
                      ValidAudience= "Students",
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))


                  };

              });*/
            services.AddScoped<IStudentDbService, SqlServerStudentDbService>();
            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStudentDbService service)
        {
           
           if (env.IsDevelopment())
           {
               app.UseDeveloperExceptionPage();
           }
            /*  app.UseMiddleware<LoggingMiddleware>();
             app.Use(async (context, next) =>
             {
                 if (!context.Request.Headers.ContainsKey("Index"))
                 {
                     context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                     await context.Response.WriteAsync(text: "Nie podoano indexu");
                     return;
                 }

                 string index = context.Request.Headers["Index"].ToString();
                 var stud = service.GetStudent(index);
                 if (stud==null)
                 {
                     context.Response.StatusCode = StatusCodes.Status404NotFound;
                     await context.Response.WriteAsync(text: "Brak studenta o podanym indexie");
                     return;
                 };
              await next();
              })*/


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

      
    }
}
