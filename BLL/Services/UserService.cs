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
        private readonly IRepository<User> _userRepository;

        public UserService(IMapper _mapper, UserManager<User> _userManager, IRepository<User> _userRepository)
        {
            this._mapper = _mapper;
            this._userManager = _userManager;
            this._userRepository = _userRepository;
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

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            try
            {
                if (id < 0) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                var user = await _userRepository.GetById(id);

                if (user == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting user by id", ex);
            }
        }

        public async Task AddUserAsync(AddUserDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                    throw new HttpException("Email is already exists.", HttpStatusCode.BadRequest);

                var newUser = _mapper.Map<User>(model);

                var res = await _userManager.CreateAsync(newUser, model.Password);

                if (!res.Succeeded)
                    throw new HttpException(string.Join(" ", res.Errors.Select(x => x.Description)), HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user", ex);
            }
        }

        public async Task UpdateUserAsync(UserDto client)
        {
            try
            {
                var userToInsert = _mapper.Map<User>(client);
                _userRepository.Update(userToInsert);
                await _userRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user", ex);
            }
        }

        public async Task DeleteUserAsync(int id)
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
    }
}
