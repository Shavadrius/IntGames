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

    private readonly List<Participant> _participants = [];
    private readonly List<TournamentRequest> _requests = [];


    public IReadOnlyList<Participant> Participants => _participants.AsReadOnly();
    public IReadOnlyList<TournamentRequest> Teams => _requests.Where(t => t.IsRegistered).ToList().AsReadOnly();
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

        var participants = request.Players.Select(x => Participant.FromPlayer(x, Id));

        if (!participants.Any())
        {
            return Result.Failure(TournamentRequestErrors.ParticipantsAreEmpty);
        }

        if (participants.Any(p => CheckIfPlayerIsApproved(p.PlayerId)))
        {
            return Result.Failure(TournamentErrors.PlayersAreNotUnique);
        }

        _participants.AddRange(participants);

        return Result.Success();
    }

    private bool CheckIfPlayerIsApproved(Guid playerId)
    {
        return _participants.FirstOrDefault(p => p.PlayerId == playerId)?.IsRegistered ?? false;
    }
}
