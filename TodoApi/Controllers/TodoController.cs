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
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            context.Entry(todoItem).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(int id)
        {
            TodoItem todoItem = await context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
            if (todoItem is null)
            {
                return NotFound();
            }

            context.TodoItems.Remove(todoItem);

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
