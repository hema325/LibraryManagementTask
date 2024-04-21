using AutoMapper;
using LibraryManagementTask.Data.Repositories._UnitOfWork;
using LibraryManagementTask.Dtos.Patrons;
using LibraryManagementTask.Entities;
using LibraryManagementTask.Errors;
using LibraryManagementTask.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementTask.Controllers
{
    [Route("api/patrons")]
    [Authorize]
    public class PatronsController : BaseApiController
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;
        private readonly ILogger<PatronsController> _logger;

        public PatronsController(IUnitOfWork ufw, IMapper mapper, ILogger<PatronsController> logger)
        {
            _ufw = ufw;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///  Add a new patron to the system.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync(CreateOrUpdatePatronDto dto)
        {
            var patron = _mapper.Map<Patron>(dto);

            _ufw.Patrons.Create(patron);
            await _ufw.CompleteAsync();

            _logger.LogInformation("New patron with id {id} has been added.", patron.Id);

            return Ok(patron.Id);
        }

        /// <summary>
        /// Update an existing patron's information.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAsync(int id, CreateOrUpdatePatronDto dto)
        {
            var patron = await _ufw.Patrons.GetByIdAsync(id);

            if (patron == null)
                return NotFound(ErrorResponse.NotFound());

            _mapper.Map(dto, patron);

            _ufw.Patrons.Update(patron);
            await _ufw.CompleteAsync();

            _logger.LogInformation("Patron with id {id} has been updated.", patron.Id);

            return NoContent();
        }

        /// <summary>
        /// Remove a patron from the system.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var patron = await _ufw.Patrons.GetByIdAsync(id);

            if (patron == null)
                return NotFound(ErrorResponse.NotFound());

            _ufw.Patrons.Delete(patron);
            await _ufw.CompleteAsync();

            return NoContent();
        }

        /// <summary>
        ///  Retrieve details of a specific patron by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PatronDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Cache]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var patron = await _ufw.Patrons.GetByIdAsync(id);

            if (patron == null)
                return NotFound(ErrorResponse.NotFound());

            return Ok(_mapper.Map<PatronDto>(patron));
        }

        /// <summary>
        /// Retrieve a list of all patrons.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PatronDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Cache]
        public async Task<IActionResult> GetAllAsync()
        {
            var patrons = await _ufw.Patrons.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<PatronDto>>(patrons));
        }
    }
}
