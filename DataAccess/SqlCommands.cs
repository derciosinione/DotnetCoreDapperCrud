namespace DataAccess;

public static class SqlCommands
{
    public const string GetAllUsers = "SELECT Id, Name, Email, Birthday FROM Users";
    public const string GetUserById = "SELECT Id, Name, Email, Birthday FROM Users WHERE Id = @Id";
    public const string DeleteUser = "DELETE FROM Users WHERE Id = @Id";
   
    public const string CreateUser = """
                                         INSERT INTO Users (Name, Email, Birthday)
                                         VALUES (@Name, @Email, @Birthday);
                                         SELECT CAST(SCOPE_IDENTITY() as int);
                                     """;
    
    public const string UpdateUser = """
                                         UPDATE Users
                                         SET Name = @Name, Email = @Email
                                         WHERE Id = @Id
                                     """;
}