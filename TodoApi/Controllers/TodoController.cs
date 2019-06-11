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
    [ApiController]
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

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            context.TodoItems.Add(todoItem);
            int id = await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { id }, todoItem);
        }

    }
}
