using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using TestCQRS3.API.Helpers;
using TestCQRS3.Application.Command.Commands.Items;
using TestCQRS3.Application.Command.Common;
using TestCQRS3.Application.Query;
using TestCQRS3.Domain;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Infrastructure.Logging;
using TestCQRS3.Infrastructure.Persistence;
using TestCQRS3.Infrastructure.Services;

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
            // DataBase Settings Config
            services.Configure<TestCQRS3DatabaseSettings>(
                Configuration.GetSection(nameof(TestCQRS3DatabaseSettings)));
            services.AddSingleton<ITestCQRS3DatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TestCQRS3DatabaseSettings>>().Value);

            // Add ReadDbContext
            services.AddSingleton<ReadDBContext>();

            // Add LogDbContext
            services.AddSingleton<LogDBContext>();

            // Add DBContext
            services.AddScoped<ITestCQRS3DBContext, TestCQRS3DBContext>();
            services.AddDbContext<TestCQRS3DBContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add Service
            services.AddScoped<IServiceWrapper, ServiceWrapper>();

            // Add MediatR
            services.AddMediatR(typeof(ReadDBContext).Assembly);
            services.AddMediatR(typeof(CreateItemCommand).Assembly);

            // Add JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:ValidIssuer"],
                            ValidAudience = Configuration["Jwt:ValidIssuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                        };
                    });

            // Add Api Versioning
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            // Add Versioned Api Explorer
            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service  
                // note: the specified format code will format the version as "'v'major[.minor][-status]"  
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat  
                // can also be used to control the format of the API version in route templates  
                options.SubstituteApiVersionInUrl = true;
            });

            // Add Swagger Options
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            // Add Swagger
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                //swagger.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    Version = "v1",
                //    Title = "TestCQRS3",
                //    Description = "Impliment CQRS With Clean Architecture"
                //});
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
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
                swagger.OperationFilter<SwaggerDefaultValues>();
            });

            // Add Controllers
            services.AddControllersWithViews();

            // Add Logger
            services.AddScoped<ILogger, Logger>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(
            options =>
            {
                // build a swagger endpoint for each discovered API version  
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
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
