#pragma checksum "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "753c3b8688adec02785d00179b62b0375c371783"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ForGoodTime.Views.Product.Views_Product_Index), @"mvc.1.0.view", @"/Views/Product/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Product/Index.cshtml", typeof(ForGoodTime.Views.Product.Views_Product_Index))]
namespace ForGoodTime.Views.Product
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\_ViewImports.cshtml"
using ForGoodTime;

#line default
#line hidden
#line 1 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
using Model.Entities;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"753c3b8688adec02785d00179b62b0375c371783", @"/Views/Product/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"295ea0bb5948337d0734d2e2ec9cbc9c842b82fc", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Product>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Add", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Product", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
  
    ViewData["Title"] = "Categories";

#line default
#line hidden
            BeginContext(91, 135, true);
            WriteLiteral("\r\n\r\n<div class=\"container box_1170 border-top-generic\">\r\n    <div class=\"section padding_top\">\r\n        <div class=\"row\">\r\n            ");
            EndContext();
            BeginContext(227, 39, false);
#line 11 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
       Write(await Html.PartialAsync("_PartialMenu"));

#line default
#line hidden
            EndContext();
            BeginContext(266, 331, true);
            WriteLiteral(@"
            <div class=""col-md-9"">
                <table class=""table table-bordered"">
                    <thead>
                        <tr><th>Id</th><th>Name</th><th>Description</th><th>Deepth</th><th>Hight</th><th>Width</th><th>Price</th><th>Date</th><th>InStock</th><th>Action</th></tr>
                    </thead>
");
            EndContext();
#line 17 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
            BeginContext(670, 103, true);
            WriteLiteral("                        <tbody>\r\n                            <tr>\r\n                                <td>");
            EndContext();
            BeginContext(774, 7, false);
#line 21 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                               Write(item.Id);

#line default
#line hidden
            EndContext();
            BeginContext(781, 43, true);
            WriteLiteral("</td>\r\n                                <td>");
            EndContext();
            BeginContext(825, 9, false);
#line 22 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                               Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(834, 44, true);
            WriteLiteral(" </td>\r\n                                <td>");
            EndContext();
            BeginContext(879, 21, false);
#line 23 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                               Write(item.ShortDescription);

#line default
#line hidden
            EndContext();
            BeginContext(900, 76, true);
            WriteLiteral("</td>\r\n                               \r\n                                <td>");
            EndContext();
            BeginContext(977, 11, false);
#line 25 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                               Write(item.Deepth);

#line default
#line hidden
            EndContext();
            BeginContext(988, 43, true);
            WriteLiteral("</td>\r\n                                <td>");
            EndContext();
            BeginContext(1032, 10, false);
#line 26 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                               Write(item.Hight);

#line default
#line hidden
            EndContext();
            BeginContext(1042, 43, true);
            WriteLiteral("</td>\r\n                                <td>");
            EndContext();
            BeginContext(1086, 10, false);
#line 27 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                               Write(item.Width);

#line default
#line hidden
            EndContext();
            BeginContext(1096, 43, true);
            WriteLiteral("</td>\r\n                                <td>");
            EndContext();
            BeginContext(1140, 10, false);
#line 28 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                               Write(item.Price);

#line default
#line hidden
            EndContext();
            BeginContext(1150, 43, true);
            WriteLiteral("</td>\r\n                                <td>");
            EndContext();
            BeginContext(1194, 9, false);
#line 29 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                               Write(item.Date);

#line default
#line hidden
            EndContext();
            BeginContext(1203, 9, true);
            WriteLiteral("</td>\r\n\r\n");
            EndContext();
#line 31 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                                 if (item.InStock == true)
                                {

#line default
#line hidden
            BeginContext(1307, 56, true);
            WriteLiteral("                                    <td> In Stock</td>\r\n");
            EndContext();
#line 34 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                                }
                                else
                                {

#line default
#line hidden
            BeginContext(1471, 60, true);
            WriteLiteral("                                    <td> Not In Stock</td>\r\n");
            EndContext();
#line 38 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                                }

#line default
#line hidden
            BeginContext(1566, 74, true);
            WriteLiteral("                                <td>\r\n                                    ");
            EndContext();
            BeginContext(1640, 306, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "200a4f25a9a14918b4717ffc6e02facb", async() => {
                BeginContext(1704, 235, true);
                WriteLiteral("\r\n                                        <button type=\"submit\" class=\"btn btn-sm btn-danger\">\r\n                                            Delete\r\n                                        </button>\r\n                                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 40 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                                                                WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1946, 112, true);
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n\r\n                        </tbody>\r\n");
            EndContext();
#line 49 "C:\Users\nechy\source\repos\ForGoodTime\ForGoodTime\Views\Product\Index.cshtml"
                    }

#line default
#line hidden
            BeginContext(2081, 44, true);
            WriteLiteral("                </table>\r\n\r\n                ");
            EndContext();
            BeginContext(2125, 88, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "585914d0278745c2914897aacdea8b43", async() => {
                BeginContext(2194, 15, true);
                WriteLiteral("Add new Product");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2213, 58, true);
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Product>> Html { get; private set; }
    }
}
#pragma warning restore 1591
