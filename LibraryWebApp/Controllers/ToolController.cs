using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using LibraryWebApp.BusinessLogic;
using LibraryWebApp.Models;

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

        [HttpPost]
        public  IActionResult CreateTool([FromBody]ToolForCreationDto toolForCreation)
        {
            try
            {
                if (toolForCreation == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
        
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                
                Tool createdTool = _toolServices.CreateTool(toolForCreation);

                return CreatedAtRoute("ToolById", new { id = createdTool.ToolId }, createdTool);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

 [HttpPut("{id}")]
        public IActionResult UpdateTool(string id, [FromBody]ToolForUpdateDto toolForUpdate)
        {
            try
            {
                if (toolForUpdate == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
        
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

               _toolServices.UpdateTool(id, toolForUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTool(string id)
        {
            try
            {
                var tool = _toolServices.GetToolById(id);
                if(tool == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
        
                _toolServices.DeleteTool(tool);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



    }
}

