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
public class GuestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GuestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GuestDto>>> GetGuests()
    {
        var result = await _mediator.Send(new GetGuestsQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GuestDto>> AddGuest([FromBody] CreateGuestRequest request)
    {
        var result = await _mediator.Send(new CreateGuestCommand(request));
        return CreatedAtAction(nameof(GetGuests), new { id = result.Id }, result);
    }
}