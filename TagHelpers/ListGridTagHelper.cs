using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PlantsShop.TagHelpers
{
    public class ListGridTagHelper: TagHelper
    {    
        public bool isList { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            
            if (isList == true)
                output.Attributes.SetAttribute("class", "list left padding-top direction-column");
            else
                output.Attributes.SetAttribute("class", "list padding-top direction-row");
                        
        }
    }
}
