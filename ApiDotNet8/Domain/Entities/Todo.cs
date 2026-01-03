using System.Net.NetworkInformation;

namespace ApiDotNet8.Domain.Entities;

public class Todo
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public bool Completed { get; private set; }

    protected Todo() { }

    public Todo(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
        Completed = false;
    }

    public void Complete() => Completed = true;
}