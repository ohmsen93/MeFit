using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Reflection;
using webapi.DatabaseContext;
using webapi.Services;
using System.Text.Json.Serialization;
using webapi.Services.ContributionrequestServices;
using webapi.Services.ExerciseServices;
using webapi.Services.SetServices;
using webapi.Services.UserProfileServices;
using webapi.Services.WorkoutServices;
using webapi.Services.UserServices;
using webapi.Services.GaolWrokutServices;
using webapi.Services.AddressServices;
using webapi.Models;
using webapi.Services.TrainingprogramServices;
using webapi.Services.WorkoutServices;
using webapi.Services.UserProfileServices;
using webapi.Services.GoalServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace webapi
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<ISetService, SetService>();
            builder.Services.AddTransient<IExerciseService, ExerciseService>();
            builder.Services.AddTransient<IWorkoutService, WorkoutService>();
            builder.Services.AddTransient<IUserProfileService, UserProfileService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IGoalWorkoutService, GoalWorkoutService>();
            builder.Services.AddTransient<IAddressService, AddressService>();

            //Sets the endpoint urls to lowercase
            builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            builder.Services.AddTransient<ITrainingprogramService, TrainingprogramService>();

            builder.Services.AddTransient<IContributionrequestService, ContributionrequestService>();

            builder.Services.AddTransient<IGoalService, GoalService>();


            // Add controllers and database context to the container
            //builder.Services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //});

            builder.Services.AddControllers();


            builder.Services.AddDbContext<MeFitContext>();

            // Add AutoMapper and Swagger to the container
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Set up Swagger documentation
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "MeFit API",
                    Description = "This API provides access to information about the MeFit application.",
                    Contact = new OpenApiContact
                    {
                        Name = "Emil Bo Solgaard Utsen, Farhang Younis, Mads Ohmsen, Simon Løvschal",
                        Url = new Uri("https://github.com/hostilelogout/MeFit/tree/main/webapi")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT 2022",
                        Url = new Uri("https://opensource.org/license/mit/")
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            // Configure authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = "mefit",
                        ValidIssuer = "https://lemur-3.cloud-iam.com/auth/realms/mefitexp",
                        IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                        {
                            var client = new HttpClient();
                            var keyuri = "https://lemur-3.cloud-iam.com/auth/realms/mefitexp/protocol/openid-connect/certs";
                            //Retrieves the keys from keycloak instance to verify token
                            var response = client.GetAsync(keyuri).Result;
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);
                            return keys.Keys;
                        }
                    };
                });

            // Build the application.
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Use Swagger UI in development environment
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeFit v1"));
            }

            // Set up database migration
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<MeFitContext>();
            //dbContext.Database.EnsureCreated(); 
            //dbContext.Database.Migrate();

            // Set up HTTPS redirection, authentication and authorization
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            // Map the controllers to HTTP endpoints
            app.MapControllers();

            // Run the application.
            app.Run();


        }
    }
}