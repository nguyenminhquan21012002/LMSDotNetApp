using Course.Core.Application.Commands;
using Course.Core.Application.DTOs;
using Course.Core.Application.Queries;
using LMSApp.Shared.DTOs;
using LMSApp.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.Presentation.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ResourceController> _logger;

        public ResourceController(IMediator mediator, ILogger<ResourceController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("lesson/{lessonId}")]
        public async Task<ActionResult<BaseListResponse<ResourceDTO>>> GetResourcesByLessonId(string lessonId)
        {
            try
            {
                var query = new GetResourcesByLessonIdQuery(lessonId);
                var result = await _mediator.Send(query);
                return Ok(ResponseHelper.SuccessList(result, "Resources retrieved successfully"));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex.Message, "GetResourcesByLessonIdInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<ResourceDTO>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetResourcesByLessonIdError");
                return StatusCode(500, ResponseHelper.InternalServerErrorList<ResourceDTO>(
                    "An error occurred while retrieving resources", 500));
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<ResourceDTO>>> CreateResource([FromBody] CreateResourceDTO requestBody)
        {
            try
            {
                if (requestBody == null)
                    return BadRequest(ResponseHelper.BadRequest<ResourceDTO>("Invalid resource data provided"));

                var command = new CreateResourceCommand(requestBody);
                var result = await _mediator.Send(command);
                var response = ResponseHelper.Success(result, "Resource created successfully");
                return CreatedAtAction(nameof(GetResourcesByLessonId), new { lessonId = result.LessonId }, response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message, "CreateResourceValidationError");
                return BadRequest(ResponseHelper.BadRequest<ResourceDTO>(ex.Message));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex.Message, "CreateResourceInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<ResourceDTO>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "CreateResourceError");
                return StatusCode(500, ResponseHelper.InternalServerError<ResourceDTO>(
                    "An error occurred while creating the resource", 500));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<ResourceDTO>>> UpdateResource(string id, [FromBody] UpdateResourceDTO updateResourceDto)
        {
            try
            {
                if (updateResourceDto == null)
                    return BadRequest(ResponseHelper.BadRequest<ResourceDTO>("Invalid resource data provided"));

                var command = new UpdateResourceCommand(id, updateResourceDto);
                var result = await _mediator.Send(command);

                if (result == null)
                    return NotFound(ResponseHelper.NotFound<ResourceDTO>($"Resource with ID {id} not found"));

                return Ok(ResponseHelper.Success(result, "Resource updated successfully"));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex.Message, "UpdateResourceInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<ResourceDTO>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "UpdateResourceError");
                return StatusCode(500, ResponseHelper.InternalServerError<ResourceDTO>(
                    "An error occurred while updating the resource", 500));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<object>>> DeleteResource(string id)
        {
            try
            {
                var command = new DeleteResourceCommand(id);
                var result = await _mediator.Send(command);

                if (!result)
                    return NotFound(ResponseHelper.NotFound<object>($"Resource with ID {id} not found"));

                return Ok(ResponseHelper.Success<object>(null, "Resource deleted successfully"));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex.Message, "DeleteResourceInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<object>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "DeleteResourceError");
                return StatusCode(500, ResponseHelper.InternalServerError<object>(
                    "An error occurred while deleting the resource", 500));
            }
        }
    }
}
