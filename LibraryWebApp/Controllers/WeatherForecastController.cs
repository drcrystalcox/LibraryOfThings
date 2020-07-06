using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibraryWebApp.Repository;
using MongoDB.Driver;
using LibraryWebApp.Models;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        protected RepositoryContext _repositoryContext;
        public WeatherForecastController(RepositoryContext repoContext,ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _repositoryContext = repoContext;
        }

        [HttpGet]
        public ActionResult<List<Tool>> Get()
        {
            var result= _repositoryContext.Tools.Find(tool=>true ).ToList();
                      //  _books.Find(book => true).ToList();
            Console.WriteLine(result);
            foreach(Tool t in result) {
                Console.WriteLine(t.ToolId);
                Console.WriteLine(t.ToolId.ToString());
            }
            return result;

           //return tools.ToEnumerable().ToArray();;
            
            /*var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();*/
        }
    }
}
