using System.Text;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.IRepository;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace HospitalAPI
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
            services.AddDbContext<HospitalDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("HospitalDb")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphicalEditor", Version = "v1" });
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IInterestService, InterestService>();
            services.AddScoped<IInterestRepository, InterestRepository>();

            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IIssueRepository, IssueRepository>();

            
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtManagerRepository, JwtManagerRepository>();

            services.AddTransient<IEmailService, EmailService>();
            
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepository, CartRepository>();

            services.AddScoped<IKeyPointRepository, KeyPointRepository>();
            services.AddScoped<IKeyPointService, KeyPointService>();

            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReportRepository, ReportRepository>();

            services.AddScoped<ITourService, TourService>();
            services.AddScoped<ITourRepository, TourRepository>();

            services.AddSingleton<ReportGenerator>();
            services.AddHostedService<ReportGenerator>();

            services.AddCors(options =>
            {
                options.AddPolicy("Policy1", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .WithMethods("POST", "GET", "PUT", "DELETE")
                        .WithHeaders(HeaderNames.ContentType);
                });
            });


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TouristPolicy", policy => policy.RequireRole(Role.Tourist.ToString()));
                options.AddPolicy("AuthorPolicy", policy => policy.RequireRole(Role.Author.ToString()));
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole(Role.Admin.ToString()));
            });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HospitalAPI v1"));
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
   
}
