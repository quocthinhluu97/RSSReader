using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RSSReader.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RSSReader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RSSController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RSSController(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [ResponseCache(Duration = 1200)]
        public IActionResult Rss()
        {
            var feed = new SyndicationFeed("Title", "Description", new Uri("https://tinhte.vn/rss"), "RSSUrl", DateTime.Now);
            feed.Copyright = new TextSyndicationContent($"{DateTime.Now.Year} Quoc Thinh Luu");
            var items = new List<SyndicationItem>();
            var postings = _appDbContext.Posts.OrderByDescending(p => p.PubDate);

            foreach (var item in postings)
            {
                //string url = $"{GetBaseUrl()}";
                var postUrl = Url.Action("Article", "Blog", new { id = item.Url }, HttpContext.Request.Scheme);
                var title = item.Title;
                var description = item.Description;
                items.Add(new SyndicationItem(title, description, new Uri(postUrl), item.Url, item.PubDate));
            }

            feed.Items = items;
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NewLineHandling = NewLineHandling.Entitize,
                NewLineOnAttributes = true,
                Indent = true
            };

            using (var stream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(stream, settings)) {
                    var rssFormatter = new Rss20FeedFormatter(feed, false);
                    rssFormatter.WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }
                return File(stream.ToArray(), "application/rss+xml; charset=utf-8");
            }
        }

        public string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}
