using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * DROPDICE
 * Used to roll dice
 * **/

public class DropDice : MonoBehaviour
{
    //Gameobject for the 2 dice
    public GameObject dice1;
    public GameObject dice2;

    //The board and dicefacechecker object to data share
    public GameObject board;
    public GameObject diceFaceChecker;
    
    //Drops the dice from above
    public void dropDice() {

        //Set that the dice are not touching the board
        diceFaceChecker.GetComponent<DiceFaceCheck>().SetIsTouchingBoard(false);

        //Set both dice acitve incase they were not
        dice1.SetActive(true);
        dice2.SetActive(true);

        //Setting the position of the dice to the drop dice location
        dice1.transform.position = new Vector3(-1.27f, gameObject.transform.position.y, -1.09f);
        dice2.transform.position = new Vector3(2.05f, gameObject.transform.position.y, -1.0811f);

        //Performing a random rotation to ensure random dice rolls
        dice1.transform.Rotate(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), Space.Self);
        dice2.transform.Rotate(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), Space.Self);
    }

    //Hides the dice
    public void HideDice() {
        dice1.SetActive(false);
        dice2.SetActive(false);
    }

    //Shows the dice
    public void ShowDice() {
        dice1.SetActive(true);
        dice2.SetActive(true);
    }
}
