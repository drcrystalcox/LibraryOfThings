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
        protected IToolRepository _toolRepository;
        protected ICheckoutRecordRepository _checkoutRecordRepository;
        public WeatherForecastController(IToolRepository toolRepository, ICheckoutRecordRepository checkoutRecordRepository, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _toolRepository = toolRepository;
            _checkoutRecordRepository = checkoutRecordRepository;
            //don't forget to add this interface to dependency injection in startup
        }

        [HttpGet]
        public ActionResult<List<CheckoutRecord>> Get()
        {
            var result= _checkoutRecordRepository.GetAllCheckoutRecords().ToList();
                      //  _books.Find(book => true).ToList();
            Console.WriteLine(result);
            foreach(CheckoutRecord t in result) {
                Console.WriteLine(t.CheckoutRecordId);
                Console.WriteLine(t);
                Console.WriteLine(t.ToString());
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
         [HttpGet]
         [Route("getone/{id}")]
        public ActionResult<CheckoutRecord> Get(string id)
        {
            var result= _checkoutRecordRepository.GetCheckoutRecordById(id);
                      //  _books.Find(book => true).ToList();
            Console.WriteLine(result);
            

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
