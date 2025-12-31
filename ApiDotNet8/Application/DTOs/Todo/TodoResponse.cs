namespace ApiDotNet8.Application.DTOs.Todo;

public record TodoResponse(Guid Id, string Title, bool Completed);
