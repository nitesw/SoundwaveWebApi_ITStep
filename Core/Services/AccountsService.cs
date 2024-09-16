using AutoMapper;
using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<User> userManager;
        private readonly IRepository<User> userRepo;
        private readonly IMapper mapper;

        public AccountsService(UserManager<User> userManager, IRepository<User> userRepo, IMapper _mapper)
        {
            this.userManager = userManager;
            this.userRepo = userRepo;
            mapper = _mapper;
        }

        public async Task Register(RegisterDto model)
        {
            var isExistsByEmail = await userManager.FindByEmailAsync(model.Email);
            if (isExistsByEmail != null)
            {
                throw new HttpException($"Email {model.Email} is already in use.", HttpStatusCode.BadRequest);
            }
            var isExistsByUsername = await userManager.FindByNameAsync(model.UserName);
            if (isExistsByUsername != null)
            {
                throw new HttpException($"Username {model.UserName} is already in use.", HttpStatusCode.BadRequest);
            }

            var user = new User()
            {
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                throw new HttpException(error.Description, HttpStatusCode.BadRequest);
            }
        }

        public async Task Login(LoginDto model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Invalid login or password.", HttpStatusCode.BadRequest);
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await userRepo.GetAll();

            return mapper.Map<List<UserDto>>(users);
        }
    }
}
