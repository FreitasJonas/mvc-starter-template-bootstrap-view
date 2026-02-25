using System.ComponentModel.DataAnnotations;

namespace mvc.starter.template.Web.ViewModels
{
    public class RoleFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name id required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name must be between 3 and 50 characters long.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Description must be between 3 and 50 characters long.")]
        public string? Description { get; set; }

        public List<PermissionTreeNodeViewModel> PermissionTree { get; set; } = new();
    }

    public class PermissionTreeNodeViewModel
    {
        public string Key { get; set; }          
        public string Label { get; set; }        
        public bool Checked { get; set; }
        public string Description { get; set; }

        public List<PermissionTreeNodeViewModel> Children { get; set; } = new();
        
    }

}
