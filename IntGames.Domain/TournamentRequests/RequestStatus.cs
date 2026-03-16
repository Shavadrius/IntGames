namespace IntGames.Domain.TournamentRequests;

public enum RequestStatus
{
    /// <summary>
    /// Participant has submitted a registration request, awaiting approval
    /// </summary>
    PendingApproval = 0,

    /// <summary>
    /// Registration has been approved, awaiting payment (for paid tournaments)
    /// </summary>
    AwaitingPayment = 1,

    /// <summary>
    /// Registration has been approved (and payment received if required)
    /// </summary>
    Approved = 2,

    /// <summary>
    /// Registration has been rejected by the organizer
    /// </summary>
    Rejected = 3,
}
