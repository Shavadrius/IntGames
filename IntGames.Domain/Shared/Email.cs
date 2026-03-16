using IntGames.Domain.Abstractions;
using System.Net.Mail;

namespace IntGames.Domain.Shared;

public record Email 
{
    private Email(string value) => Value = value;
    
    public static IntGamesError Invalid(string message) => IntGamesError.Validation("Email", message);
    
    public string Value { get; }
    
    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return Invalid("Email cannot be empty.");
        }

        email = email.Trim().ToLowerInvariant();

        if (!IsValid(email))
        {
            return Invalid("Invalid email format.");
        }
        return new Email(email);
    }

    private static bool IsValid(string email)
    {
        try
        {
            var validEmail = new MailAddress(email);
            return string.Equals(validEmail.Address, email, StringComparison.OrdinalIgnoreCase);
        }
        catch { return false; }
    }
}
