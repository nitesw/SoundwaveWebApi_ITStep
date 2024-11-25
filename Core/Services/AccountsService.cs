using AutoMapper;
using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountsService(
        UserManager<User> userManager,
        IRepository<User> userRepo,
        IMapper mapper,
        IJwtService jwtService
            ) : IAccountsService
    {
        private readonly UserManager<User> userManager = userManager;
        private readonly IRepository<User> userRepo = userRepo;
        private readonly IMapper mapper = mapper;
        private readonly IJwtService jwtService = jwtService;

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

            var user = mapper.Map<User>(model);

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                throw new HttpException(error.Description, HttpStatusCode.BadRequest);
            } 
            else
            {
                await userManager.AddToRoleAsync(user, Roles.USER);
            }
        }

        public async Task<LoginResponse> Login(LoginDto model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Invalid login or password.", HttpStatusCode.BadRequest);

            return new LoginResponse
            {
                Token = jwtService.CreateToken(jwtService.GetClaims(user))
            };
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await userRepo.GetListBySpec(new UserSpecification.All());

            return mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> Get(string id)
        {
            var user = await userRepo.GetItemBySpec(new UserSpecification.GetUser(id));

            return mapper.Map<UserDto>(user);
        }
    }
}
