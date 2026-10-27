using System.Globalization;
using SQLite;

namespace RahaHaldur.Models;

// Üks sissetulek või kulu. See klass on ühtlasi SQLite tabel.
public class MoneyEntry
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Title { get; set; } = "";
    public double Amount { get; set; }
    public bool IsIncome { get; set; }
    public string Category { get; set; } = Categories.Other;
    public DateTime Date { get; set; } = DateTime.Now;

    // Need väljad EI salvestata andmebaasi, neid kasutab ainult kuvamine
    [Ignore]
    public string CategoryText { get; set; } = "";

    [Ignore]
    public string AmountText => (IsIncome ? "+" : "-") + Format(Amount);

    public static string Format(double value)
    {
        return value.ToString("0.00", CultureInfo.InvariantCulture) + " €";
    }
}
