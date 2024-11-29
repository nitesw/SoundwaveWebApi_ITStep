using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAccountsService
    {
        Task Register(RegisterDto model);
        Task<LoginResponse> Login(LoginDto model);
        Task Logout();
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> Get(string id);
        Task ChangeRole(string id, string role);
    }
}
