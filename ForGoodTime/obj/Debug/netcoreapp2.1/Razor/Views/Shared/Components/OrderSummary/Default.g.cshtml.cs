#pragma checksum "C:\Users\nechy\OneDrive\Рабочий стол\ForGoodTime\ForGoodTime\ForGoodTime\Views\Shared\Components\OrderSummary\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "79e39a579990b8a836fd35fc5c106664ee4ea75f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ForGoodTime.Views.Shared.Components.OrderSummary.Views_Shared_Components_OrderSummary_Default), @"mvc.1.0.view", @"/Views/Shared/Components/OrderSummary/Default.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Components/OrderSummary/Default.cshtml", typeof(ForGoodTime.Views.Shared.Components.OrderSummary.Views_Shared_Components_OrderSummary_Default))]
namespace ForGoodTime.Views.Shared.Components.OrderSummary
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\nechy\OneDrive\Рабочий стол\ForGoodTime\ForGoodTime\ForGoodTime\Views\_ViewImports.cshtml"
using ForGoodTime;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79e39a579990b8a836fd35fc5c106664ee4ea75f", @"/Views/Shared/Components/OrderSummary/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"295ea0bb5948337d0734d2e2ec9cbc9c842b82fc", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_OrderSummary_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ForGoodTime.Views.Shared.Components.ShoppingCartSummary.ShoppingCartViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(86, 186, true);
            WriteLiteral("\r\n\r\n    <h2>Your Order</h2>\r\n    <ul class=\"list\">\r\n        <li>\r\n            <a href=\"#\">\r\n                Product\r\n                <span>Price</span>\r\n            </a>\r\n        </li>\r\n");
            EndContext();
#line 12 "C:\Users\nechy\OneDrive\Рабочий стол\ForGoodTime\ForGoodTime\ForGoodTime\Views\Shared\Components\OrderSummary\Default.cshtml"
         foreach (var item in Model.ShoppingCart.ShoppingCartItems)
        {

#line default
#line hidden
            BeginContext(352, 68, true);
            WriteLiteral("            <li>\r\n                <a href=\"#\">\r\n                    ");
            EndContext();
            BeginContext(421, 17, false);
#line 16 "C:\Users\nechy\OneDrive\Рабочий стол\ForGoodTime\ForGoodTime\ForGoodTime\Views\Shared\Components\OrderSummary\Default.cshtml"
               Write(item.Product.Name);

#line default
#line hidden
            EndContext();
            BeginContext(438, 45, true);
            WriteLiteral("\r\n                    <span class=\"middle\">x ");
            EndContext();
            BeginContext(484, 11, false);
#line 17 "C:\Users\nechy\OneDrive\Рабочий стол\ForGoodTime\ForGoodTime\ForGoodTime\Views\Shared\Components\OrderSummary\Default.cshtml"
                                      Write(item.Amount);

#line default
#line hidden
            EndContext();
            BeginContext(495, 48, true);
            WriteLiteral("</span>\r\n                    <span class=\"last\">");
            EndContext();
            BeginContext(545, 34, false);
#line 18 "C:\Users\nechy\OneDrive\Рабочий стол\ForGoodTime\ForGoodTime\ForGoodTime\Views\Shared\Components\OrderSummary\Default.cshtml"
                                   Write((item.Product.Price * item.Amount));

#line default
#line hidden
            EndContext();
            BeginContext(580, 52, true);
            WriteLiteral(" $</span>\r\n                </a>\r\n            </li>\r\n");
            EndContext();
#line 21 "C:\Users\nechy\OneDrive\Рабочий стол\ForGoodTime\ForGoodTime\ForGoodTime\Views\Shared\Components\OrderSummary\Default.cshtml"
        }

#line default
#line hidden
            BeginContext(643, 132, true);
            WriteLiteral("      \r\n    </ul>\r\n    <ul class=\"list list_2\">\r\n        <li>\r\n            <a href=\"#\">\r\n                Qty\r\n                <span>");
            EndContext();
            BeginContext(776, 23, false);
#line 28 "C:\Users\nechy\OneDrive\Рабочий стол\ForGoodTime\ForGoodTime\ForGoodTime\Views\Shared\Components\OrderSummary\Default.cshtml"
                 Write(Model.ShoppingCartTotal);

#line default
#line hidden
            EndContext();
            BeginContext(799, 57, true);
            WriteLiteral(" грн</span>\r\n            </a>\r\n        </li>\r\n    </ul>\r\n");
            EndContext();
            BeginContext(1752, 227, true);
            WriteLiteral("    <div class=\"creat_account\">\r\n        <input type=\"checkbox\" id=\"f-option4\" name=\"selector\" />\r\n        <label for=\"f-option4\">I’ve read and accept the </label>\r\n        <a href=\"#\">terms & conditions*</a>\r\n    </div>\r\n   \r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ForGoodTime.Views.Shared.Components.ShoppingCartSummary.ShoppingCartViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
