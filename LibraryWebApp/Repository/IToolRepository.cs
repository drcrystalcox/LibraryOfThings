using System.Collections.Generic;
using LibraryWebApp.Models;
namespace LibraryWebApp.Repository{
    public interface IToolRepository{
         public IEnumerable<Tool> GetAllTools() ;
        public Tool GetToolById(string toolId);

        public Tool Create(Tool tool);
        
        public void Update(string id, Tool toolIn);

        public void Remove(Tool toolIn);
        public void Remove(string id);


    }
}