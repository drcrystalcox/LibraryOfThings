
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
    }
}