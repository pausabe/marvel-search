using System;
using System.Collections.Generic;

namespace MarvelSearch.Core.Models
{
    public class Comic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Image Thumbnail { get; set; }
        public List<Image> Images { get; set; }
    }

    public class Image
    {
        public string Path { get; set; }
        public string Extension { get; set; }
    }
}
