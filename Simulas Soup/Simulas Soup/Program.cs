(Food food, Ingredient ingredient, Seasoning seasoning) meal;

meal.food = ChooseFood();
meal.ingredient = ChooseIngredient();
meal.seasoning = ChooseSeasoning();

Console.WriteLine();
Console.Write("Well chosen. Let's get started on your ");
Console.WriteLine($"{meal.seasoning} {meal.ingredient} {meal.food}.");
Console.ReadLine();

static Food ChooseFood()
{
    Console.WriteLine();
    Console.WriteLine("Choose a type: 1: Soup, 2: Stew, or 3: Gumbo");
    Console.Write("Enter the number:...");
    return Console.ReadLine() switch
    {
        "1" => Food.Soup,
        "2" => Food.Stew,
        "3" => Food.Gumbo,
        _ => Food.Unknown
    };
}

static Ingredient ChooseIngredient()
{
    Console.WriteLine();
    Console.Write("Choose an ingredient: 1: Mushroom, 2: Chicken, ");
    Console.WriteLine("3: Carrots, or 4: Potatoes");
    Console.Write("Enter the number:...");
    return Console.ReadLine() switch
    {
        "1" => Ingredient.Mushroom,
        "2" => Ingredient.Chicken,
        "3" => Ingredient.Carrots,
        "4" => Ingredient.Potatoes,
        _ => Ingredient.Unknown
    };
}

static Seasoning ChooseSeasoning()
{
    Console.WriteLine();
    Console.WriteLine("Choose a seasoning: 1: Spicy, 2: Salty, or 3: Sweet ");
    Console.Write("Enter the number:...");
    return Console.ReadLine() switch
    {
        "1" => Seasoning.Spicy,
        "2" => Seasoning.Salty,
        "3" => Seasoning.Sweet,
        _ => Seasoning.Unknown
    };
}

enum Food
{
    Unknown,
    Soup,
    Stew,
    Gumbo
}

enum Ingredient
{
    Unknown,
    Mushroom,
    Chicken,
    Carrots,
    Potatoes
}

enum Seasoning
{
    Unknown,
    Spicy,
    Salty,
    Sweet
}