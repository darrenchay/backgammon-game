using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceUIUpdater : MonoBehaviour
{

    public RawImage[] diceUI;
    public Texture[] diceTexture;
    public GameObject board;

    // Updates the dice ui with the board numbers and the textures added to the gameobject attached
    void Update()
    {
        int[] rolls = new int[4];
        rolls = board.GetComponent<MovementManager>().GetCompleteRoll();

        for (int i = 0; i < 4; i++) {
            if (rolls[i] == -1)
            {
                diceUI[i].texture = diceTexture[0];
            }
            else {
                diceUI[i].texture = diceTexture[rolls[i]];
            }
        }

    }
}
