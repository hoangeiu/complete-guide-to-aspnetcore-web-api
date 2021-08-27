using books.Context;
using books.Exceptions;
using books.Models;
using books.Models.Dto;
using books.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber) {
            var allPublishers = _context.Publishers.OrderBy(n => n.Name).ToList();

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            // Searching
            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers
                    .Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            // Paging
            int pageSize = 5;
            allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1 , pageSize);

            return allPublishers;
        }

        public Publisher AddPublisher(PublisherDto publisher)
        {
            if (StringStartWithNumber(publisher.Name)) 
                throw new PublisherNameException("Name starts with number", publisher.Name);

            var _publisher = new Publisher()
            {
                Name = publisher.Name,
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(p => p.Id == id);

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
            else
            {
                throw new Exception($"The publisher with id: {id} does not exist");
            }
        }

        private bool StringStartWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));
    }
}
