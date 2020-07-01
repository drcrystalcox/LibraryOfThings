using System.Collections.Generic;
using LibraryWebApp.Models;
namespace LibraryWebApp.BusinessLogic{
    public interface IToolServices{
        public IEnumerable<Tool> getAllTools();
          public Tool GetToolById(string toolId);
        
    }
}