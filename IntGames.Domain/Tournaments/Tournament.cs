using IntGames.Domain.Abstractions;
using IntGames.Domain.Participants;
using IntGames.Domain.Shared;
using IntGames.Domain.TournamentRequests;

namespace IntGames.Domain.Tournaments;

public sealed class Tournament : Entity
{
    private Tournament(
        Guid id,
        Title title,
        TournamentType type,
        DateRange dateRange,
        bool isPaymentRequired) : base(id)
    {
        Title = title;
        Type = type;
        DateRange = dateRange;
        IsPaymentRequired = isPaymentRequired;
    }

    private readonly List<TournamentRequest> _requests = [];

    public TournamentType Type { get; private init; }
    public Title Title { get; private set; }
    public DateRange DateRange { get; private set; }
    public bool IsPaymentRequired { get; private set; }

    public static Result<Tournament> Create(
        Title title,
        TournamentType type,
        DateRange dateRange,
        bool isPaymentRequired)
    {
        return new Tournament(
            id: Guid.NewGuid(),
            title,
            type,
            dateRange,
            isPaymentRequired);
    }

    public IReadOnlyList<Participant> GetRegisteredParticipants()
    {
        return [.. _requests.Where(r => r.IsRegistered).SelectMany(r => r.Participants)];
    }

    public Result AddRequest(TournamentRequest request)
    {
        if (request.TournamentId != Id)
        {
            return Result.Failure(IntGamesError.Validation("Request.TournamentId", "Wrong tournament in request."));
        }

        if (_requests.Any(r => r.Id == request.Id))
        {
            return Result.Failure(IntGamesError.Validation("Request.Id", "Requests Duplication."));
        }

        switch (Type)
        {
            case TournamentType.Chgk:
                {
                    /*TODO: Implement later*/
                    break;
                }
            case TournamentType.SiGame:
                {
                    if (request.Participants.Count != 1)
                    {
                        return Result.Failure(TournamentErrors.WrongTournamentType);
                    }
                    break;
                }
        }

        _requests.Add(request);

        return Result.Success();
    }

    public Result ApproveRequest(Guid requestId)
    {
        var request = _requests.FirstOrDefault(t => t.Id == requestId);

        if (request == null)
        {
            return Result.Failure(TournamentErrors.RequestNotFound);
        }

        if (!request.Participants.Any())
        {
            return Result.Failure(TournamentRequestErrors.ParticipantsAreEmpty);
        }

        if (request.Participants.Any(p => CheckIfPlayerIsApproved(p.PlayerId, requestId)))
        {
            return Result.Failure(TournamentErrors.PlayersAreNotUnique);
        }

        request.Approve(IsPaymentRequired);

        return Result.Success();
    }

    private bool CheckIfPlayerIsApproved(Guid playerId, Guid requestId)
    {
        return _requests
            .Where(r => r.Id != requestId)
            .FirstOrDefault(r => r.IsRegistered && r.Participants.Any(p => p.PlayerId == playerId)) is not null;
    }
}
