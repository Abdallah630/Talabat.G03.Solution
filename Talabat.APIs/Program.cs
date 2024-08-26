using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using Talabat.APIs.Error;
using Talabat.APIs.Helper;
using Talabat.APIs.MiddleWares;
using Talabat.Core;
using Talabat.Core.Module.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Repository;
using Talabat.Repository._Data.DataContext;
using Talabat.Repository._Data.DataSeeding;
using Talabat.Repository._Identity;
using Talabat.Repository._Identity.DataSeed;
using Talabat.Repository.GenericRepository;
using Talabat.Repository.Repositories.BasketRepository;
using Talabat.Service.AuthService;
using Talabat.Service.OrderService;

namespace Talabat.APIs
{
    public class Program
	{
		public static async Task Main(string[] args)
		{

			int x;
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			#region Configure Service
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationIdentityDbContext>();
			builder.Services.AddScoped<IConnectionMultiplexer>(serverProvider =>
			{
				var connection = builder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			});
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
					ValidateAudience = true,
					ValidAudience = builder.Configuration["JWT:ValidAudience"],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:AuthKey"])),
					ValidateLifetime = true,
					ClockSkew =TimeSpan.Zero,
				};
			});
			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(p => p.Value.Errors.Any())
						.SelectMany(p => p.Value.Errors)
						.Select(p => p.ErrorMessage);

					var response = new ApiValidationResponse
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);
				};
			});
			
			builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
			builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddAutoMapper(typeof(MappingProfile));
			builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
			builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
			#endregion
			var app = builder.Build();

			#region Apply All Pending Migrations [Update-Database] and Data Seeding
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var _dbContext = services.GetRequiredService<StoreContext>();
			var _identityDbContext = services.GetRequiredService<ApplicationIdentityDbContext>();
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{
				await _dbContext.Database.MigrateAsync();
				await StoreContextSeed.SeedAsync(_dbContext);
				await _identityDbContext.Database.MigrateAsync();
				var _userManger = services.GetRequiredService<UserManager<ApplicationUser>>();
				await ApplicationIdentityDataSeed.SeedUserAsync(_userManger);
			}

			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an Error has been Occurred during apply the Migration");
			} 
			#endregion

			#region Configure kestrel Middle wares
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseStaticFiles();
			app.UseMiddleware<ExceptionMiddleWare>();
			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}
