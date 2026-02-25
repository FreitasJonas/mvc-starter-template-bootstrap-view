using System.ComponentModel.DataAnnotations;

namespace mvc.starter.template.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
