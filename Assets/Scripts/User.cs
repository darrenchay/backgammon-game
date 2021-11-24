using System.Collections.Generic;

public class User
{
    private string username;
    private int wins;
    private int losses;

    public User(string username, int wins, int losses)
    {
        this.username = username;
        this.wins = wins;
        this.losses = losses;
    }

    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public int Wins
    {
        get { return wins; }
        set { wins = value; }
    }

    public int Losses
    {
        get { return losses; }
        set { losses = value; }
    }
}
