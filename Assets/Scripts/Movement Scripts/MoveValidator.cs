using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * MOVEVALIDATOR
 * Used to ensure moves are legal based on the rules of backgammon
 * **/

public class MoveValidator : MonoBehaviour
{

    //Checks if a move is valid based on where a player is from to where and their rolls
    public bool IsValidTo(int[] rolls, int from, int to) {
        bool whitesTurn = this.gameObject.GetComponent<BoardController>().GetWhitesTurn();

        //The value we want to search for in the die rolls
        int searchValue;

        //Allowing only forward movement based on the turn
        if (whitesTurn)
        {
            searchValue = to - from;
        }
        else {
            searchValue = from - to;
        }

        //No going backwards
        if (searchValue <= 0) {
            return false;
        }

        //searching for valid move for the dice roll (linear search, only 4 values)
        for (int i = 0; i < 4; i++) {
            if (searchValue == rolls[i]) {
                return true;
            }
        }
        return false;
    }

    //Checks if the player can move from where they are trying
    public bool IsValidFrom(int from) {
        bool whitesTurn = this.gameObject.GetComponent<BoardController>().GetWhitesTurn();

        //Checking if the top piece is the users
        if (whitesTurn)
        {
            if (this.gameObject.GetComponent<BoardController>().GetTopColorOnEdge(from) != "white")
            {
                return false;
            }
        }
        else
        {
            if (this.gameObject.GetComponent<BoardController>().GetTopColorOnEdge(from) != "red")
            {
                return false;
            }
        }
        return true;

    }
}
