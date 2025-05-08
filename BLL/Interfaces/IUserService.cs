using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id);
        Task RegisterAdminAsync(RegisterAdminDto user);
        Task RegisterClientAsync(RegisterDefaultUserDto user);
        Task RegisterSpecialistAsync(RegisterDefaultUserDto user);
        Task UpdateUserAsync(UserDto user);
        Task DeleteUserAsync(string id);
        Task<LoginResponseDto> LoginViaEmailAsync(LoginViaEmailDto model);
        Task<LoginResponseDto> LoginViaUserNameAsync(LoginViaUserNameDto model);
        Task Logout(string refreshToken);
    }
}
