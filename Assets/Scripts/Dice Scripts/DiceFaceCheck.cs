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

    void FixedUpdate()
    {
        diceVel1 = Dice1.GetComponent<Dice>().GetDiceVel();
        diceVel1 = Dice2.GetComponent<Dice>().GetDiceVel();
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.name.Contains("Side"))
        {
            if (diceVel1.x == 0f && diceVel1.y == 0f && diceVel1.z == 0f && diceVel2.x == 0f && diceVel2.y == 0f && diceVel2.z == 0f)
            {
                if (col.gameObject.GetComponent<DiceFace>().GetDiceNum() == 1)
                {
                    DiceNum.diceNumber1 = col.gameObject.GetComponent<DiceFace>().GetFaceUp();
                }
                else if (col.gameObject.GetComponent<DiceFace>().GetDiceNum() == 2)
                {
                    DiceNum.diceNumber2 = col.gameObject.GetComponent<DiceFace>().GetFaceUp();
                }
            }
        }
    }
}
