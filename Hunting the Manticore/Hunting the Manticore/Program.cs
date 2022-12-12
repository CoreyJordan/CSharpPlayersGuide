Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("The Manticore is Attacking!");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("The Manticore, The Uncoded One's airship, has been\n " +
    "spotted approaching the Consolas city walls. YOU must direct the\n " +
    "defenses to bring it down.\nCommand your forces well Captain!\n\n" + 
    "Enter an integer between 1 and 100 to range the Manticore.\n" +
    "Every 3rd round, you will fire an electric pulse dealing 3 damage.\n" +
    "Every 5th round, you will fire a fire bolt dealing 3 damage.\n" +
    "In the final round, you will deal a devastating combination blow,\n" +
    "   combining both fire and electric damage for 15 points.\n\n" +
    "But beware, each round you fail to bring the ship down, it will\n" +
    "   return fire, dealing 1 point to your city walls.\n\n" + 
    "Press enter to join the fray...");
Console.ReadLine();

// Establish the Manticore's starting position, and default game state.
int healthCity = 15;
int healthManticore = 10;
int round = 1;
int attackDistance = EstablishDistance();
string[] charges = { "Normal", "Fire", "Electric", "Combination" };


while (healthManticore > 0 && healthCity > 0)
{
    StartNewRound(healthCity, healthManticore, round);

    // Charge the cannon.
    string chargeType = ChargeCannon(round);
    Console.Write("Cannon charged for a ");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.Write(chargeType);
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(" shot, Sir!");
    Console.Write("Range to target?     ");

    // Take aim.
    int  targetRange = SetRangeToTarget(Console.ReadLine());
    Console.WriteLine();

    // Fire and track the shot.
    Console.WriteLine("AIM!!!    FIRE!!!");
    Console.WriteLine();
    bool hit = TrackShot(targetRange);
    Console.WriteLine();

    if (hit)
    {
        healthManticore -= CalculateDamage(chargeType);
        Console.ForegroundColor = ConsoleColor.White;
    }

    if (healthManticore <= 0)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("YOU'VE DONE IT SIR!!!!! THEY'RE GOING DOWN!");

    }
    else if (healthCity <= 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("The walls crumble around you as the last shot\n" +
            "from the Manticore collapsed the supports. The last thing you\n" +
            "as you feel the heat drain from your body is the sky turn to\n" +
            "darkness, the great Uncoded One sailing above you into Consolas");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("INCOMING!!!");
        Console.WriteLine("*1 dmg*");

        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("[Hit Enter]...");
        Console.ReadLine();

        healthCity--;
        round++;
    }
}




int CalculateDamage(string charge)
{
    int dmg;
    if (charge == charges[1])
    {
        Console.ForegroundColor = ConsoleColor.Red;
        dmg = 3;
    }
    else if (charge == charges[2])
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        dmg = 3;
    }
    else if (charge == charges[3])
    {
        Console.ForegroundColor = ConsoleColor.Green;
        dmg = 10;
    }
    else
    {
        dmg = 1;
    }
    Console.WriteLine($"{dmg} dmg");
    return dmg;
}

bool TrackShot(int range)
{
    if (range < attackDistance)
    {
        Console.WriteLine("Sir, we've come up short.");
        Console.WriteLine("Recomend we INCREASE our angle of attack.");
        return false;
    }
    else if (range > attackDistance)
    {
        Console.WriteLine("Sir, we've overshot the target.");
        Console.WriteLine("Recomend we DECREASE our angle of attack.");
        return false;
    }
    else
    {
        Console.WriteLine("DIRECT HIT, SIR!");
        Console.WriteLine("We'vegot them dialed in now, sir!");
        return true;
    }
}

int SetRangeToTarget(string? order)
{
    bool invalid = true;
    while (invalid)
    {
        if (order == null)
        {
            order = RepeatOrders(order);
        }
        else if (!int.TryParse(order, out int value))
        {
            order = RepeatOrders(order);
        }
        else if (value < 1)
        {
            order = RepeatOrders(order);
        }
        else
        {
            invalid = false;
        }
    }
    return int.Parse(order);
}

string ChargeCannon(int round)
{
    string type = "";
    if (round % 3 == 0 && round % 5 == 0)
    {
        type = charges[3];
    }
    else if (round % 5 == 0)
    {
        type = charges[2];
    }
    else if (round % 3 == 0)
    {
        type = charges[1];
    }
    else
    {
        type = charges[0];
    }
    return type;
}

int EstablishDistance()
{
    Random random = new Random();
    var distance = random.Next(100) + 1;
    return distance;
}

void StartNewRound(int hpCity, int hpAttacker, int round)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    for (int i = 0; i < 50; i++) { Console.Write("-"); }
    Console.WriteLine();
    Console.WriteLine($"Round {round} - " +
        $"City HP: {hpCity} || " +
        $"Manticore HP: {hpAttacker}");
    for (int i = 0; i < 50; i++) { Console.Write("-"); }
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
}

static string? RepeatOrders(string? order)
{
    Console.Write("Sorry, Sir. Repeat your order.     " );
    order = Console.ReadLine();
    return order;
}