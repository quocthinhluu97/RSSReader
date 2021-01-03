using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSSReader.Shared
{
    public class Post
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }


    }
}
