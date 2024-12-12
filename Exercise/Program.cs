using Exercise.Helper;
using Exercise.Model;
using Exercise.Service;
using Exercise.Utils;

namespace Exercise
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddSingleton<IOilService, OilService>();

            var url = builder.Configuration[CommonConfigVariables.Url].ThrowExIfNullOrEmpty();

            builder.Services.AddControllers();

            var app = builder.Build();

            var myService = app.Services.GetRequiredService<IOilService>();
            CommonConfigVariables.ListOilPrices = await myService.GetDatasource<List<OilPrice>>(url);

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
