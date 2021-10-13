using System.Collections;
using System.Collections.Generic;
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
            if(col.gameObject.name == "Side 1")
            {
                DiceNum.diceNumber = 6;
            } 
            else if (col.gameObject.name == "Side 2")
            {
                DiceNum.diceNumber = 3;
            }
            else if (col.gameObject.name == "Side 3")
            {
                DiceNum.diceNumber = 2;
            }
            else if (col.gameObject.name == "Side 4")
            {
                DiceNum.diceNumber = 5;
            }
            else if (col.gameObject.name == "Side 5")
            {
                DiceNum.diceNumber = 4;
            }
            else if (col.gameObject.name == "Side 6")
            {
                DiceNum.diceNumber = 1;
            }
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
