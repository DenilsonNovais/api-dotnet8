using ApiDotNet8.Application.DTOs.Todo;
using ApiDotNet8.Domain.Entities;
using ApiDotNet8.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet8.Controllers;

[ApiController]
[Route("todos")]
public class TodosController : ControllerBase
{
    private readonly AppDbContext _context;

    public TodosController(AppDbContext context)
    {
        _context = context;
    }

    // POST /todos
    [HttpPost]
    public async Task<ActionResult<TodoResponse>> Create(CreateTodoRequest request)
    {
        var todo = new Todo(request.Title);
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = todo.Id },
            new TodoResponse(todo.Id, todo.Title, todo.Completed));
    }

    // GET /todos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoResponse>>> GetAll()
    {
        var todos = await _context.Todos
            .AsNoTracking()
            .Select(t => new TodoResponse(t.Id, t.Title, t.Completed))
            .ToListAsync();

        return Ok(todos);
    }

    // GET /todos/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TodoResponse>> GetById(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo is null) return NotFound();

        return Ok(new TodoResponse(todo.Id, todo.Title, todo.Completed));
    }

    // PUT /todos/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Complete(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo is null) return NotFound();

        todo.Complete();
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /todos/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo is null) return NotFound();

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
