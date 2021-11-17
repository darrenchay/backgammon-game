using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/**
 * DICEFACECHECK
 * Used to record the faceup values of the die when they stop moving
 * **/
public class DiceFaceCheck : MonoBehaviour
{
    private Vector3 diceVel1;
    private Vector3 diceVel2;
    public GameObject Dice1; //reference to dice 1 game object
    public GameObject Dice2; //reference to dice 2 game object
    private AudioSource diceSound;


    //Values of the dice
    private int dice1Num;
    private int dice2Num;

    //Determines whether the dice are touching the board
    private bool isTouchingBoard;

    //Start function
    void start() {
        //Initializing that the dice are not touching the board
        isTouchingBoard = false;
    }
    void FixedUpdate()
    {
        diceVel1 = Dice1.GetComponent<Dice>().GetDiceVel();
        diceVel1 = Dice2.GetComponent<Dice>().GetDiceVel();
    }

    void OnTriggerEnter(Collider coll)
    {
        diceSound = GetComponent<AudioSource>();
        Debug.Log(diceSound);
        Debug.Log("Hit");
        diceSound.Play();
    }


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name.Contains("Side"))
        {
            if (diceVel1.x == 0f && diceVel1.y == 0f && diceVel1.z == 0f && diceVel2.x == 0f && diceVel2.y == 0f && diceVel2.z == 0f)
            {
                if (col.gameObject.GetComponent<DiceFace>().GetDiceNum() == 1)
                {
                    DiceNum.diceNumber1 = col.gameObject.GetComponent<DiceFace>().GetFaceUp();
                    dice1Num = col.gameObject.GetComponent<DiceFace>().GetFaceUp();
                }
                else if (col.gameObject.GetComponent<DiceFace>().GetDiceNum() == 2)
                {
                    DiceNum.diceNumber2 = col.gameObject.GetComponent<DiceFace>().GetFaceUp();
                    dice2Num = col.gameObject.GetComponent<DiceFace>().GetFaceUp();
                }
            }
            isTouchingBoard = true;
        }
        else {
            isTouchingBoard = false;
        }
    }

    //Getter for the value of the first die
    public int GetDice1Num() {
        return this.dice1Num;
    }

    //Getter for the value of the second die
    public int GetDice2Num()
    {
        return this.dice2Num;
    }

    //Getter for if the dice are touching the board
    public bool GetIsTouchingBoard() {
        return isTouchingBoard;
    }

    //Setter for if the dice are touching the board
    public void SetIsTouchingBoard(bool isTouchingBoard)
    {
        this.isTouchingBoard = isTouchingBoard;
    }

}
