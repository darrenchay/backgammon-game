using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * DICEFACE
 * Used to set the data for each face side game object (face up value and dicenum)
 * **/
public class DiceFace : MonoBehaviour
{
    public int faceUp;
    public int diceNum;


    public int GetFaceUp()
    {
        return faceUp;
    }

    public int GetDiceNum()
    {
        return diceNum;
    }

    public void SetFaceUp(int faceVal)
    {
        faceUp = faceVal;
    }

    public void SetDiceNum(int diceNo)
    {
        diceNum = diceNo;
    }
}
