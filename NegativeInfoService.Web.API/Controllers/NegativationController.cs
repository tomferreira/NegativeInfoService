using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NegativeInfoService.Application.Exceptions;
using NegativeInfoService.Application.Interfaces;
using NegativeInfoService.Application.ViewModels;
using NegativeInfoService.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NegativeInfoService.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NegativationController : ControllerBase
    {
        private readonly INegativationService _negativationService;
        private readonly ILogger<NegativationController> _logger;

        public NegativationController(
            INegativationService negativationService,
            ILogger<NegativationController> logger)
        {
            _negativationService = negativationService;
            _logger = logger;
        }

        // List all active negativations
        // GET: /api/negativation/index
        public async Task<ListNegativationViewModel> Index()
        {
            return await _negativationService.AllActiveAsync();
        }

        [HttpGet]
        [HttpGet("{id:guid}", Name = nameof(Show))]
        public async Task<IActionResult> Show(Guid? Id)
        {
            if (Id == null)
                return NotFound();

            var model = await _negativationService.GetAsync(Id.Value);

            return Ok(model);
        }

        // POST: /api/negativation/create
        [HttpPost]
        public IActionResult Create([FromBody] CreateNegativationViewModel model)
        {
            if (model == null)
            {
                _logger.LogError("Negativation model is null.");
                return BadRequest("Negativation model is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid negativation data.");
                return BadRequest("Invalid data");
            }

            try
            {
                var newModel = _negativationService.Add(model);

                return CreatedAtRoute(nameof(Show), new { id = newModel.Id }, newModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _negativationService.ResolveAsync(id);

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
