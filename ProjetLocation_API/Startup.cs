using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ProjetLocation.API.Helpers;
using ProjetLocation.API.Services;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Tools.Security.RSA;
using DB = Tools.Database;

namespace ProjetLocation_API
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
            IConfigurationSection dbConnectionStringsSection = Configuration.GetSection("DBConnectionStrings");
            DBConnectionStrings connectionStrings = dbConnectionStringsSection.Get<DBConnectionStrings>();
            string connectionString = dbConnectionStringsSection.Get<DBConnectionStrings>().ConnectionStrings;

            IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    });
            });

            services.AddControllers();

            services.AddSingleton<KeyGenerator>();
            services.AddSingleton<DbProviderFactory>(sp => SqlClientFactory.Instance);
            services.AddSingleton(sp => new DB.ConnectionInfo(connectionString));
            services.AddSingleton<DB.Connection>();
            services.AddSingleton<AuthRepository>();
            services.AddSingleton<UserRepository>();
            services.AddSingleton<RoleRepository>();
            services.AddSingleton<GoodRepository>();
            services.AddSingleton<SectionRepository>();
            services.AddSingleton<CategoryRepository>();
            services.AddSingleton<RentalRepository>();
            services.AddSingleton<AuthService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<RoleService>();
            services.AddSingleton<GoodService>();
            services.AddSingleton<SectionService>();
            services.AddSingleton<RentalService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors();

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
