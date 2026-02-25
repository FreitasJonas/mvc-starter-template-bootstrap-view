using System.ComponentModel.DataAnnotations;
using mvc.starter.template.Domain.Entities;
using mvc.starter.template.Web.Validation;

namespace mvc.starter.template.Web.ViewModels
{
    public class UsuarioFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name must be between 3 and 50 characters long.")]
        public string Name { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Last Name must be between 3 and 50 characters long.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Login is required")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "The Login must be between 3 and 15 characters long.")]
        public string Login { get; set; }

        
        [RequiredIf("Id", 0, ErrorMessage = "Password is required")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "The Login must be between 5 and 15 characters long.")]
        public string? Password { get; set; }

        [RequiredIf("Id", 0, ErrorMessage = "Password Confirmation is required")]
        [Display(Name = "Password Confirmation")]
        [PasswordConfirmation("Password", ErrorMessage = "Password confirmation is required and must match the password.")]
        public string? PasswordConfirmation { get; set; }


        [Required(ErrorMessage = "E-mail is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "E-mail invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "The Login must be between 8 and 11 characters long.")]
        public string Telephone { get; set; }

        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Date of birth is required")]
        public string DtBirth { get; set; }

        [Display(Name = "Is Active")]
        public bool Status { get; set; }


        [Required(ErrorMessage = "Role is required")]
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        public IEnumerable<Role>? Roles { get; set; }
    }
}
