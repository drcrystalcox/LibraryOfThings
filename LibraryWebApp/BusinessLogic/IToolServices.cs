using System.Collections.Generic;
using LibraryWebApp.Models;
namespace LibraryWebApp.BusinessLogic{
    public interface IToolServices{
        public IEnumerable<Tool> getAllTools();
          public Tool GetToolById(string toolId);
          public Tool CreateTool(ToolForCreationDto toolForCreation);
           public void UpdateTool(string id, ToolForUpdateDto toolForUpdate);
		public void DeleteTool(Tool toolToDelete);
        
    }
}