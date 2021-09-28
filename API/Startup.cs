using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCQRS3.Application.Query;
using TestCQRS3.Domain;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Infrastructure.Persistence;
using TestCQRS3.Infrastructure.Services;
using TestCQRS3.Application.Command.Commands.Items;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestCQRS3.Application.Command.Common;
using Microsoft.AspNetCore.Authorization;

namespace TestCQRS3.API
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
            //DataBase Settings Config
            services.Configure<TestCQRS3DatabaseSettings>(
                Configuration.GetSection(nameof(TestCQRS3DatabaseSettings)));
            services.AddSingleton<ITestCQRS3DatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TestCQRS3DatabaseSettings>>().Value);

            services.AddSingleton<ReadDBContext>();

            //Add DBContext
            services.AddScoped<ITestCQRS3DBContext, TestCQRS3DBContext>();
            services.AddDbContext<TestCQRS3DBContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Add Service
            services.AddScoped<IServiceWrapper, ServiceWrapper>();

            //Add MediatR
            services.AddMediatR(typeof(ReadDBContext).Assembly);
            services.AddMediatR(typeof(CreateItemCommand).Assembly);

            //Add JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                        };
                    });


            //services.AddSwaggerGen();
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TestCQRS3",
                    Description = "Impliment CQRS With Clean Architecture"
                });
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter �Bearer� [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                 {
                 new OpenApiSecurityScheme
                 {
                 Reference = new OpenApiReference
                 {
                 Type = ReferenceType.SecurityScheme,
                 Id = "Bearer"
                 }
                 },
                 new string[] {}
                 }
                 });
            });

            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

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
