using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Request;
using Application.Contracts.Response;

namespace Application.IRepository;

public interface IUserRepository
{
    Task<List<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<UserResponse?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<UserResponse> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdatedUserAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken = default);
}