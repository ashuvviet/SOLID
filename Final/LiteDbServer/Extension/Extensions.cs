using DataBaseCore.DBContext;
using Microsoft.Extensions.DependencyInjection;

namespace DataBaseCore.Extensions
{
    public static class Extensions
    {
        public static void RegisterDatabase(this IServiceCollection services, string pathToDB)
        {
            services.AddSingleton<LiteDBContext>();
        }
    }
}
