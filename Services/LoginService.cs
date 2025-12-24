using Files.Models.Interfaces;
using Files.Models.Request;
using Files.Models.Response;

namespace Files.Services
{
    public class LoginService
    {
        private readonly IAccountRepository _repo;

        public LoginService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var account = _repo.Login(request.UserName!, request.Password!);

            if (account == null)
            {
                return new LoginResponse
                {
                    Message = "Sai tài khoản hoặc mật khẩu"
                };
            }

            return new LoginResponse
            {
                Code = account.Code,
                UserName = account.UserName,
                Role = account.Role,
                Message = "Đăng nhập thành công"
            };
        }
    }
}
