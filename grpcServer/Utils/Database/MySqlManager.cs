using System.Data;
using System.Data.Common;
using grpcServer.Utils.PasswordHashing;
using MySql.Data.MySqlClient;

namespace grpcServer.Utils.Database
{
    public class MySqlManager
    {
        private static MySqlManager _instance = new MySqlManager();
        private MySqlConnection connection;
        public string mySqlConnectionString = "server=localhost;user=nihal;password=password;database=grpc_server";

        public MySqlManager()
        {
            connection = new MySqlConnection(mySqlConnectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static MySqlManager GetInstance()
        {
            return _instance;
        }

        public async Task<APIResponse> CreateUser(UserModel user, string salt)
        {
            APIResponse response;
            try
            {
                string sql = @"INSERT INTO `tbl_user`(`user_name`,`user_password`, `user_salt`) 
                            VALUES (@username, @password, @salt)";
                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@salt", salt);
                    cmd.ExecuteNonQuery();
                    response = new APIResponse { ResponseCode = 201, ResponseMessage = "Success" };
                }
            }
            catch (MySqlException e)
            {
                if (e.Number == 1062)
                {
                    response = new APIResponse { ResponseCode = 409, ResponseMessage = "User already exists" };
                }
                else
                {
                    response = new APIResponse { ResponseCode = 500, ResponseMessage = "Internal Server Error" };
                }
            }
            return response;
        }

        public async Task<APIResponse> VerifyUser(string userName, string password)
        {
            APIResponse response;
            try
            {
                string sql = $"SELECT user_name, user_password, user_salt FROM tbl_user WHERE user_name = '{userName}'";
                var table = new DataTable();
                var adapter = new MySqlDataAdapter(sql, connection);
                await adapter.FillAsync(table);
                var salt = Convert.FromBase64String(table.Rows[0]["user_salt"].ToString());
                var userPassword = table.Rows[0]["user_password"].ToString();
                PasswordService passwordService = new(Sha256Hasher.getInstance());
                password = passwordService.HashPassword(password, salt);
                if (password.Equals(userPassword))
                {
                    response = new APIResponse { ResponseCode = 200, ResponseMessage = "Success" };
                }
                else
                {
                    response = new APIResponse { ResponseCode = 401, ResponseMessage = "Invalid Credentials" };
                }
            }
            catch (MySqlException e)
            {
                response = new APIResponse { ResponseCode = 500, ResponseMessage = "Something went wrong, Please try again." };
            }
            catch (IndexOutOfRangeException e)
            {
                response = new APIResponse { ResponseCode = 404, ResponseMessage = "User not found" };
            }
            return response;
        }
    }
}