using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;
internal class UserService : IUserService
{
    private readonly IusersRepository _UserRepo;
    private readonly IMapper _mapper;
    public UserService(IusersRepository userRepo , IMapper mapper)
    {
        _UserRepo = userRepo;
        _mapper = mapper;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequestDTO loginRequest)
    {
        ApplicationUser? User = await _UserRepo.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);
        if (User == null)
        {
            return null;
        }

        // return new AuthenticationResponse(User.UserID, User.Email, User.PersonName, User.Gender, "token", success: true);
        return _mapper.Map<AuthenticationResponse>(User);
        //var authResponse = _mapper.Map<AuthenticationResponse>(User);
        //return authResponse with { success = true, Token = "token" };


    }

    public async Task<AuthenticationResponse?> Register(RegistorRequestDTO registor)
    {
        //ApplicationUser User = new ApplicationUser()
        //{
        //    Email = registor.Email,
        //    Password = registor.Password,
        //    PersonName = registor.PersonName,
        //    Gender = registor.Gender.ToString()
        //};

        var mapuser = _mapper.Map<ApplicationUser>(registor);
        ApplicationUser? RegistorUser = await _UserRepo.AddUser(mapuser);

        if (RegistorUser == null)
        {
            return null;
        }

        //return new AuthenticationResponse(
        //        UserID: RegistorUser.UserID,
        //        Email: RegistorUser.Email,
        //        PersonName: RegistorUser.PersonName,
        //        Gender: RegistorUser.Gender,
        //        Token: "Token",
        //        success: true
        //);

        return _mapper.Map<AuthenticationResponse>(RegistorUser);
    }
}

