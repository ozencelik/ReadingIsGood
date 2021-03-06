using Api.Configuration;
using AutoMapper;
using Core.Infrastructure;
using Core.Services.Customers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace Api
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

            #region Settings
            // Configure Options Patternvar 
            var settings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            #endregion

            #region AutoMapper
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });
            services.AddSingleton(mappingConfig.CreateMapper());
            #endregion

            #region Dependencies
            services.RegisterDependencies();
            #endregion

            #region Db
            //Add Db Connection
            string connectionString = Configuration.GetConnectionString("live_readingisgood");
            services.AddDbContext(connectionString);

            services.EnsureDatabaseCreated();
            #endregion

            #region JWT
            //Add Jwt Token
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var customerService = context.HttpContext.RequestServices.GetRequiredService<ICustomerService>();
                        var customerId = int.Parse(context.Principal.Identity.Name);
                        var customer = await customerService.GetCustomerByIdAsync(customerId);
                        if (customer == null)
                        {
                            // return unauthorized if customer no longer exists
                            context.Fail("Unauthorized");
                        }
                    }
                };
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });
            #endregion

            #region Swagger
            //Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiRoutes.Version, new OpenApiInfo
                {
                    Version = ApiRoutes.Version,
                    Title = "Reading Is Good API",
                    Description = "ReadingIsGood is an API to buy books faster than ever :)",
                    Contact = new OpenApiContact
                    {
                        Name = "�zen� �elik",
                        Email = "ozenc.celik@hotmail.com",
                        Url = new Uri("https://www.linkedin.com/in/%C3%B6zen%C3%A7-%C3%A7elik/"),
                    }
                });

                var security = new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                     },
                      new string[] { }
                   }
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(security);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api " + ApiRoutes.Version));

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
