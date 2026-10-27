namespace RahaHaldur.Models;

// Lihtne "kleeplapp" lehtede vahel.
// Ülevaate leht paneb siia kirje, mida tahetakse muuta, ja muutmise leht loeb selle siit.
// Kui siin on null, lisatakse uus kirje.
public static class AppState
{
    public static MoneyEntry EntryToEdit;
}
