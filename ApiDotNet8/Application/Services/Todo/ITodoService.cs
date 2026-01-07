using ApiDotNet8.Application.DTOs.Todo;

namespace ApiDotNet8.Application.Services.Todo;

public interface ITodoService
{
    Task<TodoResponse> CreateAsync(CreateTodoRequest request);
    Task<IEnumerable<TodoResponse>> GetAllAsync();
    Task<TodoResponse?> GetByIdAsync(Guid id);
    Task<bool> CompleteAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);

}