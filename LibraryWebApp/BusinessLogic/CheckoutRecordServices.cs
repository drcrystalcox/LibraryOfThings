
using System.Collections.Generic;
using LibraryWebApp.Models;
using LibraryWebApp.Repository;
using System;

namespace LibraryWebApp.BusinessLogic{
    public class CheckoutRecordServices : ICheckoutRecordServices {
        
        protected ICheckoutRecordRepository _checkoutRecordRepository;
        protected IToolServices _toolServices;

        public CheckoutRecordServices(ICheckoutRecordRepository checkoutRecordRepo,IToolServices toolservices){
            _checkoutRecordRepository = checkoutRecordRepo;
            _toolServices = toolservices;
        }
        public IEnumerable<CheckoutRecord> getAllCheckoutRecords(){
            var results =  _checkoutRecordRepository.GetAllCheckoutRecords();
          
			
            /*var results = await _repositoryWrapper.Owner.GetAllOwnersAsync();

			var ownerDtos = results.Select(o => new OwnerDto()
			{
				Id = o.Id,
				Name = o.Name,
				Address = o.Address,
				Accounts = o.Accounts.Select(a => new AccountDto()
				{
					AccountType = a.AccountType,
					DateCreated = a.DateCreated,
					Id = a.Id
				})
			});

			return ownerDtos;*/
            //return _mapper.Map<IEnumerable<OwnerDto>>(results);
            return results;
        }
        public IEnumerable<CheckoutRecordWithItemDetailsDto> getAllCheckoutRecordsWithItemDetails(){
            var results =  _checkoutRecordRepository.GetAllCheckoutRecords();

            IList<CheckoutRecordWithItemDetailsDto> checkoutRecordsWithDetailsDtos = new List<CheckoutRecordWithItemDetailsDto>();
            foreach(CheckoutRecord cr in results) {
                Tool item = _toolServices.GetToolById(cr.ItemCheckedOutId);
                //Console.WriteLine($"Found {cr.ItemCheckedOutId}: {item.Name}");
                
                CheckoutRecordWithItemDetailsDto crDetails = new CheckoutRecordWithItemDetailsDto() {
                    
                    CheckoutRecordId =cr.CheckoutRecordId,
                    ItemCheckedOutId = cr.ItemCheckedOutId,
                    theItem = new Tool(){
                        ToolId=item.ToolId,
                        Name=item.Name,
                        Description=item.Description,
                        QuantityAvailable=item.QuantityAvailable,
                        DailyCost=item.DailyCost,
                        ReplacementCost=item.ReplacementCost
    
                    },
                    CustomerId =cr.CustomerId,
                     DateCheckedOut=cr.DateCheckedOut,  
                     DateDue=cr.DateDue,
                     DateReturned =cr.DateReturned,
                      Notes=cr.Notes,
                       AgreedDailyCost=cr.AgreedDailyCost, 
                        AmountPaid =cr.AmountPaid,
                         HasBeenReturned =cr.HasBeenReturned

                };
                checkoutRecordsWithDetailsDtos.Add(crDetails);
                //Console.WriteLine(crDetails);
            } 
            
            return checkoutRecordsWithDetailsDtos;
          
			
            /*var results = await _repositoryWrapper.Owner.GetAllOwnersAsync();

			var ownerDtos = results.Select(o => new OwnerDto()
			{
				Id = o.Id,
				Name = o.Name,
				Address = o.Address,
				Accounts = o.Accounts.Select(a => new AccountDto()
				{
					AccountType = a.AccountType,
					DateCreated = a.DateCreated,
					Id = a.Id
				})
			});

			return ownerDtos;*/
            //return _mapper.Map<IEnumerable<OwnerDto>>(results);
            //return results;
        }

        public CheckoutRecord GetCheckoutRecordById(string checkoutRecordId){
            var result = _checkoutRecordRepository.GetCheckoutRecordById(checkoutRecordId);
            //do mapping if necessary
            return result;
        }


