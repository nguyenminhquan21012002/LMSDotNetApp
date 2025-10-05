using Course.Core.Application.Commands;
using Course.Core.Application.DTOs;
using Course.Core.Application.Queries;
using LMSApp.Shared.DTOs;
using LMSApp.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Course.Presentation.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CoursesController> _logger;
        
        public CoursesController(IMediator mediator, ILogger<CoursesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<ActionResult<BaseListResponse<CourseDTO>>> GetAllCourses()
        {
            try
            {
                var query = new GetAllCoursesQuery();
                var result = await _mediator.Send(query);
                return Ok(ResponseHelper.SuccessList(result, "Courses retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetAllCoursesError");
                return StatusCode(500, ResponseHelper.InternalServerErrorList<CourseDTO>(
                    "An error occurred while retrieving courses", 500));
            }
        }

        [HttpGet("paged")]
        public async Task<ActionResult<BaseListResponse<CourseDTO>>> GetCoursesWithPagination([FromQuery] BaseRequest request)
        {
            try
            {
                if (request == null)
                    request = new BaseRequest();

                var query = new GetCoursesPagedQuery(request);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetCoursesWithPaginationError");
                return StatusCode(500, ResponseHelper.InternalServerErrorList<CourseDTO>(
                    "An error occurred while retrieving courses", 500));
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<CourseDTO>>> GetCourseById(string id)
        {
            try
            {
                var query = new GetCourseByIdQuery(id);
                var result = await _mediator.Send(query);
                
                if (result == null)
                    return NotFound(ResponseHelper.NotFound<CourseDTO>($"Course with ID {id} not found"));
                    
                return Ok(ResponseHelper.Success(result, "Course retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetCoursesByIdError");
                return StatusCode(500, ResponseHelper.InternalServerError<CourseDTO>(
                    "An error occurred while retrieving the course", 500));
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<BaseResponse<CourseDTO>>> CreateCourse([FromBody] CreateCourseDTO createCourseDto)
        {
            try
            {
                if (createCourseDto == null)
                    return BadRequest(ResponseHelper.BadRequest<CourseDTO>("Invalid course data provided"));

                var command = new CreateCourseCommand(createCourseDto);
                var result = await _mediator.Send(command);
                
                var response = ResponseHelper.Success(result, "Course created successfully");
                return CreatedAtAction(nameof(GetCourseById), new { id = result.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "CreateCourseError");
                return StatusCode(500, ResponseHelper.InternalServerError<CourseDTO>(
                    "An error occurred while creating the course", 500));
            }
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<CourseDTO>>> UpdateCourse(string id, [FromBody] UpdateCourseDTO updateCourseDto)
        {
            try
            {
                if (updateCourseDto == null)
                    return BadRequest(ResponseHelper.BadRequest<CourseDTO>("Invalid course data provided"));

                var command = new UpdateCourseCommand(id, updateCourseDto);
                var result = await _mediator.Send(command);
                
                if (result == null)
                    return NotFound(ResponseHelper.NotFound<CourseDTO>($"Course with ID {id} not found"));
                    
                return Ok(ResponseHelper.Success(result, "Course updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "UpdateCourseError");
                return StatusCode(500, ResponseHelper.InternalServerError<CourseDTO>(
                    "An error occurred while updating the course", 500));
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<object>>> DeleteCourse(string id)
        {
            try
            {
                var command = new DeleteCourseCommand(id);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound(ResponseHelper.NotFound<object>($"Course with ID {id} not found"));
                    
                return Ok(ResponseHelper.Success<object>(null, "Course deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "DeleteCourseError");
                return StatusCode(500, ResponseHelper.InternalServerError<object>(
                    "An error occurred while deleting the course", 500));
            }
        }
    }
}
