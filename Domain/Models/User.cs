namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime Created { get; set; }
}
