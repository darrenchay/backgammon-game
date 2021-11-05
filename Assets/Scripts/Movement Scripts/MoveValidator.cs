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
    //A piece can move if the following is true
    //1. The piece is going to an empty edge
    //2. The piece is going to an edge with one of the other color
    //3. the piece is going to an edge with only its own color
    //4. if the piece is not from the bar of the players color (unless they are moving the bar piece)
    public bool IsValidTo(int[] rolls, int from, int to) {

        bool whitesTurn = this.gameObject.GetComponent<BoardController>().GetWhitesTurn();

        //Normalizing if coming from the bar
        if (from == 26)
        {
            //if white next is zero if not next is 23
            if (whitesTurn)
            {
                from = -1; //next for white is 0
            }
            else
            {
                from = 24; //next for red is 23
            }
        }
        else {
            //Making sure the move cannot happen if a piece on the bar exists
            if (whitesTurn)
            {
                if (this.gameObject.GetComponent<BoardController>().GetEdgeWhiteCount(26) > 0)
                {
                    return false;
                }
            }
            else {
                if (this.gameObject.GetComponent<BoardController>().GetEdgeRedCount(26) > 0)
                {
                    return false;
                }
            }
            
        }

        //The value we want to search for in the die rolls
        int searchValue = 99; //Will give error if the bottom code fails

        //Allowing only forward movement based on the turn
        if (to != 24 && to != 25)
        {
            if (whitesTurn)
            {
                searchValue = to - from;
            }
            else
            {
                searchValue = from - to;
            }
        }
        //If the user is trying to move to a born off edge
        else {
            if (to == 24)
            {
                //Normalizing born off areas
                if (whitesTurn)
                {
                    searchValue = (-1) - from;
                }
                else
                {
                    searchValue = from - (-1);
                }
                //No going backwards or own edge
                if (searchValue <= 0)
                {
                    return false;
                }
                //Making sure the dice roll exists (equal to or greater than)
                if (SearchEdgeBaring(searchValue, rolls))
                {
                    //Get all top pieces of the players turn
                    List<int> possibleFrom = new List<int>();

                    //Getting all top pieces
                    for (int i = 0; i < 24; i++)
                    {
                        if (this.gameObject.GetComponent<BoardController>().GetTopColorOnEdge(i) == "red")
                        {
                            possibleFrom.Add(i);
                        }
                    }

                    //Checking if all red pieces are home 
                    return CheckHome(possibleFrom, "red");

                }
            }
            else if (to == 25)
            {
                if (whitesTurn)
                {
                    searchValue = (24) - from;
                }
                else
                {
                    searchValue = from - (24);
                }
                //No going backwards or own edge
                if (searchValue <= 0)
                {
                    return false;
                }
                //Making sure the dice roll exists (equal to or greater than)
                if (SearchEdgeBaring(searchValue, rolls))
                {
                    //Get all top pieces of the players turn
                    List<int> possibleFrom = new List<int>();

                    //Getting all top pieces
                    for (int i = 0; i < 24; i++)
                    {
                        if (this.gameObject.GetComponent<BoardController>().GetTopColorOnEdge(i) == "white")
                        {
                            possibleFrom.Add(i);
                        }
                    }

                    //Checking if all red pieces are home 
                    return CheckHome(possibleFrom, "white");
                }
            }
        }

        //No going backwards or own edge
        if (searchValue <= 0) {
            return false;
        }

        //Making sure the dice roll exists
        if (!SearchEdge(searchValue, rolls)) {
            return false;
        }

        //Check if edge[to] stacksize == 0
        if (this.gameObject.GetComponent<BoardController>().GetEdgeStackSize(to) == 0) {
            return true;
        }

        //Check if other player color stack count == 0
        if (whitesTurn && this.gameObject.GetComponent<BoardController>().GetEdgeRedCount(to) == 0)
        {
            return true;
        }
        if (!whitesTurn && this.gameObject.GetComponent<BoardController>().GetEdgeWhiteCount(to) == 0)
        {
            return true;
        }

        //Check if edge only contains other player color
        if (whitesTurn && this.gameObject.GetComponent<BoardController>().GetEdgeRedCount(to) == 1)
        {
            return true;
        }
        else if (!whitesTurn && this.gameObject.GetComponent<BoardController>().GetEdgeWhiteCount(to) == 1)
        {
            return true;
        }

        //default value 
        return false;

    }

    //Need to make the main function know if it was a home piece and if so then find the first instance greater or equal
    private bool SearchEdgeBaring(int searchValue, int[] rolls)
    {
        //searching for valid move for the dice roll (linear search, only 4 values)
        for (int i = 0; i < 4; i++)
        {
            if (searchValue <= rolls[i])
            {
                return true;
            }
        }
        return false;
    }

    private bool SearchEdge(int searchValue, int[] rolls) {
        //searching for valid move for the dice roll (linear search, only 4 values)
        for (int i = 0; i < 4; i++)
        {
            if (searchValue == rolls[i])
            {
                return true;
            }
        }
        return false;
    }

    //Checks if the player can move from where they are trying
    public bool IsValidFrom(int from) {
        bool whitesTurn = this.gameObject.GetComponent<BoardController>().GetWhitesTurn();

        //A player should not be able to move from the born off zone
        if (from == 24 || from == 25) {
            return false;
        }

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

    //Determines if a valid move exists based on the current board and the users roll
    public bool ValidMoveExists(int[] rolls) {
        bool whitesTurn = this.gameObject.GetComponent<BoardController>().GetWhitesTurn();
        string playerColor;
        
        //Determining the turn for easier equating
        if (whitesTurn) {
            playerColor = "white";
        } else {
            playerColor = "red";
        }

        //If there is a bar piece for the current user they MUST take it off, so if there is one and there are no valid moves we skip turn
        if (playerColor == "white" && this.gameObject.GetComponent<BoardController>().GetEdgeWhiteCount(26) > 0 || playerColor == "red" && this.gameObject.GetComponent<BoardController>().GetEdgeRedCount(26) > 0) {
            return CheckBar(playerColor, rolls);
        }

        //Get all top pieces of the players turn
        List<int> possibleFrom = new List<int>();

        //Getting all top pieces
        for (int i = 0; i < 24; i++) {
            if (this.gameObject.GetComponent<BoardController>().GetTopColorOnEdge(i) == playerColor) {
                possibleFrom.Add(i);
            }
        }

        //Check the base board, if not check that the player is home and if there is a valid barinmg off move
        return CheckBoard(possibleFrom, rolls) || (CheckHome(possibleFrom, playerColor) && CheckBaring(possibleFrom, playerColor, rolls));
    }

    //Checks if there is a valid move from the bar
    private bool CheckBar(string playerColor, int[] rolls) {
            for (int i = 0; i < 24; i++)
            {
                if (IsValidTo(rolls, 26, i))
                {
                    return true;
                }
            }
        return false;
    }

    //Checks if there is a valid move on the original 24 edges
    private bool CheckBoard(List<int> possibleFrom, int[] rolls)
    {
        foreach (int i in possibleFrom)
        {
            for (int j = 0; j < 24; j++)
            {
                if (IsValidTo(rolls, i, j))
                {
                    return true;
                }
            }
        }
        return false;
    }

    //Checks if the player is all home
    private bool CheckHome(List<int> possibleFrom, string playerColor) {
        if (playerColor == "white")
        {
            foreach (int i in possibleFrom)
            {
                if (i < 18)
                {
                    return false;
                }
            }
        }
        else
        {
            foreach (int i in possibleFrom)
            {
                if (i > 5)
                {
                    return false;
                }
            }
        }
        return true;
    }

    //Checks if the player has a valid born off move
    private bool CheckBaring(List<int> possibleFrom, string playerColor, int[] rolls) {
        if (playerColor == "white")
        {
            foreach (int i in possibleFrom)
            {
                if (IsValidTo(rolls, i, 25))
                {
                    return true;
                }
            }
        }
        else
        {
            foreach (int i in possibleFrom)
            {
                if (IsValidTo(rolls, i, 24))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
