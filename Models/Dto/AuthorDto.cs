using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books.Models.Dto
{
    public class AuthorDto
    {
        public string FullName { get; set; }
    }

    public class AuthorWithBooksDto
    {
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
