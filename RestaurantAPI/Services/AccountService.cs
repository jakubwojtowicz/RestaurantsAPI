using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto dto);
    }
    public class AccountService: IAccountService
    {
        private readonly RestaurantDbContext context;
        private readonly IPasswordHasher<User> hasher;

        public AccountService(RestaurantDbContext context, IPasswordHasher<User> hasher)
        {
            this.context = context;
            this.hasher = hasher;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };
            var hashedPassword = hasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;

            context.Users.Add(newUser); 
            context.SaveChanges();
        }
    }
}
