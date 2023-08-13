using System.Globalization;
using Humanizer;

namespace Sharee.Application.Services;

public class AuthorizationSession
{
    public readonly Guid Token;

    public AuthorizationSession(Guid token)
    {
        Token = token;
    }
    
    public Boolean CanAuthorizationOnSession(ISession session)
    {
        if (!session.IsAvailable)
        {
            return false;
        }
        
        if (!(session.GetString(nameof(Token)) is {} value 
            && Guid.TryParse(value, CultureInfo.InvariantCulture, out var token)))
        {
            return false;
        }

        if (token != Token)
        {
            return false;
        }

        return true;
    }

    public Boolean CanAuthorization(Guid token, ISession session)
    {
        if (Token == token)
        {
            session.SetString(nameof(Token), token.ToString());
            return true;
        }

        return false;
    }
}