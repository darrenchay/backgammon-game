using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * SAVE DATA
 * Used to store and load user data
 * **/
public class SaveData : MonoBehaviour
{
    //Temporary user data
    private List<string> userNames;
    private List<int> userWins;
    private List<int> userLosses;

    // Start is called before the first frame update
    void Start()
    {
        //Initializing
        this.userNames = new List<string>();
        this.userWins = new List<int>();
        this.userLosses = new List<int>();

        //Initialize data if it does not exist
        if (!PlayerPrefs.HasKey("userNamesCount"))
        {
            PlayerPrefs.SetInt("userNamesCount", 0);
            PlayerPrefs.SetInt("userWinsCount", 0);
            PlayerPrefs.SetInt("userLossesCount", 0);
        }
        //Check for saved data and load
        else
        {
            //Load on start
            Load();
        }
    }

    //Getter
    public List<string> GetUserNames() {
        return this.userNames;
    }

    //Getter
    public List<int> GetUserWins()
    {
        return this.userWins;
    }

    //Getter
    public List<int> GetUserLosses()
    {
        return this.userLosses;
    }

    //Adds a win to a certain user
    public void AddWinToUser(string user) {
        int userIndex = userNames.IndexOf(user);

        userWins[userIndex] += 1;
    }

    //Adds a loss to a certain user
    public void AddLossToUser(string user) {
        int userIndex = userNames.IndexOf(user);

        userLosses[userIndex] += 1;
    }

    //Initializes user
    public void InitUser(string user) {
        print("Adding " + user);
        userNames.Add(user);
        userWins.Add(0);
        userLosses.Add(0);
    }

    //Determines if a user exists in the data
    public bool UserExists(string user) {
        return this.userNames.Contains(user);
    }

    //Loads data
    public void Load() {
        //Getting counts
        int userNameCount = PlayerPrefs.GetInt("userNamesCount");
        int userWinsCount = PlayerPrefs.GetInt("userWinsCount");
        int userLossesCount = PlayerPrefs.GetInt("userLossesCount");

        //Filling arrays
        for (int i = 0; i < userNameCount; i++)
            userNames.Add(PlayerPrefs.GetString("userNames" + i));

        for (int i = 0; i < userWinsCount; i++)
            userWins.Add(PlayerPrefs.GetInt("userWins" + i));

        for (int i = 0; i < userLossesCount; i++)
            userLosses.Add(PlayerPrefs.GetInt("userLosses" + i));
    }

    //Saving data
    public void Save() {
        PlayerPrefs.SetInt("userNamesCount", userNames.Count);
        PlayerPrefs.SetInt("userWinsCount", userWins.Count);
        PlayerPrefs.SetInt("userLossesCount", userLosses.Count);

        for (int i = 0; i < userNames.Count; i++)
            PlayerPrefs.SetString("userNames" + i, userNames[i]);

        for (int i = 0; i < userWins.Count; i++)
            PlayerPrefs.SetInt("userWins" + i, userWins[i]);

        for (int i = 0; i < userLosses.Count; i++)
            PlayerPrefs.SetInt("userLosses" + i, userLosses[i]);

        PlayerPrefs.Save();
    }

    //Deletes all user prefs
    public void DeleteUserPrefs() {
        PlayerPrefs.DeleteAll();
    }

}
