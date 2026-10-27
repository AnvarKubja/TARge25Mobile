namespace RahaHaldur.Models;

// Kulude kategooriad. Tõlge on ressursis nimega "Cat_" + kategooria nimi.
public static class Categories
{
    public const string Food = "Food";
    public const string Transport = "Transport";
    public const string Fun = "Fun";
    public const string Other = "Other";
    public const string Income = "Income"; // sissetulekutel alati see

    public static readonly string[] All = { Food, Transport, Fun, Other };
}
