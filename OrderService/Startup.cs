using EquipmentService.BLL.Interfaces;
using EquipmentService.BLL.Managers;
using EquipmentService.DAL;
using EquipmentService.DAL.Interfaces;
using EquipmentService.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

namespace EquipmentService
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EquipmentService", Version = "v1" });
            });
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnString")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IOrderManager, OrderManager>();

            services
               .AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer("AuthScheme", options =>
               {
                   options.RequireHttpsMetadata = true;
                   options.SaveToken = true;
                   var secret = Configuration.GetSection("Jwt").GetSection("Token").Get<String>();
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateLifetime = true,
                       RequireExpirationTime = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
                   options.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                           {
                               context.Response.Headers.Add("Token-Expired", "true");
                           }
                           return Task.CompletedTask;
                       }
                   };
               });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", policy => policy.RequireRole("Admin").RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());
                opt.AddPolicy("Employee", policy => policy.RequireRole("Employee").RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());
                opt.AddPolicy("Manager", policy => policy.RequireRole("Manager").RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EquipmentService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
