using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
namespace learningGate.Helpers
{

  [HtmlTargetElement("a", Attributes = "asp-active-link")]
  public class ActiveLinkTagHelper : TagHelper
  {
    [HtmlAttributeName("asp-active-link")]
    public bool IsActive { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (IsActive)
      {
        output.AddClass("active", HtmlEncoder.Default);
      }
    }
  }
}
