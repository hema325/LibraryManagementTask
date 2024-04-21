using AutoMapper;
using LibraryManagementTask.Data.Repositories._UnitOfWork;
using LibraryManagementTask.Dtos.BorrowingRecords;
using LibraryManagementTask.Entities;
using LibraryManagementTask.Errors;
using LibraryManagementTask.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementTask.Controllers
{
    [Route("api/borrowingRecords")]
    [Authorize]
    public class BorrowingRecordsController: BaseApiController
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;
        private readonly ILogger<BorrowingRecordsController> _logger;

        public BorrowingRecordsController(IUnitOfWork ufw, IMapper mapper, ILogger<BorrowingRecordsController> logger)
        {
            _ufw = ufw;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Allow a patron to borrow a book.
        /// </summary>
        [HttpPost("/api/borrow/{bookId}/patron/{patronId}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync(int bookId, int patronId, CreateBorrowingRecordDto dto)
        {
            var record = _mapper.Map<BorrowingRecord>(dto);
            record.BookId = bookId;
            record.PatronId = patronId;

            _ufw.BorrowingRecords.Create(record);
            await _ufw.CompleteAsync();

            _logger.LogInformation("Patron with id {patronId} has borrowed a book with id {bookId}.", patronId, bookId);

            return Ok(record.Id);
        }

        /// <summary>
        /// Record the return of a borrowed book by a patron.
        /// </summary>
        [HttpPut("/api/return/{bookId}/patron/{patronId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAsync(int bookId, int patronId, ReturnBorrowingRecordDto dto)
        {
            var record = await _ufw.BorrowingRecords.GetBorrowedBookByPatronIdBookIdAsync(patronId, bookId);

            if (record == null)
                return NotFound(ErrorResponse.NotFound());

            _mapper.Map(dto, record);

            _ufw.BorrowingRecords.Update(record);
            await _ufw.CompleteAsync();

            _logger.LogInformation("Patron with id {patronId} has returned a book with id {bookId}.", patronId, bookId);

            return NoContent();
        }

        /// <summary>
        /// Retrieve details of a specific borrowing record by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BorrowingRecordDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Cache]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var record = await _ufw.BorrowingRecords.GetByIdAsync(id);

            if (record == null)
                return NotFound(ErrorResponse.NotFound());

            return Ok(_mapper.Map<BorrowingRecordDto>(record));
        }

        /// <summary>
        /// Retrieve a list of all borrowing records.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BorrowingRecordDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Cache]
        public async Task<IActionResult> GetAllAsync()
        {
            var records = await _ufw.BorrowingRecords.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<BorrowingRecordDto>>(records));
        }

    }
}
