using Course.Core.Application.Commands;
using Course.Core.Application.DTOs;
using Course.Core.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.Presentation.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetAllCourses()
        {
            var query = new GetAllCoursesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourseById(Guid id)
        {
            var query = new GetCourseByIdQuery(id);
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound($"Course with ID {id} not found");
                
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<CourseDTO>> CreateCourse([FromBody] CreateCourseDTO createCourseDto)
        {
            var command = new CreateCourseCommand(createCourseDto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCourseById), new { id = result.Id }, result);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDTO>> UpdateCourse(Guid id, [FromBody] UpdateCourseDTO updateCourseDto)
        {
            var command = new UpdateCourseCommand(id, updateCourseDto);
            var result = await _mediator.Send(command);
            
            if (result == null)
                return NotFound($"Course with ID {id} not found");
                
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(Guid id)
        {
            var command = new DeleteCourseCommand(id);
            var result = await _mediator.Send(command);
            
            if (!result)
                return NotFound($"Course with ID {id} not found");
                
            return NoContent();
        }
    }
}
