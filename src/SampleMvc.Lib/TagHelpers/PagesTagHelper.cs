using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace SampleMvc.Lib.TagHelpers
{
    [HtmlTargetElement("pagination")]
    public class PagesTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-pages")]
        public int TotalPage { get; set; }

        [HtmlAttributeName("asp-current")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// 페이징 영역에 표시할 최대 페이지 수
        /// </summary>
        [HtmlAttributeName("asp-display")]
        public int DisplayPageCount { get; set; } = 10;

        public IDictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();

        [HtmlAttributeName("asp-model")]
        public IPagedList Source { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Source != null)
            {
                var startPage = GetStartPage();
                var processPage = 0;

                output.TagName = "div";
                output.TagMode = TagMode.StartTagAndEndTag;
                StringBuilder html = new StringBuilder();
                html.Append("<nav aria-label=\"Page navigation\"");
                html.Append("<ul class=\"pagination\">");

                // navigate Previous page
                html.Append($"<li{(startPage == 1 ? " class=\"disabled\"" : "")}>");
                html.Append($"<a href=\"{ (startPage == 1 ? "#" : GetQueryString( startPage - 1)) }\">");
                
                html.Append("Prev");
                html.Append("</a>");
                html.Append("</li>");

                for (int i = 0; i < DisplayPageCount; i++)
                {
                    processPage = (i + startPage);

                    html.Append($"<li{(Source.CurrentPage == processPage ? " class=\"active\"" : "")}>");
                    html.Append($"<a href=\"{(Source.CurrentPage == processPage ? "#" : GetQueryString(processPage))}\">");
                    html.Append($"{processPage}");
                    html.Append("</a>");
                    html.Append("</li>");

                    if (processPage >= Source.TotalPages) { break; }
                }

                // navigate next page
                html.Append($"<li{(processPage == Source.TotalPages ? " class=\"disabled\"" : "")}>");
                html.Append($"<a href=\"{ (processPage == Source.TotalPages ? "#" : GetQueryString( processPage + 1)) }\">");
                html.Append("Next");
                html.Append("</a>");
                html.Append("</li>");

                html.Append("</ul>");
                html.Append("</nav>");

                output.Content.SetHtmlContent(html.ToString());
            }
        }

        private int GetStartPage()
        {
            if(Source != null)
            {
                return (((int)(Source.CurrentPage - 1) / DisplayPageCount) * DisplayPageCount) + 1 ;
            }

            return 1;
        }

        private string GetQueryString(int page)
        {
            var queryString = new StringBuilder();
            queryString.Append($"?page={page}");
            if (Filters != null && Filters.Count > 0)
            {
                foreach (var item in Filters)
                {
                    if (!String.IsNullOrEmpty(item.Value))
                    {
                        queryString.Append($"&{item.Key}={Uri.EscapeUriString(item.Value)}");
                    }
                }
            }

            return queryString.ToString();
        }
    } 
}
