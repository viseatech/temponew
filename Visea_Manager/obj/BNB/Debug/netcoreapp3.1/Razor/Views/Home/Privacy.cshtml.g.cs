#pragma checksum "C:\Users\zlayani\source\repos\CleanViseagit\Visea_extranet\Visea_Manager\Views\Home\Privacy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9061e4a95dd6ddae254ad89ea4a4010971a2fe7b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Privacy), @"mvc.1.0.view", @"/Views/Home/Privacy.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\zlayani\source\repos\CleanViseagit\Visea_extranet\Visea_Manager\Views\_ViewImports.cshtml"
using Visea_Expense_Manager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\zlayani\source\repos\CleanViseagit\Visea_extranet\Visea_Manager\Views\_ViewImports.cshtml"
using Visea_Expense_Manager.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9061e4a95dd6ddae254ad89ea4a4010971a2fe7b", @"/Views/Home/Privacy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5b09bff2e5c0af606700ca2fd8d1840a3ba6ef9", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Privacy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\zlayani\source\repos\CleanViseagit\Visea_extranet\Visea_Manager\Views\Home\Privacy.cshtml"
  
    ViewData["Title"] = "Info";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>");
#nullable restore
#line 4 "C:\Users\zlayani\source\repos\CleanViseagit\Visea_extranet\Visea_Manager\Views\Home\Privacy.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>

<p>
    Bonjour, ce site est le nouvel extranet de visea, il est en <b class=""term""> phase de test </b> pour le moment.
    Ce site a été créé par <b class=""term""> Ziane Layadi (zlayadi@viseaconsulting.com)</b>.
    Pour tout retour, bug ou demande d’amélioration <b class=""term""> n’hésitez pas </b> à m'envoyer un mail.
    En effet le site est en amélioration constante, de nombreux points sont déjà créés mais n’ont pas encore passé les tests.
</p>


<iframe");
            BeginWriteAttribute("src", " src=\"", 544, "\"", 598, 1);
#nullable restore
#line 14 "C:\Users\zlayani\source\repos\CleanViseagit\Visea_extranet\Visea_Manager\Views\Home\Privacy.cshtml"
WriteAttributeValue("", 550, Url.Content("~/Export/TutoTEMPOcorrectpdf.pdf"), 550, 48, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" asp-append-version=\"true\" width=\"100%\" height=\"1000\"></iframe>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
