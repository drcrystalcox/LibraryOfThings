using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using LibraryWebApp.BusinessLogic;

namespace LibraryWebApp.Controllers
{
    [Route("api/tools")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
	    private readonly ILogger<ToolsController> _logger;

	    private readonly IToolServices _toolServices;

	    public ToolsController(ILogger<ToolsController> logger, IToolServices toolServices)
        {
	        _logger = logger;
	        _toolServices = toolServices;
        }
 
        [HttpGet]
        public  IActionResult GetAllTools()
        {
            try
            {
                var tools =  _toolServices.getAllTools();
 
                _logger.LogInformation($"Returned all owners from database.");
                return Ok(tools);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

         [HttpGet("{id}", Name="ToolById")]
        public IActionResult GetToolById(string id)
        {
            try
            {
                var tool = _toolServices.GetToolById(id);
        
                if (tool == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                _logger.LogInformation($"Returned owner with id: {id}");
                
                return Ok(tool); 
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}");
                    return StatusCode(500, "Internal server error");
            }
        }

    }
}

