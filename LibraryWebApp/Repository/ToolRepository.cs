using System.Collections.Generic;
using LibraryWebApp.Models;
using MongoDB.Driver;

namespace LibraryWebApp.Repository{
    public class ToolRepository : IToolRepository {
        protected RepositoryContext _repositoryContext;

        public ToolRepository(RepositoryContext repoContext){
            _repositoryContext = repoContext;
        }

        public IEnumerable<Tool> GetAllTools() {
          //  IQueryable<ToolItem>  tools = _repositoryContext.Set<ToolItem>().AsNoTracking();
           // return tools.OrderBy(to=>to.Name).ToList();

           var tools =_repositoryContext.Tools.Find(tool=>true );
           return tools.ToEnumerable();

           //Find(book => true).ToList()
        }

        public Tool GetToolById(string toolId) {
            var tool =_repositoryContext.Tools.Find<Tool>(tool=>tool.ToolId==toolId).FirstOrDefault();
            return tool;
        }
    }
}