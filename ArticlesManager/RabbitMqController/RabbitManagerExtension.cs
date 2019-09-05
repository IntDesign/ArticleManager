using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ArticlesManager.RabbitMqController
{
    public static class RabbitManagerExtension
    {
        private static RabbitManager Manager { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Manager = app.ApplicationServices.GetService<RabbitManager>();
            var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
            lifetime.ApplicationStarted.Register(OnStarted);
            lifetime.ApplicationStopped.Register(OnStop);
            return app;
        }
        private static void OnStarted()
        {
            Manager.Listen();
        }
        
        private static void OnStop()
        {
            //todo stop listener
        }
    }
}