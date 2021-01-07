using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RSSReader.Data;
using RSSReader.Shared;
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
        private readonly IMapper _mapper;

        public RSSController(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [Consumes("application/json")]
        [HttpGet]
        public IActionResult GetNewsFeed()
        {
            try
            {
                string url = "https://tinhte.vn/rss";
                var feed = new SyndicationFeed();
                using (var reader = XmlReader.Create(url))
                {
                    feed = SyndicationFeed.Load(reader);
                };

                var posts = new List<Post>();
                foreach (var item in feed.Items)
                {
                    var post = _mapper.Map<Post>(item);
                    //var post = new Post
                    //{
                    //    Title = item.Title.Text,
                    //    Summary = item.Summary.Text,
                    //    PublishDate = item.PublishDate.DateTime,
                    //    Authors = new List<SyndicationPerson>(item.Authors),
                    //    Links = new List<SyndicationLink>(item.Links)
                    //};
                    posts.Add(post);
                }

                return Ok(posts);
            }
            catch (Exception)
            {
            }
            return NotFound();
        }
    }
}
