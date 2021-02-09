using Api.Configuration;
using AutoMapper;
using Core.Services.Books;
using Data.Dtos.BookDtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class BookController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        #endregion

        #region Ctor
        public BookController(IMapper mapper,
            IBookService bookService)
        {
            this._mapper = mapper;
            this._bookService = bookService;
        }
        #endregion

        #region Methods

        [HttpGet(ApiRoutes.Books.GetById)]
        public async Task<IActionResult> GetBook(int bookId)
        {
            if (bookId <= 0)
                return BadRequest("Book id must be greater than zero.");

            // Get book
            var book = await _bookService.GetBookByIdAsync(bookId);

            return book is null ?
                BadRequest("Book not found !!!") :
                Ok(_mapper.Map<GetBookDto>(book));
        }

        [HttpGet(ApiRoutes.Books.GetAll)]
        public async Task<IActionResult> GetAllBooks()
        {
            // Get books
            var books = await _bookService.GetAllBooksAsync();

            return books is null ?
                BadRequest("No books found !!!") :
                Ok(_mapper.Map<IList<GetBookDto>>(books));
        }
        #endregion
    }
}
