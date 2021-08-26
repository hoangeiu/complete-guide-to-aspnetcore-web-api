using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books.Models.Dto
{
    public class PublisherDto
    {
        public string Name { get; set; }
    }

    public class PublisherWithBooksAndAuthorsDto
    {
        public string Name { get; set; }
        public List<BookAuthorDto> BookAuthors {  get; set; }
    }

    public class BookAuthorDto
    {
        public string BookName { get; set; }
        public List<string> BookAuthors {  get; set; }
    }
}
