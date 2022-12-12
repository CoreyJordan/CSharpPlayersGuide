State currentState = State.Locked;
bool valid;
string action;
const string openChest = "open";
const string closeChest = "close";
const string lockChest = "lock";
const string unlockChest = "unlock";

Console.ForegroundColor = ConsoleColor.White;
while (true)
{
    action = GetPlayerChoice();
    ActOnChest(action);
}



void ActOnChest(string act)
{
    if (currentState == State.Locked)
    {
        currentState = act switch
        {
            unlockChest => State.Closed,
            _ => State.Locked
        };
    }
    else if (currentState == State.Closed)
    {
        currentState = act switch
        {
            openChest => State.Open,
            lockChest => State.Locked,
            _ => State.Closed
        };
    }
    else
    {
        currentState = act switch
        {
            closeChest => State.Closed,
            _ => State.Open
        };
    }
}

string GetPlayerChoice()
{
    valid = false;
    string? action = "";
    while (!valid)
    {
        Console.Write("The chest is ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"{currentState.ToString().ToLower()}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(". What do you want to do?..");
   
        action = Console.ReadLine()!.ToLower();
        
        valid = action switch
        {
            openChest => true,
            closeChest => true,
            lockChest => true,
            unlockChest => true,
            _ => false
        };
    }
    return action;
}



enum State
{
    Locked,
    Closed,
    Open
}