        public CheckoutRecord CreateCheckoutRecord(CheckoutRecordForCreationDto checkoutRecordForCreation){
            //***Validate input;
            //TODO

            //map the creation dto to regular checkoutRecord
            CheckoutRecord checkoutRecord = new CheckoutRecord(){
                   
                    ItemCheckedOutId=checkoutRecordForCreation.ItemCheckedOutId,
                    CustomerId=checkoutRecordForCreation.CustomerId,
                    DateCheckedOut=checkoutRecordForCreation.DateCheckedOut,
                    DateDue=checkoutRecordForCreation.DateDue,
                    AgreedDailyCost=checkoutRecordForCreation.AgreedDailyCost,
                    Notes=checkoutRecordForCreation.Notes,
                   //Just need a placeholder for return date
                    DateReturned=DateTime.MinValue,
                    AmountPaid=0,
                    HasBeenReturned=false
                    
                };
            /*
                public string ItemCheckedOutId {get;set;}
        public string CustomerId {get;set;}
        public DateTime DateCheckedOut {get;set;}
        public DateTime DateDue {get;set;}
        public IList<string> Notes {get;set;}
        public Decimal AgreedDailyCost {get;set;}
            */
            /*
public string CheckoutRecordId {get;set;}

        public string ItemCheckedOutId {get;set;}

        public string CustomerId {get;set;}

        public DateTime DateCheckedOut {get;set;}
        public DateTime DateDue {get;set;}
        public DateTime DateReturned {get;set;}
        public IList<string> Notes {get;set;}
        public decimal AgreedDailyCost {get;set;}
        public decimal AmountPaid {get;set;}

        public Boolean HasBeenReturned {get;set;}
            */

                CheckoutRecord createdCheckoutRecord=_checkoutRecordRepository.Create(checkoutRecord);
                
                //decrement the item id quantity
                string itemId = createdCheckoutRecord.ItemCheckedOutId;
                //later, generalize to be items instead of tools
                Tool originalTool = _toolServices.GetToolById(itemId);
                ToolForUpdateDto tool = new ToolForUpdateDto() {
                    Name = originalTool.Name,
                    Description=originalTool.Description,
                    DailyCost =originalTool.DailyCost,
                    ReplacementCost=originalTool.ReplacementCost,
                    QuantityAvailable = --originalTool.QuantityAvailable
                };
                _toolServices.UpdateTool(itemId, tool);
                
                
                
                return createdCheckoutRecord;

        }

        public void UpdateCheckoutRecord(string id, CheckoutRecordForCheckInUpdateDto checkoutRecordForUpdate)
		{
			// Want to try to find the owner first before updating. Throw error if the owner does not exist.
			// What if the user attempts to modify the id field for the owner? Throw error. 
			var checkoutRecord = GetCheckoutRecordById(id);
			if (checkoutRecord == null)
			{
				//_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
				return;
				// TODO: need to return notfound to the user
			}

//TODO validate new checkoutRecord
			//_mapper.Map(owner, ownerEntity);
            checkoutRecord.DateReturned=checkoutRecordForUpdate.DateReturned;
            checkoutRecord.AmountPaid=checkoutRecordForUpdate.AmountPaid;
            checkoutRecord.Notes=checkoutRecordForUpdate.Notes;
            checkoutRecord.HasBeenReturned=true;
            //leave all the rest unchanged from after the retrieve
            
             Tool originalTool = _toolServices.GetToolById(checkoutRecord.ItemCheckedOutId);
                ToolForUpdateDto tool = new ToolForUpdateDto() {
                    Name = originalTool.Name,
                    Description=originalTool.Description,
                    DailyCost =originalTool.DailyCost,
                    ReplacementCost=originalTool.ReplacementCost,
                    QuantityAvailable = ++originalTool.QuantityAvailable
                };
                _toolServices.UpdateTool(checkoutRecord.ItemCheckedOutId, tool);
            

			 _checkoutRecordRepository.Update(checkoutRecord.CheckoutRecordId, checkoutRecord);
		}
  /*
public string CheckoutRecordId {get;set;}

        public string ItemCheckedOutId {get;set;}

        public string CustomerId {get;set;}

        public DateTime DateCheckedOut {get;set;}
        public DateTime DateDue {get;set;}
        public DateTime DateReturned {get;set;}
        public IList<string> Notes {get;set;}
        public decimal AgreedDailyCost {get;set;}
        public decimal AmountPaid {get;set;}

        public Boolean HasBeenReturned {get;set;}
            */
		public void DeleteCheckoutRecord(CheckoutRecord checkoutRecordToDelete)
		{
			 _checkoutRecordRepository.Remove(checkoutRecordToDelete);
             //todo shouldn't these return true or false? or deal with an exception
            
		}

    }
}