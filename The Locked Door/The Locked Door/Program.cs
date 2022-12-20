
string choice = "";
Door frontDoor;
BuildDoor();

while (choice?.ToUpper() != "Q")
{
    choice = GetUserChoice();
    RouteChoice(choice);

}

void RouteChoice(string userChoice)
{
    if (userChoice == "1") frontDoor.State = frontDoor.OpenDoor();
    else if (userChoice == "2") frontDoor.State = frontDoor.CloseDoor();
    else if (userChoice == "3") frontDoor.State = frontDoor.LockDoor();
    else if (userChoice == "4")
    {
        string code = GetPasscode();
        if (int.TryParse(code, out int userCode))
        {
            frontDoor.State = frontDoor.UnlockDoor(userCode);
        }
        else Console.WriteLine("Try again.");
    }
    else if (userChoice == "5")
    {
        string code = GetPasscode();
        Console.WriteLine("And the new code?");
        string newCode = GetPasscode();
        if (int.TryParse(code, out int userCode))
        {
            if (int.TryParse(newCode, out int resetCode))
            {
                frontDoor.ChangeCode(userCode, resetCode);
            }
        }
    }
    else choice = "Q";
}

Console.WriteLine("Goodbye");
Console.ReadLine();



string GetUserChoice()
{
    Console.WriteLine();
    Console.WriteLine("Select an action:");
    Console.WriteLine(
        "   1: Open the door\n" +
        "   2: Close the door\n" +
        "   3: Lock the door\n" +
        "   4: Unlock the door\n" +
        "   5: Change the passcode\n" +
        "   Any Key: Quit");
    return Console.ReadLine() ?? "Q";
}

string GetPasscode()
{
    Console.Write("Enter a passcode number: ");
    string? userInput = Console.ReadLine() ?? "";

    return userInput;
}

void BuildDoor()
{
    string userCode = GetPasscode();
    if (userCode == "") userCode = "1111";
    if (int.TryParse(userCode, out int factoryCode))
    {
        frontDoor = new Door(factoryCode);
    }
    else
    {
        Console.WriteLine("Try again.");
        BuildDoor();
    }
}


internal class Door
{
    public State State { get; set; }
    public int Passcode { get; set; }

    public Door(int code)
    {
        State = State.Locked;
        Passcode = code;
    }

    public State CloseDoor()
    {
        // If door is open, close it, otherwise it remains unchanged.
        State state;
        if (State == State.Open)
        {
            Console.WriteLine("The door is now Closed.");
            state = State.Closed;
        }
        else
        {
            Console.WriteLine($"The door is {State}. You cannot close it.");
            state = State;
        }
        return state;
    }

    public State LockDoor( )
    {
        //  If door is closed and unlocked, lock it, otherwise, it remains unchanged.
        State state;
        if (State == State.Closed)
        {
            Console.WriteLine("The door is now Locked.");
            state = State.Locked;
        }
        else
        {
            Console.WriteLine($"The door is {State}. You cannot lock it.");
            state = State;
        }
        return state;
    }

    public State OpenDoor()
    {
        // If door is closed, open it. If door is open or locked, it remains unchanged.
        State state;
        if (State == State.Closed)
        {
            Console.WriteLine("The door is now Open.");
            state = State.Open;
        }
        else
        {
            Console.WriteLine($"The door is {State}. You cannot open it.");
            state = State;
        }
        return state;
    }

    public State UnlockDoor(  int code)
    {
        // If door is locked, user needs to enter code, else door remains unchanged.
        // This could have been written in a similar nature as above with an AND clause
        // (door.State == State.Locked && code == door.Passcode), and it would have
        // behaved but in this way, I chose to give the user more nuanced feedback as 
        // to whether they door was in the wrong state or the code was wrong. We 
        // sacrificed simplicity for specificity.
        State state;
        if (State != State.Locked)
        {
            Console.WriteLine($"The door is {State}. You cannot unlock it.");
            state = State;
        }
        else if (code != Passcode)
        {
            Console.WriteLine("That code is incorrect. The door is still locked.");
            state = State;
        }
        else
        {
            Console.WriteLine("The door is now Unlocked.");
            state = State.Closed;
        }
        return state;
    }

    public void ChangeCode( int code, int newCode)
    {
        if (code == Passcode) Passcode = newCode;
        else Console.WriteLine("Incorrect code. You cannot change code.");
    }
}



public enum State
{
    Open,
    Closed,
    Locked
}
