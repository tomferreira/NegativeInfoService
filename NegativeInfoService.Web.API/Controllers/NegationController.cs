using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NegativeInfoService.Application.Exceptions;
using NegativeInfoService.Application.Interfaces;
using NegativeInfoService.Application.ViewModels;
using NegativeInfoService.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace NegativeInfoService.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NegationController : ControllerBase
    {
        private readonly INegationService _negationService;
        private readonly ILogger<NegationController> _logger;

        public NegationController(
            INegationService negationService,
            ILogger<NegationController> logger)
        {
            _negationService = negationService;
            _logger = logger;
        }

        // List all active negations
        // GET: /api/negation/index
        public async Task<ListNegationViewModel> Index()
        {
            return await _negationService.AllActiveAsync();
        }

        // Retrieve a specific negation by id
        // GET: /api/negation/show/{id}
        [HttpGet("{id:guid}", Name = nameof(Show))]
        public async Task<IActionResult> Show(Guid? Id)
        {
            if (Id == null)
                return NotFound();

            var model = await _negationService.GetAsync(Id.Value);

            return Ok(model);
        }

        // Create a negation
        // POST: /api/negation/create
        [HttpPost]
        public IActionResult Create([FromBody] CreateNegationViewModel model)
        {
            if (model == null)
            {
                _logger.LogError("Negation model is null.");
                return BadRequest("Negation model is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid negation data.");
                return BadRequest("Invalid data");
            }

            try
            {
                var newModel = _negationService.Add(model);

                return CreatedAtRoute(nameof(Show), new { id = newModel.Id }, newModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // Resolve (virtual delete) a negation
        // DELETE: /api/negation/delete/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _negationService.ResolveAsync(id);

                return Ok();
            }
            catch (BusinessRuleException ex)
            {
                if (ex is EntityNotFoundException)
                    return NotFound();

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
