using HeyRed.MarkdownSharp;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SampleMvc.TagHelpers
{
    [HtmlTargetElement("markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        [HtmlAttributeName("text")]
        public string Text { get; set; }

        [HtmlAttributeName("source")]
        public ModelExpression Source { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //base.Process(context, output);

            var processor = new Markdown(new MarkdownOptions {
                AllowTargetBlank = true,
                AutoHyperlink = false,
                AutoNewLines = true,                
            });

            if(Source != null)
            {
                Text = Source.Model?.ToString();
            }
            
            var result = processor.Transform(Text);

            output.TagName = "div";
            output.Attributes.Add("class", "markdown");
            output.Content.SetHtmlContent(result);
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
