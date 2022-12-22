bool accepted = false;
var check = new PasswordValidator();
while (!accepted)
{
    Console.Write("Enter password: ");
    string? password = Console.ReadLine();
    if (password != null)
    {
        if (!check.IsValidPassword(password))
        {
            Console.WriteLine("Invalid password.");
        }
        else
        {
            Console.WriteLine("Valid password");
            accepted = true;
        }
    }
    else
    {
        break;
    }
}

public class PasswordValidator
{
    public bool IsValidPassword(string password)
    {
        if (IsValidLength(password) &&
            HasUpper(password) &&
            HasLower(password) &&
            HasNumber(password) &&
            !password.Contains('T') &&
            !password.Contains('&'))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static bool IsValidLength(string s)
    {
        if (s.Length >= 6 && s.Length <= 13)
        {
            return true;
        }
        else return false;
    }

    private static bool HasUpper(string s)
    {
        foreach (char c in s)
        {
            if (char.IsUpper(c))
            {
                return true;
            }
        }
        return false;
    }

    private static bool HasLower(string s)
    {
        foreach (char c in s)
        {
            if (char.IsLower(c))
            {
                return true;
            }
        }
        return false;
    }

    private static bool HasNumber(string s)
    {
        foreach (char c in s)
        {
            if (char.IsNumber(c))
            {
                return true;
            }
        }
        return false;
    }

    private static bool Has(string s)
    {
        if (s.Contains('T'))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}