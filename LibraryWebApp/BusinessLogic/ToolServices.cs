
using System.Collections.Generic;
using LibraryWebApp.Models;
using LibraryWebApp.Repository;

namespace LibraryWebApp.BusinessLogic{
    public class ToolServices : IToolServices {
        
        protected IToolRepository _toolRepository;

        public ToolServices(IToolRepository toolRepo){
            _toolRepository = toolRepo;
        }
        public IEnumerable<Tool> getAllTools(){
            var results =  _toolRepository.GetAllTools();
          
			
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

        public Tool GetToolById(string toolId){
            var result = _toolRepository.GetToolById(toolId);
            //do mapping if necessary
            return result;
        }


        public Tool CreateTool(ToolForCreationDto toolForCreation){
            //***Validate input;
            //TODO

            //map the creation dto to regular tool
            Tool tool = new Tool(){
                   
                    Name=toolForCreation.Name,
                    Description=toolForCreation.Description,
                    DailyCost=toolForCreation.DailyCost,
                    ReplacementCost=toolForCreation.ReplacementCost,
                    QuantityAvailable=toolForCreation.QuantityAvailable
                    
                };
                Tool createdTool=_toolRepository.Create(tool);
                return createdTool;

        }

        public void UpdateTool(string id, ToolForUpdateDto toolForUpdate)
		{
			// Want to try to find the owner first before updating. Throw error if the owner does not exist.
			// What if the user attempts to modify the id field for the owner? Throw error. 
			var tool = GetToolById(id);
			if (tool == null)
			{
				//_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
				return;
				// TODO: need to return notfound to the user
			}

//TODO validate new tool
			//_mapper.Map(owner, ownerEntity);
            tool.Name=toolForUpdate.Name;
            tool.Description=toolForUpdate.Description;
            tool.DailyCost=toolForUpdate.DailyCost;
            tool.ReplacementCost=toolForUpdate.ReplacementCost;
            tool.QuantityAvailable=toolForUpdate.QuantityAvailable;
			 _toolRepository.Update(tool.ToolId, tool);
		}

		public void DeleteTool(Tool toolToDelete)
		{
			 _toolRepository.Remove(toolToDelete);
             //todo shouldn't these return true or false? or deal with an exception
            
		}

    }
}