using System;

namespace IDV_NET5_API.Models
{
    public class SearchParam
    {
        public int PageIndex;
        public int nbResult;
        public string Title { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public string Genre { get; set; }
    }
}
