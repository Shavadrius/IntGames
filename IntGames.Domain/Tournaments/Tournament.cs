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

    public void AddRequest(TournamentRequest request)
    {
        _requests.Add(request);
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

        if (request.Participants.Any(p => CheckIfPlayerIsApproved(p.PlayerId)))
        {
            return Result.Failure(TournamentErrors.PlayersAreNotUnique);
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

        request.Approve(IsPaymentRequired);

        return Result.Success();
    }

    private bool CheckIfPlayerIsApproved(Guid playerId)
    {
        return _requests.FirstOrDefault(r => r.IsRegistered && r.Participants.Any(p => p.PlayerId == playerId)) is not null;
    }
}
