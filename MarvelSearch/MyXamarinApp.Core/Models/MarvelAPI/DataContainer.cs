using System;
using System.Collections.Generic;

namespace MarvelSearch.Core.Models.MarvelAPI
{
    public class DataContainer
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public List<Comic> Results { get; set; }
    }
}
