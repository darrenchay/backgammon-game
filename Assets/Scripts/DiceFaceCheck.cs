using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DiceFaceCheck : MonoBehaviour
{
    Vector3 diceVel;

    void FixedUpdate()
    {
        diceVel = Dice.diceVel;
    }

    void OnTriggerStay(Collider col)
    {
        if(diceVel.x == 0f && diceVel.y == 0f && diceVel.z == 0f)
        {
            Console.WriteLine(col.gameObject);
            Console.WriteLine("Test");
            Console.WriteLine(col.gameObject.GetComponent<DiceFace>());
            if(col.gameObject.GetComponent<DiceFace>().getDiceNum() == 1)
            {
                DiceNum.diceNumber1 = col.gameObject.GetComponent<DiceFace>().getFaceUp();
            }
            else if (col.gameObject.GetComponent<DiceFace>().getDiceNum() == 2)
            {
                DiceNum.diceNumber2 = col.gameObject.GetComponent<DiceFace>().getFaceUp();
            }
            //if(col.gameObject.name == "Side 1")
            //{
            //    DiceNum.diceNumber = 6;
            //} 
            //else if (col.gameObject.name == "Side 2")
            //{
            //    DiceNum.diceNumber = 3;
            //}
            //else if (col.gameObject.name == "Side 3")
            //{
            //    DiceNum.diceNumber = 2;
            //}
            //else if (col.gameObject.name == "Side 4")
            //{
            //    DiceNum.diceNumber = 5;
            //}
            //else if (col.gameObject.name == "Side 5")
            //{
            //    DiceNum.diceNumber = 4;
            //}
            //else if (col.gameObject.name == "Side 6")
            //{
            //    DiceNum.diceNumber = 1;
            //}
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
