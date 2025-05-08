using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IJwtService _jwtService;

        public UserService(IMapper _mapper,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IRepository<User> _userRepository, 
            IRepository<RefreshToken> _refreshTokenRepository, 
            IJwtService _jwtService)
        {
            this._mapper = _mapper;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._userRepository = _userRepository;
            this._refreshTokenRepository = _refreshTokenRepository;
            this._jwtService = _jwtService;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAll();
                return _mapper.Map<IEnumerable<UserDto>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting users", ex);
            }
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                var user = await _userRepository.GetById(id);

                if (user == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting user by id", ex);
            }
        }

        public async Task RegisterAdminAsync(RegisterAdminDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                    throw new HttpException("Email is already exists.", HttpStatusCode.BadRequest);

                var newUser = _mapper.Map<User>(model);
                newUser.UserType = (int)UserType.Admin;

                var res = await _userManager.CreateAsync(newUser, model.Password);

                if (!res.Succeeded)
                    throw new HttpException(string.Join(" ", res.Errors.Select(x => x.Description)), HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Error register admin", ex);
            }
        }

        public async Task RegisterClientAsync(RegisterDefaultUserDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                    throw new HttpException("Email is already exists.", HttpStatusCode.BadRequest);

                var newUser = _mapper.Map<User>(model);
                newUser.UserType = (int)UserType.Client;

                var res = await _userManager.CreateAsync(newUser, model.Password);

                if (!res.Succeeded)
                    throw new HttpException(string.Join(" ", res.Errors.Select(x => x.Description)), HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Error register client", ex);
            }
        }

        public async Task RegisterSpecialistAsync(RegisterDefaultUserDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                    throw new HttpException("Email is already exists.", HttpStatusCode.BadRequest);

                var newUser = _mapper.Map<User>(model);
                newUser.UserType = (int)UserType.Specialist;

                var res = await _userManager.CreateAsync(newUser, model.Password);

                if (!res.Succeeded)
                    throw new HttpException(string.Join(" ", res.Errors.Select(x => x.Description)), HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Error register specialist", ex);
            }
        }

        public async Task UpdateUserAsync(UserDto model)
        {
            try
            {
                var userToInsert = _mapper.Map<User>(model);
                _userRepository.Update(userToInsert);
                await _userRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user", ex);
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            if (await GetUserByIdAsync(id) == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

            try
            {
                _userRepository.Delete(id);
                await _userRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user", ex);
            }
        }

        public async Task<LoginResponseDto> LoginViaEmailAsync(LoginViaEmailDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Invalid user email or password.", HttpStatusCode.BadRequest);

            return new LoginResponseDto
            {
                AccessToken = _jwtService.CreateToken(_jwtService.GetClaims(user)),
                RefreshToken = CreateRefreshToken(user.Id).Token
            };
        }

        public async Task<LoginResponseDto> LoginViaUserNameAsync(LoginViaUserNameDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Invalid user name or password.", HttpStatusCode.BadRequest);

            return new LoginResponseDto
            {
                AccessToken = _jwtService.CreateToken(_jwtService.GetClaims(user)),
                RefreshToken = CreateRefreshToken(user.Id).Token
            };
        }

        public async Task Logout(string refreshToken) => await _signInManager.SignOutAsync();

        private RefreshToken CreateRefreshToken(string userId)
        {
            var refeshToken = _jwtService.CreateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refeshToken,
                UserId = userId,
                CreationDate = DateTime.UtcNow
            };

            _refreshTokenRepository.Insert(refreshTokenEntity);
            _refreshTokenRepository.Save();

            return refreshTokenEntity;
        }
    }
}
