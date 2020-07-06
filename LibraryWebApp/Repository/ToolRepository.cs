using System.Collections.Generic;
using LibraryWebApp.Models;
using MongoDB.Driver;
using System;

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

        public Tool Create(Tool tool) {
            _repositoryContext.Tools.InsertOne(tool);
            Console.WriteLine("tool after inserting:"+tool.ToolId);
            //
            return tool;
        }
        
        public void Update(string id, Tool toolIn){
            _repositoryContext.Tools.ReplaceOne(tool=>tool.ToolId==id, toolIn);
        }

        public void Remove(Tool toolIn) {
            _repositoryContext.Tools.DeleteOne(tool=>tool.ToolId==toolIn.ToolId);
        }
        public void Remove(string id) {
            _repositoryContext.Tools.DeleteOne(tool=>tool.ToolId==id);
        }
    }
}