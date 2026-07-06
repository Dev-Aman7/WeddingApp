using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using WeddingApp.Application.DTOs;
using WeddingApp.Application.Commands;
using WeddingApp.Application.Queries;

namespace WeddingApp.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventController: ControllerBase
{
    private readonly IMediator _mediator;
    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<EventResponseDto>> Create([FromBody] CreateEventRequestDto eventEntity)
    {
        var createdEvent = await _mediator.Send(new CreateEventCommand(eventEntity));
        return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventResponseDto>> GetEventById(int id)
    {
        var result = await _mediator.Send(new GetEventByIdQuery(id));
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<EventResponseDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetEventsQuery());
        return Ok(result);
    }
}   