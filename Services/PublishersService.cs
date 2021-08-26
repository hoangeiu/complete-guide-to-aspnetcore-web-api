using books.Context;
using books.Models;
using books.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public void AddPublisher(PublisherDto publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name,
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
        }

        public PublisherWithBooksAndAuthorsDto GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers
                .Where(p => p.Id == publisherId).Select(n => new PublisherWithBooksAndAuthorsDto()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorDto()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(ba => ba.Author.FullName).ToList()
                    }).ToList(),
                }).SingleOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);

            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
        }
    }
}
