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
		if (p1Name.text != "" && p2Name.text != "" && p1Name.text != p2Name.text && p1Name.text.Length <= nameLengthLimit && p1Name.text.Length <= nameLengthLimit)
		{
			PlayerPrefs.SetString("p1Name", p1Name.text);
			PlayerPrefs.SetString("p2Name", p2Name.text);
			PlayerPrefs.SetInt("playAsGuest", 0);
			PlayerPrefs.Save();

			SceneManager.LoadScene(sceneName);
		}
	}

	//Starts game as guest
	public void StartGameGuest(string sceneName) 
	{
		PlayerPrefs.SetString("p1Name", "Guest");
		PlayerPrefs.SetString("p2Name", "Guest");
		PlayerPrefs.SetInt("playAsGuest", 1);
		PlayerPrefs.Save();
		SceneManager.LoadScene(sceneName);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
