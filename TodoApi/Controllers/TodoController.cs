using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly TodoContext context;

        public TodoController(TodoContext todoContext)
        {
            context = todoContext;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await context.TodoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            TodoItem item = await context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
            return item is null ? (ActionResult)NotFound() : Ok(item);
        }


    }
}
