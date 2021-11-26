using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Button rollButton;

    //Drops the dice from above
    public void Roll() {

        //If frozen unfreeze
        UnfreezDice();

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

        //Disable roll button if not deciding turn
        if (rollButton.GetComponentInChildren<Text>().text != "Decide Turn")
        {
            rollButton.interactable = false;
        }
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

    //Freezes the dice movement
    public void FreezeDice() {
        Rigidbody rb1 = dice1.GetComponent<Rigidbody>();
        Rigidbody rb2 = dice2.GetComponent<Rigidbody>();
        rb1.velocity = Vector3.zero;
        rb1.isKinematic = true;
        rb2.velocity = Vector3.zero;
        rb2.isKinematic = true;
    }

    //Unfreezes the dice movement
    public void UnfreezDice()
    {
        Rigidbody rb1 = dice1.GetComponent<Rigidbody>();
        Rigidbody rb2 = dice2.GetComponent<Rigidbody>();
        rb1.isKinematic = false;
        rb2.isKinematic = false;
    }
}
