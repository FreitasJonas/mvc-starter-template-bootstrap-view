namespace mvc.starter.template.Web.ViewModels
{
    public class LogIndexViewModel
    {
        public DateTime? Date { get; set; }

        public string? LogContent { get; set; }

        public int TotalLines { get; set; }

        public bool FileExists { get; set; }
    }
}
