using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * DICENUM 
 * Used to record the faceup values of the dies
 * **/
public class DiceNum : MonoBehaviour
{
    public static int diceNumber1;
    public static int diceNumber2;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Updated to new array display
        //text.text = "Dice 1 Roll: " +  diceNumber1.ToString() + ", Dice 2: " + diceNumber2.ToString();
        
    }
}
