﻿using books.Context;
using books.Exceptions;
using books.Models;
using books.Models.Dto;
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
