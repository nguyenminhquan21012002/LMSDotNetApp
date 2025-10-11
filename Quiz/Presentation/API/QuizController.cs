using LMSApp.Shared.DTOs;
using LMSApp.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quiz.Core.Application.Commands;
using Quiz.Core.Application.DTOs;
using Quiz.Core.Application.DTOs.Request;
using Quiz.Core.Application.Queries;

namespace Quiz.Presentation.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuizController> _logger;

        public QuizController(IMediator mediator, ILogger<QuizController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<QuizDTO>>> CreateQuiz([FromBody] CreateQuizDTO createQuizDTO)
        {
            try
            {
                var command = new CreateQuizCommand(createQuizDTO);
                var result = await _mediator.Send(command);

                return CreatedAtAction(
                    nameof(GetQuizById),
                    new { id = result.Id },
                    ResponseHelper.Success(result, "Quiz created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateQuizError");
                return StatusCode(500, ResponseHelper.InternalServerError<QuizDTO>(
                    "An error occurred while creating the quiz", 500));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<QuizDTO>>> GetQuizById(string id)
        {
            try
            {
                var query = new GetQuizByIdQuery(id);
                var result = await _mediator.Send(query);

                if (result == null)
                    return NotFound(ResponseHelper.NotFound<QuizDTO>($"Quiz with ID {id} not found"));

                return Ok(ResponseHelper.Success(result, "Quiz retrieved successfully"));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex.Message, "GetQuizByIdInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<bool>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetQuizByIdError");
                return StatusCode(500, ResponseHelper.InternalServerError<QuizDTO>(
                    "An error occurred while retrieving the quiz", 500));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<QuizDTO>>> UpdateQuiz(string id, [FromBody] UpdateQuizDTO updateQuizDTO)
        {
            try
            {
                var command = new UpdateQuizCommand(id, updateQuizDTO);
                var result = await _mediator.Send(command);

                if (result == null)
                    return NotFound(ResponseHelper.NotFound<QuizDTO>($"Quiz with ID {id} not found"));

                return Ok(ResponseHelper.Success(result, "Quiz updated successfully"));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex, "UpdateQuizInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<QuizDTO>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateQuizError");
                return StatusCode(500, ResponseHelper.InternalServerError<QuizDTO>(
                    "An error occurred while updating the quiz", 500));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> DeleteQuiz(string id)
        {
            try
            {
                var query = new DeleteQuizCommand(id);
                var result = await _mediator.Send(query);
                if (!result)
                    return NotFound(ResponseHelper.NotFound<bool>($"Quiz with ID {id} not found"));
                return Ok(ResponseHelper.Success(result, "Quiz deleted successfully"));
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex.Message, "DeleteQuizInvalidIdFormat");
                return BadRequest(ResponseHelper.BadRequest<bool>("Invalid ID format. Please provide a valid 24-character hexadecimal ID."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "DeleteQuizError");
                return StatusCode(500, ResponseHelper.InternalServerError<bool>(
                    "An error occurred while deleting the quiz", 500));
            }
        }
    }
}
