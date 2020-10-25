using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTestPagination.Models.Todo {

    /// <summary>
    /// No need to use an actual persistent datasource. 
    /// All operations can be mocked in-memory as long as they are consistent with the chosen datasource implementation 
    /// (e.g. dont create new model instances when executing a search 'query', etc).
    /// TL;DR: from this point on Database-like operations can be mocked.
    /// </summary>
    public class TodoRepository : IModelRepository<Todo> {

        /// <summary>
        /// Example in-memory model datasource 'indexed' by id.
        /// </summary>
        private static readonly IDictionary<long, Todo> DataSource = new ConcurrentDictionary<long, Todo>();

        static TodoRepository() {
            // initializing datasource
            var startDate = DateTime.Today;
            for (var i = 1; i <= 55; i++) {
                var createdDate = startDate.AddDays(i);
                DataSource[i] = new Todo(id: i, task: "Dont forget to do " + i, createdDate: createdDate);
            }
        }

        public IEnumerable<Todo> All() {
            return DataSource.Values.OrderByDescending(t => t.CreatedDate);
        }

        public IEnumerable<Todo> List(string orderBy, string sortDirection)
        {
            IOrderedEnumerable<Todo> orderedList;

            switch (orderBy)
            {
                case "Created Date":
                    if(sortDirection == "asc")
                        orderedList = DataSource.Select(x => x.Value).OrderBy(x => x.CreatedDate);
                    else
                        orderedList = DataSource.Select(x => x.Value).OrderByDescending(x => x.CreatedDate);
                    break;
                case "Task":
                    if (sortDirection == "asc")
                        orderedList = DataSource.Select(x => x.Value).OrderBy(x => x.Task);
                    else
                        orderedList = DataSource.Select(x => x.Value).OrderByDescending(x => x.Task);
                    break;
                default:
                    if (sortDirection == "asc")
                        orderedList = DataSource.Select(x => x.Value).OrderBy(x => x.Id);
                    else
                        orderedList = DataSource.Select(x => x.Value).OrderByDescending(x => x.Id);
                    break;
            }

            return orderedList.ToList();
        }
    }
}
