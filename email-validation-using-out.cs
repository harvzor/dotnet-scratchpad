#:property TargetFramework net10.0

Console.Write("Input email: ");
string email = Console.ReadLine() ?? string.Empty;

if (TryValidateEmail(email, out string? errorReason) is false)
{
    Console.WriteLine($"Invalid - {errorReason}");
}
else
{
    Console.WriteLine("Valid");
}

bool TryValidateEmail(string email, out string? errorReason)
{
    if (string.IsNullOrWhiteSpace(email))
    {
        errorReason = "Email cannot be null or empty.";
        return false;
    }
    
    string[] atSplit = email.Split('@');
    
    if (atSplit.Length != 2
        || string.IsNullOrWhiteSpace(atSplit[0])
        || string.IsNullOrWhiteSpace(atSplit[1]))
    {
        errorReason = "Email must contain exactly one '@' character with non-empty parts on both sides.";
        return false;
    }
    
    string domainPart = atSplit[1];
    string [] domainDotSplit = domainPart.Split('.');
    if (domainDotSplit.Length < 2
        || string.IsNullOrWhiteSpace(domainDotSplit[0])
        || string.IsNullOrWhiteSpace(domainDotSplit[1]))
    {
        errorReason = "Domain part must contain at least one '.' character with non-empty parts on both sides.";
        return false;
    }

    errorReason = null;
    return true;
}
