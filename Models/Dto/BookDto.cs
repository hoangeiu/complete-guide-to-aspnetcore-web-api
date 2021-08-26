﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books.Models.Dto
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public string CoverUrl { get; set; }
    }
}
