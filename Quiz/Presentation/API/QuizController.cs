using LMSApp.Shared.DTOs;
using LMSApp.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quiz.Core.Application.DTOs;
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetQuizByIdError");
                return StatusCode(500, ResponseHelper.InternalServerError<QuizDTO>(
                    "An error occurred while retrieving the quiz", 500));
            }
        }
    }
}
