using System.Collections;
using System.Linq;
using Grpc.Core;
using grpcServer;
using grpcServer.Utils.Database;
using grpcServer.Utils.PasswordHashing;
using MySql.Data.MySqlClient;

namespace grpcServer.Services;
public class UserService : User.UserBase
{
    public List<UserModel> users = new();
    private readonly ILogger<UserService> _logger;
    public UserService(ILogger<UserService> logger)
    {
        _logger = logger;
    }
    public override Task<APIResponse> CreateUser(UserModel user, ServerCallContext context)
    {
        PasswordService passwordService = new(Sha256Hasher.getInstance());
        user.Password = passwordService.HashPassword(user.Password);
        var salt = Convert.ToBase64String(passwordService.salt);
        var mySqlManager = MySqlManager.GetInstance();
        var response = mySqlManager.CreateUser(user, salt);
        return response;
    }

    public override Task<APIResponse> Login(LoginRequest request, ServerCallContext context)
    {
        string userName = request.Username;
        string password = request.Password;
        var mySqlManager = MySqlManager.GetInstance();
        var response = mySqlManager.VerifyUser(userName, password);
        return response;
    }
}