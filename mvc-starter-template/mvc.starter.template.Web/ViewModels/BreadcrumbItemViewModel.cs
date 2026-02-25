namespace mvc.starter.template.Web.ViewModels
{
    public class BreadcrumbItemViewModel
    {
        public string Text { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public bool IsActive { get; set; }
    }

    public class IndexSubheaderViewModel
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public IEnumerable<BreadcrumbItemViewModel> Breadcrumb { get; set; }

        public string ActionText { get; set; }
        public string ActionUrl { get; set; }
    }
}
