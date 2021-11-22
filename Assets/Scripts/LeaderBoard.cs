using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{

    private SaveData saveData;
    public Text leaderBoard;

    // Start is called before the first frame update
    void Start()
    {
        saveData = this.gameObject.GetComponent<SaveData>();
        List<User> leaderboardList = SortLeaderboardList(GetLeaderboardList());
        string sortedLeaderboardString = GetSortedLeaderboardString(leaderboardList);
        leaderBoard.text = sortedLeaderboardString;
    }

    public string GetSortedLeaderboardString(List<User> leaderboardList)
    {
        string sortedLeaderboardString = System.String.Empty;
        int rank = 1;
        foreach (User user in leaderboardList)
        {
            sortedLeaderboardString += " " + rank + ". " + user.Username + "    W: " + user.Wins + "    L: " + user.Losses + "\n";
            rank++;
        }
        return sortedLeaderboardString;
    }

    public List<User> SortLeaderboardList(List<User> leaderboardList)
    {
        leaderboardList.Sort(delegate (User x, User y)
        {
            return (y.Wins - y.Losses) - (x.Wins - x.Losses);
        });
        return leaderboardList;
    }

    //GetUserWins
    //GetUserLosses
    //Wins - Losses
    //Who comes first? Combined score of win - losses
    public List<User> GetLeaderboardList()
    {
        List<string> usernames = saveData.GetUserNames();
        List<int> userWins = saveData.GetUserWins();
        List<int> userLosses = saveData.GetUserLosses();
        List<User> leaderboardList = new List<User>();

        for (int i = 0; i < usernames.Count; i++)
        {
            leaderboardList.Add(new User(usernames[i], userWins[i], userLosses[i]));
        }
        return leaderboardList;
    }
}
