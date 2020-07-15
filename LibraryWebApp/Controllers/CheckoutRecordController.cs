using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using LibraryWebApp.BusinessLogic;
using LibraryWebApp.Models;
using System.Linq;

namespace LibraryWebApp.Controllers
{
    [Route("api/checkoutrecords")]
   [ApiController]
    public class CheckoutRecordsController : ControllerBase
    {
	    private readonly ILogger<CheckoutRecordsController> _logger;

	    private readonly ICheckoutRecordServices _checkoutRecordServices;

	    public CheckoutRecordsController(ILogger<CheckoutRecordsController> logger, ICheckoutRecordServices checkoutRecordServices)
        {
	        _logger = logger;
	        _checkoutRecordServices = checkoutRecordServices;
        }
 
        [HttpGet]
        public  IActionResult GetAllCheckoutRecords()
        {
            try
            {
                var checkoutRecords =  _checkoutRecordServices.getAllCheckoutRecords();
 
                _logger.LogInformation($"Returned all owners from database.");
                return Ok(checkoutRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        [Route("open")]
        public  IActionResult GetAllOpenCheckoutRecordsWithItemDetails()
        {
            try
            {
                var checkoutRecords =  _checkoutRecordServices.getAllCheckoutRecordsWithItemDetails();
                var openRecords = checkoutRecords.Where(c => !c.HasBeenReturned);
 
                _logger.LogInformation($"Returned all owners from database.");
                return Ok(openRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        [Route("details")]
        public  IActionResult GetAllCheckoutRecordsWithItemDetails()
        {
            try
            {
                var checkoutRecords =  _checkoutRecordServices.getAllCheckoutRecordsWithItemDetails();
 
                _logger.LogInformation($"Returned all owners from database.");
                return Ok(checkoutRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

         [HttpGet("{id}", Name="CheckoutRecordById")]
        public IActionResult GetCheckoutRecordById(string id)
        {
            try
            {
                var checkoutRecord = _checkoutRecordServices.GetCheckoutRecordById(id);
        
                if (checkoutRecord == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                _logger.LogInformation($"Returned owner with id: {id}");
                
                return Ok(checkoutRecord); 
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}");
                    return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public  IActionResult CreateCheckoutRecord([FromBody]CheckoutRecordForCreationDto checkoutRecordForCreation)
        {
            try
            {
                if (checkoutRecordForCreation == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
        
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                
                CheckoutRecord createdCheckoutRecord = _checkoutRecordServices.CreateCheckoutRecord(checkoutRecordForCreation);

                return CreatedAtRoute("CheckoutRecordById", new { id = createdCheckoutRecord.CheckoutRecordId }, createdCheckoutRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

 [HttpPut("{id}")]
        public IActionResult UpdateCheckoutRecord(string id, [FromBody]CheckoutRecordForCheckInUpdateDto checkoutRecordForUpdate)
        {
            try
            {
                if (checkoutRecordForUpdate == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
        
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

               _checkoutRecordServices.UpdateCheckoutRecord(id, checkoutRecordForUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCheckoutRecord(string id)
        {
            try
            {
                var checkoutRecord = _checkoutRecordServices.GetCheckoutRecordById(id);
                if(checkoutRecord == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
        
                _checkoutRecordServices.DeleteCheckoutRecord(checkoutRecord);

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

