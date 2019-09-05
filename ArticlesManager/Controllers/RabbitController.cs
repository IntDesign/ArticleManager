using System.Threading.Tasks;
using ArticlesManager.RabbitMqController;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesManager.Controllers
{
    [Route("[controller]")]
    public class RabbitController
    {
        private readonly RabbitManager _manager;
        public RabbitController(RabbitManager manager) => _manager = manager;

        [Route("crawler")]
        public Task<string> RequestCrowler(string url, int level)
        {
            _manager.Send(url + "&" + level);
            return Task.FromResult(url +  " " + level);
        }
    }
}