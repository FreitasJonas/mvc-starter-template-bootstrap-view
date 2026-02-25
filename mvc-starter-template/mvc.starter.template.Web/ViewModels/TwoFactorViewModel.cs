using System.ComponentModel.DataAnnotations;

namespace mvc.starter.template.Web.ViewModels
{
    public class TwoFactorViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Enter the code")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Code invalid")]
        public string Code { get; set; }
    }
}
