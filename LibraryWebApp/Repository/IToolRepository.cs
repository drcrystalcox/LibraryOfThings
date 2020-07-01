using System.Collections.Generic;
using LibraryWebApp.Models;
namespace LibraryWebApp.Repository{
    public interface IToolRepository{
         public IEnumerable<Tool> GetAllTools() ;
        public Tool GetToolById(string toolId);
    }
}