using ApiDotNet8.Application.DTOs.Todo;
using ApiDotNet8.Domain.Entities;
using ApiDotNet8.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet8.Application.Services.Todo;

public class TodoService : ITodoService
{
    private readonly AppDbContext _context;

    public TodoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TodoResponse> CreateAsync(CreateTodoRequest request)
    {
        var todo = new Domain.Entities.Todo(request.Title);
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return new TodoResponse(todo.Id, todo.Title, todo.Completed);
    }

    public async Task<IEnumerable<TodoResponse>> GetAllAsync()
    {
        return await _context.Todos
            .AsNoTracking()
            .Select(t => new TodoResponse(t.Id, t.Title, t.Completed))
            .ToListAsync();
    }

    public async Task<TodoResponse?> GetByIdAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        return todo is null
            ? null
            : new TodoResponse(todo.Id, todo.Title, todo.Completed);
    }

    public async Task<bool> CompleteAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo is null) return false;

        todo.Complete();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo is null) return false;

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        return true;
    }
}
