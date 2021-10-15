using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour
{
    public int faceUp;
    public int diceNum;


    public int getFaceUp()
    {
        return faceUp;
    }

    public int getDiceNum()
    {
        return diceNum;
    }

    public void setFaceUp(int faceVal)
    {
        faceUp = faceVal;
    }

    public void setDiceNum(int diceNo)
    {
        diceNum = diceNo;
    }
}
