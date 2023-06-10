using System;
namespace MarvelSearch.Core.Models.MarvelAPI
{
    public class ComicsResponse
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public DataContainer Data { get; set; }
    }
}
