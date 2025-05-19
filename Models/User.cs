using SQLite;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }
}
