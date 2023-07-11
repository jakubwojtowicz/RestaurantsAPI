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
        public AccountService(RestaurantDbContext context)
        {
            this.context = context;
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

            context.Users.Add(newUser); 
            context.SaveChanges();
        }
    }
}
