using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace mvc.starter.template.Web.TagHelpers
{
    [HtmlTargetElement("form-switch", Attributes = "asp-for")]
    public class FormSwitchTagHelper : TagHelper
    {
        private readonly IHtmlGenerator _generator;

        public FormSwitchTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Clear();
            output.Content.Clear();

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "form-check form-switch mb-3");

            var checkbox = _generator.GenerateCheckBox(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                isChecked: (For.Model as bool?) == true,
                htmlAttributes: new { @class = "form-check-input" }
            );

            var label = _generator.GenerateLabel(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                labelText: For.Metadata.DisplayName,
                htmlAttributes: new { @class = "form-check-label" }
            );

            output.Content.AppendHtml(checkbox);
            output.Content.AppendHtml(label);
        }
    }
}
