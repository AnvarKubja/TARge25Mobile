namespace Memo;

// Mängija: nimi ja statistika
public class Player
{
    public string Name { get; set; }
    public int Moves { get; set; }

    public Player(string name)
    {
        Name = name;
        Moves = 0;
    }

    // Parim tulemus (vähim käike) salvestatakse telefoni mällu
    public int BestMoves
    {
        get => Preferences.Get("BestMoves", 0);
        private set => Preferences.Set("BestMoves", value);
    }

    // Tagastab true, kui see mäng oli uus rekord
    public bool SaveIfBest()
    {
        if (BestMoves == 0 || Moves < BestMoves)
        {
            BestMoves = Moves;
            return true;
        }
        return false;
    }
}
