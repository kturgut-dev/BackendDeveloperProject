using BackendDeveloperProject.Core.Helpers.Security;
using BackendDeveloperProject.Core.Services.Concrete;
using BackendDeveloperProject.DataAccess.Abstract;
using BackendDeveloperProject.Entities.Concrete;
using BackendDeveloperProject.Entities.DataTransferObjects.Request;
using BackendDeveloperProject.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BackendDeveloperProject.Services.Concrate
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContext;
        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContext) : base(userRepository)
        {
            _userRepository = userRepository;
            _httpContext = httpContext;
        }

        public async Task<Result> Login(UserLoginForm formData, CancellationToken cancellationToken = default)
        {
            Result result = new();

            User? user = await _userRepository.GetAsync(x => x.Email == formData.Email);

            if (user == null)
            {
                result.SetError("Kullanıcı bulunamadı. Lütfen bilgilerinizi kontrol ediniz.");
                return result;
            }

            // Password hash control
            SecurePasswordHasher securePasswordHasher = new SecurePasswordHasher();
            securePasswordHasher.UserSalt = user!.PasswordSalt;
            bool passHashControl = securePasswordHasher.Verify(formData!.Password, user!.PasswordHash);

            if (!passHashControl)
            {
                result.SetError("Şifre hatalı. Lütfen tekrar deneyiniz.");
                return result;
            }


            List<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties authProperties = new AuthenticationProperties
            {
                IsPersistent = formData.RememberMe
            };

            await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            result.IsSuccess = true;
            result.Message = "Oturumunuz başarıyla açıldı.";

            return result;
        }

        public async Task<Result> Logout()
        {
            Result result = new();

            await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            result.IsSuccess = true;
            result.Message = "Oturumunuz başarıyla kapatıldı.";
            return result;
        }
    }
}
