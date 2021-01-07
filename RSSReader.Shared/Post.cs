using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;

namespace RSSReader.Shared
{
    public class Post
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime PublishDate { get; set; }
        public Uri Uri{ get; set; }
        public string Author { get; set; }


    }
}
