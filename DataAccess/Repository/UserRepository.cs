using System.Collections.Generic;
using Application.Contracts.Response;
using Application.IRepository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Request;

namespace DataAccess.Repository;

public sealed class UserRepository(IConfiguration configuration) : IUserRepository
{
    private string? ConnectionString { get; } = configuration.GetConnectionString("DefaultConnection");
    private SqlConnection CreateConnection() => new(ConnectionString);

    public async Task<List<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = CreateConnection();
        var users = await connection.QueryAsync<UserResponse>(SqlCommands.GetAllUsers, cancellationToken);
        return users.ToList();
    }

    public async Task<UserResponse?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<UserResponse>(SqlCommands.GetUserById, new { Id = id });
    }

    public async Task<UserResponse> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        await using var connection = CreateConnection();
      
        var id = await connection.ExecuteScalarAsync<int>(SqlCommands.CreateUser, request);

        return new UserResponse(id, request.Name, request.Email, request.Birthday);
    }

    public async Task<bool> UpdatedUserAsync(int id, UpdateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        await using var connection = CreateConnection();
       
        var parameters = new { Id = id, request.Name, request.Email };
        
        var rowsAffected = await connection.ExecuteAsync(SqlCommands.UpdateUser, parameters);
        return rowsAffected > 0;
    }
    
    public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(SqlCommands.DeleteUser, new { Id = id });
        return rowsAffected > 0;
    }
}