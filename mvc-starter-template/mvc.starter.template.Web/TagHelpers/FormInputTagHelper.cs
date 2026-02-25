using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using mvc.starter.template.Web.Validation;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace mvc.starter.template.Web.TagHelpers
{
    [HtmlTargetElement("form-input", Attributes = "asp-for")]
    public class FormInputTagHelper : TagHelper
    {
        private readonly IHtmlGenerator _generator;

        public FormInputTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("type")]
        public string Type { get; set; } = "text";

        [HtmlAttributeName("mask")]
        public string Mask { get; set; }

        [HtmlAttributeName("placeholder")]
        public string Placeholder { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Clear();
            output.Content.Clear();

            var property = For.Metadata.ContainerType
                .GetProperty(For.Metadata.PropertyName);

            // Wrapper
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "mb-3");

            // Label (oficial)
            var label = _generator.GenerateLabel(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                labelText: null,
                htmlAttributes: new { });



            var inputAttributes = new Dictionary<string, object>
            {
                { "class", "form-control" },
                { "type", Type }
            };

            // Só adiciona máscara se realmente existir
            if (!string.IsNullOrWhiteSpace(Mask))
            {
                inputAttributes.Add("data-mask", Mask);
            }

            // Só adiciona se realmente existir
            if (!string.IsNullOrWhiteSpace(Placeholder))
            {
                inputAttributes.Add("placeholder", Placeholder);
            }

            // Input (oficial)
            var input = _generator.GenerateTextBox(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                value: For.Model,
                format: null,
                htmlAttributes: inputAttributes);

            ApplyValidationAttributes(input, property, ViewContext.ViewData.Model);

            // VALIDAÇÃO OFICIAL
            var validation = _generator.GenerateValidationMessage(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                message: null,
                tag: null,
                htmlAttributes: new { @class = "text-danger" });

            output.Content.AppendHtml(label);
            output.Content.AppendHtml(input);
            output.Content.AppendHtml(validation);
        }

        private static void ApplyValidationAttributes(TagBuilder input, PropertyInfo property, object model)
        {
            foreach (var attr in property.GetCustomAttributes(true))
            {
                if (attr is RequiredIfAttribute reqIf)
                {
                    var dependentProp = model?.GetType()
                        .GetProperty(reqIf.PropertyName);

                    var dependentValue = dependentProp?.GetValue(model);

                    // SÓ gera data-required se a condição for verdadeira
                    if (Equals(dependentValue, reqIf.ExpectedValue))
                    {
                        input.Attributes["data-required"] = "true";
                        input.Attributes["data-msg"] =
                            reqIf.ErrorMessage ?? "Campo obrigatório";
                    }

                    continue;
                }

                if (attr is RequiredAttribute req)
                {
                    input.Attributes["data-required"] = "true";
                    input.Attributes["data-msg"] =
                        req.ErrorMessage ?? "Campo obrigatório";
                }

                if (attr is StringLengthAttribute str)
                {
                    input.Attributes["data-min"] = str.MinimumLength.ToString();
                    input.Attributes["data-max"] = str.MaximumLength.ToString();
                    input.Attributes["data-msg"] =
                        str.ErrorMessage ?? "Tamanho inválido";
                }

                if (attr is RegularExpressionAttribute reg)
                {
                    input.Attributes["data-regex"] = reg.Pattern;
                    input.Attributes["data-msg"] =
                        reg.ErrorMessage ?? "Formato inválido";
                }
            }
        }

    }
}
