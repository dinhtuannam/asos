using BuildingBlock.Grpc;
using BuildingBlock.Grpc.Protos;
using BuildingBlock.Installers;
using Ordering.API.Data.Seeding;
using Ordering.API.Implements;
using Ordering.API.Interfaces;
using Ordering.API.Settings;
using System.Reflection;
using BuildingBlock.Grpc.Settings;

namespace Ordering.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.Configure<VnpaySettings>(builder.Configuration.GetSection("VnpaySettings"));

			#region BuildingBlock
			builder.InstallSerilog();
			builder.Services.InstallGrpc(builder.Configuration, new ProtoSetting()
			{
				Identity = true
			});
			builder.Services.InstallSwagger("v1", "API Ordering");
			builder.Services.InstallCORS();
			builder.Services.InstallAuthentication();
			builder.Services.InstallMediatR(assembly);
			#endregion

			#region DataContext
			var cnStr = builder.Configuration.GetConnectionString("Database");
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
			builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(cnStr));
			#endregion

			builder.Services.AddTransient<IDataContextInitializer, DataContextInitializer>();
			builder.Services.AddScoped<IPaymentService, PaymentService>();

			var app = builder.Build();
			app.UseSwaggerService();
			app.UseCors();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<ExceptionMiddleware>();
			app.MigrationAutoUpdate<DataContext>();
			app.MapControllers();

			using (var scope = app.Services.CreateScope())
			{
				IServiceProvider services = scope.ServiceProvider;
				IDataContextInitializer initializer = services.GetRequiredService<IDataContextInitializer>();
				await initializer.SeedAsync();
				scope.Dispose();
			}

			app.Run();
		}
	}
}
