using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace mvc.starter.template.Web.TagHelpers
{
    [HtmlTargetElement("form-checkbox", Attributes = "asp-for")]
    public class FormCheckboxTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Clear();
            output.Content.Clear();

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "form-check mb-3");

            // Hidden para false (padrão MVC)
            var hidden = new TagBuilder("input");
            hidden.Attributes["type"] = "hidden";
            hidden.Attributes["name"] = For.Name;
            hidden.Attributes["value"] = "false";

            // Checkbox real
            var checkbox = new TagBuilder("input");
            checkbox.AddCssClass("form-check-input");
            checkbox.Attributes["type"] = "checkbox";
            checkbox.Attributes["name"] = For.Name;
            checkbox.Attributes["id"] = For.Name;
            checkbox.Attributes["value"] = "true";

            if (For.Model is bool b && b)
                checkbox.Attributes["checked"] = "checked";

            // Label
            var label = new TagBuilder("label");
            label.AddCssClass("form-check-label");
            label.Attributes["for"] = For.Name;
            label.InnerHtml.Append(For.Metadata.DisplayName ?? For.Name);

            // Render
            output.Content.AppendHtml(hidden);
            output.Content.AppendHtml(checkbox);
            output.Content.AppendHtml(label);
        }
    }
}
