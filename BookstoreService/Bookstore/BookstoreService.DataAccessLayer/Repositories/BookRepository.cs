﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreService.DataAccessLayer.Contexts;
using BookstoreService.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreService.DataAccessLayer.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly BookDbContext context;

		public BookRepository(BookDbContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<Book>> GetAsync()
		{
			return await this.context.Books.ToListAsync();
		}

		public async Task<int> AddAsync(Book book)
		{
			var newBook = this.context.Add(book);
			await this.context.SaveChangesAsync();
			return newBook.Entity.Id;
		}

		public async Task DeleteAsync(int id)
		{
			var attachedBook = await this.context.FindAsync<Book>(id);
			this.context.Remove(attachedBook);
			await this.context.SaveChangesAsync();
		}

		public async Task UpdateAsync(int id, Book newBook)
		{
			var existing = await this.GetById(id);
			existing.Author = newBook.Author;
			existing.Title = newBook.Title;

			await this.context.SaveChangesAsync();
		}

		public async Task<Book> GetById(int id)
		{
			return await this.context.Books.FindAsync(id);
		}
	}

	public interface IBookRepository
	{
		Task<IEnumerable<Book>> GetAsync();
		Task<Book> GetById(int id);
		Task<int> AddAsync(Book book);
		Task DeleteAsync(int id);
		Task UpdateAsync(int id, Book newBook);
	}
}
