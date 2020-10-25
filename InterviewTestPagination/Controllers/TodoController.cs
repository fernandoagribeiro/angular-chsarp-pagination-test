using InterviewTestPagination.Models;
using InterviewTestPagination.Models.Page;
using InterviewTestPagination.Models.Todo;
using System.Web.Http;

namespace InterviewTestPagination.Controllers
{
    /// <summary>
    /// 'Rest' controller for the <see cref="Todo"/>
    /// model.
    /// 
    /// TODO: implement the pagination Action
    /// </summary>
    public class TodoController : ApiController {

        // TODO: [low priority] setup DI 
        private readonly IModelService<Todo> _todoService = new TodoService();

        [HttpGet]
        public Page<Todo> Todos(int currentPage, int pageSize, string orderBy, string sortDirection = "asc") {
            var originalCollection = _todoService.List(orderBy, sortDirection);

            var page = new Page<Todo>(currentPage, pageSize, originalCollection);

            return page;
        }
    }
}
