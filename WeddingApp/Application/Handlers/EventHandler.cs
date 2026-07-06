using MediatR;

using WeddingApp.Application.Commands;
using WeddingApp.Application.DTOs;
using WeddingApp.Application.Queries;
using WeddingApp.Domain.Entities;
using WeddingApp.Domain.Repositories;


namespace WeddingApp.Application.Handlers;

public class CreateEventCommandHandler : 
    IRequestHandler<CreateEventCommand, EventResponseDto>
{
    private readonly IEventRepository _eventRepository;

    public CreateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventResponseDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = new Event
        {
            Name = request.Request.Name,
            Date = request.Request.Date,
            Location = request.Request.Location,
            Description = request.Request.Description
        };

        var createdEvent = await _eventRepository.AddAsync(eventEntity);

        return new EventResponseDto
        {
            Id = createdEvent.Id,
            Name = createdEvent.Name,
            Date = createdEvent.Date,
            Location = createdEvent.Location,
            Description = createdEvent.Description,
        };
    }
}

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventResponseDto>
{
    private readonly IEventRepository _eventRepository;

    public GetEventByIdQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventResponseDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(request.Id);

        if (eventEntity == null)
        {
            throw new KeyNotFoundException($"Event with ID {request.Id} not found.");
        }

        return new EventResponseDto
        {
            Id = eventEntity.Id,
            Name = eventEntity.Name,
            Date = eventEntity.Date,
            Location = eventEntity.Location,
            Description = eventEntity.Description,
        };
    }
}

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, List<EventResponseDto>>
{
    private readonly IEventRepository _eventRepository;

    public GetEventsQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<List<EventResponseDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetAllAsync();

        return events.Select(e => new EventResponseDto
        {
            Id = e.Id,
            Name = e.Name,
            Date = e.Date,
            Location = e.Location,
            Description = e.Description,
        }).ToList();
    }
}

