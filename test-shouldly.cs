#:property TargetFramework net10.0

#:package Shouldly@4.3.0

using Shouldly;

// Arrange
const string email = "a@b";

// Act
bool result = IsEmail(email);

// Assert
result.ShouldBe(true);

static bool IsEmail(string email)
{
    try
    {
        var address = new System.Net.Mail.MailAddress(email).Address;
        return true;
    }
    catch
    {
        return false;
    }
}