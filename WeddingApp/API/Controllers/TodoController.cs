using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeddingApp.Application.Commands;
using WeddingApp.Application.DTOs;
using WeddingApp.Application.Queries;

namespace WeddingApp.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("todos")]
    public async Task<ActionResult<List<TodoDto>>> GetTodos()
    {
        var result = await _mediator.Send(new GetTodosQuery());
        return Ok(result);
    }

    [HttpPost("todos")]
    public async Task<ActionResult<TodoDto>> CreateTodo([FromBody] CreateTodoRequest request)
    {
        var result = await _mediator.Send(new CreateTodoCommand(request));
        return CreatedAtAction(nameof(GetTodos), new { id = result.Id }, result);
    }

    [HttpDelete("todos/{id}")]
    public async Task<ActionResult> DeleteTodo(int id)
    {
        var result = await _mediator.Send(new DeleteTodoCommand(id));
        if (!result)
            return NotFound();
        return NoContent();
    }
}