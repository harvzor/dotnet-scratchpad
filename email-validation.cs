#:property TargetFramework net10.0

Console.Write("Input email: ");
var input = Console.ReadLine();

try
{
    var address = new System.Net.Mail.MailAddress(input).Address;
    Console.WriteLine("System.Net.Mail - Valid");
}
catch
{
    Console.WriteLine("System.Net.Mail - Invalid");
}

// https://learn.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format?redirectedfrom=MSDN
if (RegexExamples.RegexUtilities.IsValidEmail(input))
{

    Console.WriteLine("Microsoft Regex Validator - Valid");
}
else
{
    Console.WriteLine("Microsoft Regex Validator - Invalid");
}

namespace RegexExamples
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    class RegexUtilities
    {
        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}