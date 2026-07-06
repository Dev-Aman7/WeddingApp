using MediatR;
using WeddingApp.Application.Commands;
using WeddingApp.Application.Queries;
using WeddingApp.Application.DTOs;
using WeddingApp.Domain.Entities;
using WeddingApp.Domain.Repositories;

namespace WeddingApp.Application.Handlers;

public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, GuestDto>
{
    private readonly IGuestRepository _guestRepository;

    public CreateGuestCommandHandler(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<GuestDto> Handle(CreateGuestCommand request, CancellationToken ct)
    {
        var guest = new Guest
        {
            Name = request.Request.Name,
            Email = request.Request.Email,
            PhoneNumber = request.Request.PhoneNumber,
            Address = request.Request.Address
        };

        var created = await _guestRepository.AddAsync(guest);

        return new GuestDto
        {
            Id = created.Id,
            Name = created.Name,
            Email = created.Email,
            PhoneNumber = created.PhoneNumber,
            Address = created.Address,
            IsConfirmed = created.IsConfirmed,
            CreatedAt = created.CreatedAt
        };
    }
}

public class GetGuestsQueryHandler : IRequestHandler<GetGuestsQuery, List<GuestDto>>
{
    private readonly IGuestRepository _guestRepository;

    public GetGuestsQueryHandler(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<List<GuestDto>> Handle(GetGuestsQuery request, CancellationToken ct)
    {
        var guests = await _guestRepository.GetAllAsync();
        return guests.Select(g => new GuestDto
        {
            Id = g.Id,
            Name = g.Name,
            Email = g.Email,
            PhoneNumber = g.PhoneNumber,
            Address = g.Address,
            IsConfirmed = g.IsConfirmed,
            CreatedAt = g.CreatedAt
        }).ToList();
    }
}