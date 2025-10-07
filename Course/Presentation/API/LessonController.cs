using Azure.Core;
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
    public class LessonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LessonController> _logger;

        public LessonController(IMediator mediator, ILogger<LessonController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<BaseListResponse<LessonDTO>>> GetAllLessons()
        {
            try
            {
                var query = new GetAllLessonsQuery();
                var result = await _mediator.Send(query);
                return Ok(ResponseHelper.SuccessList(result, "Lessons retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetAllLessonsError");
                return StatusCode(500, ResponseHelper.InternalServerErrorList<LessonDTO>(
                    "An error occurred while retrieving lessons", 500));
            }
        }

        [HttpGet("paged")]
        public async Task<ActionResult<BaseListResponse<LessonDTO>>> GetLessonWithPagination([FromQuery] BaseRequest request)
        {
            try
            {
                if (request == null)
                    request = new BaseRequest();
                var query = new GetLessonsPagedQuery(request, null);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetLessonWithPaginationError");
                return StatusCode(500, ResponseHelper.InternalServerErrorList<LessonDTO>(
                    "An error occurred while retrieving lessons", 500));
            }
        }

        [HttpGet("paged-with-course-id/{courseId}")]
        public async Task<ActionResult<BaseListResponse<LessonDTO>>> GetLessonWithPaginationCourseId([FromQuery] BaseRequest request, string courseId)
        {
            try
            {
                if (request == null)
                    request = new BaseRequest();
                var query = new GetLessonsPagedQuery(request, courseId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetLessonWithPaginationCourseIdError");
                return StatusCode(500, ResponseHelper.InternalServerErrorList<LessonDTO>(
                    "An error occurred while retrieving lessons", 500));
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<LessonDTO>>> CreateLesson([FromBody] CreateLessonDTO requestBody)
        {
            try
            {
                if (requestBody == null)
                    return BadRequest(ResponseHelper.BadRequest<LessonDTO>("Invalid course data provided"));
                var query = new CreateLessonCommand(requestBody);
                var result = await _mediator.Send(query);
                var response = ResponseHelper.Success(result, "Lesson created successfully");
                return CreatedAtAction(nameof(GetLessonById), new { id = result.Id }, response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message, "CreateLessonValidationError");
                return BadRequest(ResponseHelper.BadRequest<LessonDTO>(ex.Message));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex.Message, "CreateLessonInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<LessonDTO>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "CreateLessonsError");
                return StatusCode(500, ResponseHelper.InternalServerError<LessonDTO>(
                    "An error occurred while creating lessons", 500));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<LessonDTO>>> GetLessonById(string id)
        {
            try
            {
                var query = new GetLessonByIdQuery(id);
                var result = await _mediator.Send(query);

                if (result == null)
                    return NotFound(ResponseHelper.NotFound<LessonDTO>($"Lesson with ID {id} not found"));

                return Ok(ResponseHelper.Success(result, "Lesson retrieved successfully"));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex.Message, "GetLessonByIdInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<LessonDTO>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetLessonById");
                return StatusCode(500, ResponseHelper.InternalServerError<LessonDTO>(
                    "An error occurred while retrieving the lesson", 500));
            }
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<BaseListResponse<LessonDTO>>> GetLessonsByCourseId(string courseId)
        {
            try
            {
                var query = new GetLessonsByCourseIdQuery(courseId);
                var result = await _mediator.Send(query);
                return Ok(ResponseHelper.SuccessList(result, "Lessons retrieved successfully"));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message, "GetLessonsByCourseIdValidationError");
                return BadRequest(ResponseHelper.BadRequest<LessonDTO>(ex.Message));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex.Message, "GetLessonsByCourseIdInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<LessonDTO>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetLessonsByCourseIdError");
                return StatusCode(500, ResponseHelper.InternalServerErrorList<LessonDTO>(
                    "An error occurred while retrieving lessons", 500));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<LessonDTO>>> UpdateLesson(string id, [FromBody] UpdateLessonDTO updateLessonDto)
        {
            try
            {
                if (updateLessonDto == null)
                    return BadRequest(ResponseHelper.BadRequest<LessonDTO>("Invalid lesson data provided"));

                var command = new UpdateLessonCommand(id, updateLessonDto);
                var result = await _mediator.Send(command);

                if (result == null)
                    return NotFound(ResponseHelper.NotFound<LessonDTO>($"Lesson with ID {id} not found"));

                return Ok(ResponseHelper.Success(result, "Lesson updated successfully"));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message, "UpdateLessonValidationError");
                return BadRequest(ResponseHelper.BadRequest<LessonDTO>(ex.Message));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex.Message, "UpdateLessonInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<LessonDTO>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "UpdateLessonError");
                return StatusCode(500, ResponseHelper.InternalServerError<LessonDTO>(
                    "An error occurred while updating the lesson", 500));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<object>>> DeleteLesson(string id)
        {
            try
            {
                var command = new DeleteLessonCommand(id);
                var result = await _mediator.Send(command);

                if (!result)
                    return NotFound(ResponseHelper.NotFound<object>($"Lesson with ID {id} not found"));

                return Ok(ResponseHelper.Success<object>(null, "Lesson deleted successfully"));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex.Message, "DeleteLessonInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<object>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "DeleteLessonError");
                return StatusCode(500, ResponseHelper.InternalServerError<object>(
                    "An error occurred while deleting the lesson", 500));
            }
        }
    }
}
