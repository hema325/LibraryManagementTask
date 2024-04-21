using AutoMapper;
using LibraryManagementTask.Data.Repositories._UnitOfWork;
using LibraryManagementTask.Dtos.Books;
using LibraryManagementTask.Entities;
using LibraryManagementTask.Errors;
using LibraryManagementTask.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementTask.Controllers
{
    [Route("api/books")]
    [Authorize]
    public class BooksController: BaseApiController
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IUnitOfWork ufw, IMapper mapper, ILogger<BooksController> logger)
        {
            _ufw = ufw;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///  Add a new book to the library.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync(CreateOrUpdateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);

            _ufw.Books.Create(book);
            await _ufw.CompleteAsync();

            _logger.LogInformation("New book with id {id} has been added.", book.Id);

            return Ok(book.Id);
        }

        /// <summary>
        /// Update an existing book's information.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAsync(int id, CreateOrUpdateBookDto dto)
        {
            var book = await _ufw.Books.GetByIdAsync(id);

            if (book == null)
                return NotFound(ErrorResponse.NotFound());

            _mapper.Map(dto, book);

            _ufw.Books.Update(book);
            await _ufw.CompleteAsync();

            _logger.LogInformation("book with id {id} has been updated.", book.Id);

            return NoContent();
        }

        /// <summary>
        /// Remove a book from the library.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var book = await _ufw.Books.GetByIdAsync(id);

            if (book == null)
                return NotFound(ErrorResponse.NotFound());

            _ufw.Books.Delete(book);
            await _ufw.CompleteAsync();

            return NoContent();
        }

        /// <summary>
        /// Retrieve details of a specific book by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Cache]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var book = await _ufw.Books.GetByIdAsync(id);

            if (book == null)
                return NotFound(ErrorResponse.NotFound());

            return Ok(_mapper.Map<BookDto>(book));
        }

        /// <summary>
        /// Retrieve a list of all books.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Cache]
        public async Task<IActionResult> GetAllAsync()
        {
            var books = await _ufw.Books.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }
    }
}
