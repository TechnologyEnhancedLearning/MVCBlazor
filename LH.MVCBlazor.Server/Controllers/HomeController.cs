using LH.MVCBlazor.Server.ViewModels;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Markdig;
using System.IO;
using Microsoft.AspNetCore.Html;

namespace LH.MVCBlazor.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

 

            ViewBag.HtmlContent = GetMarkdownReadMe();
            return View();
        }
        public IActionResult Blazor()
        {
            return View("_Host");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private IHtmlContent GetMarkdownReadMe() 
        {
            // Read the file contents
            string markdown = System.IO.File.ReadAllText("../ReadMe.md");
            string html = Markdig.Markdown.ToHtml(System.IO.File.ReadAllText($"{System.IO.Directory.GetCurrentDirectory()}{@"\wwwroot\StaticFiles\Readme.md"}"));



            // Convert Markdown to HTML using Markdig
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build(); // Use advanced extensions for tables and more
            string htmlContent = Markdown.ToHtml(markdown, pipeline);

            IHtmlContent htmlContentObj = new HtmlString(htmlContent);

            return htmlContentObj;


        }
    }
}
