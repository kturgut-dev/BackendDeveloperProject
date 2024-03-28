using System.ComponentModel.DataAnnotations;

namespace BackendDeveloperProject.Entities.DataTransferObjects.Request
{
    public class UserLoginForm
    {
        [Required(ErrorMessage = "Lütfen E-Posta adresinizi giriniz.")]
        [Display(Name = "E-Posta Adresi")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir E-Posta adresi giriniz.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [Display(Name = "Şifre")]
        public string? Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
