using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * SCENECHANGER 
 * Used to navigate between the scenes
 * **/
public class SceneChanger : MonoBehaviour
{
	//Player1 and Player2 names
	public Text p1Name;
	public Text p2Name;

	private int nameLengthLimit = 15; 

	//Basic scene changer
	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	//Scene changer to start game (needs proper user validation)
	public void StartGame(string sceneName)
	{
		if (p1Name.text == "") //If there is no username entered, set player as guest
		{
			PlayerPrefs.SetString("p1Name", "Guest");
			PlayerPrefs.SetInt("p1Guest", 1);
		}
		if (p1Name.text != "" && p1Name.text.Length <= nameLengthLimit)
		{
			PlayerPrefs.SetString("p1Name", p1Name.text);
			PlayerPrefs.SetInt("p1Guest", 0);
		}
		if (p2Name.text == "")
		{
			PlayerPrefs.SetString("p2Name", "Guest");
			PlayerPrefs.SetInt("p2Guest", 1);
		}
		if (p2Name.text != "" && p1Name.text.Length <= nameLengthLimit)
		{
			PlayerPrefs.SetString("p2Name", p2Name.text);
			PlayerPrefs.SetInt("p2Guest", 0);
		}

		//Both players must be guest, or different users (user - user, user - guest + vice versa)
		if (p1Name.text != p2Name.text || p1Name.text == "" && p2Name.text == "")
		{
			PlayerPrefs.Save();
			SceneManager.LoadScene(sceneName);
		}
	}

	public void Exit()
	{
		Application.Quit();
	}
}
