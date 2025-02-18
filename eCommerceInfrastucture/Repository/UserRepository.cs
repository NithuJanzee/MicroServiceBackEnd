using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerceInfrastucture.DbContext;

namespace eCommerceInfrastucture.Repository
{
    internal class UserRepository : IusersRepository
    {
        private readonly DapperDbContext _dbContext;

        public UserRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser?> AddUser(ApplicationUser user)
        {
            user.UserID = Guid.NewGuid();

            // sql querry insert into user data into the table "Users" tabele
            string querry = "INSERT INTO public.\"Users\" (\"UserID\", \"Email\", \"PersonName\", \"Gender\", \"Password\") " +
                "VALUES (@UserID, @Email, @PersonName, @Gender, @Password);";

            int NumberOfRowAffected = await _dbContext.DbConnection.ExecuteAsync(querry, user);
            if (NumberOfRowAffected > 0)
            {
                return user;

            }
            return null;
        }

        public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
        {
            // sql querry to get user data from the table "Users" tabele

            //string querry = "select * from public.\"Users\" where \"Email\" = @Email and \"Password\" = @Password";
            string query = "SELECT * FROM public.\"Users\" WHERE \"Email\" = @Email AND \"Password\" = @Password";
            var parameter = new { Email = email, Password = password };
            ApplicationUser? user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query,parameter );
            return user;

            //return new ApplicationUser()
            //{
            //    UserID = Guid.NewGuid(),
            //    Email = email,
            //    Password = password,
            //    PersonName = "Test User",
            //    Gender = GenderOptions.Male.ToString()
            //};
        }
    }
}
