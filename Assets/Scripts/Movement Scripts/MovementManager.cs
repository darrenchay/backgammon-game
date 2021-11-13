using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovementManager : MonoBehaviour
{

    //Holds the current roll from either user
    private int[] completeRoll;

    //Dice gameobject used to see if they are still moving
    public GameObject dice1;
    public GameObject dice2;

    //Dice face checker used to get dice values
    public GameObject diceFaceChecker;

    //The dice spawn gameobject so we can force the dice there
    public GameObject dropDiceLocation;

    //Determines if it is a new roll
    private bool newRoll;

    //Text for the current roll values (seen on the demo screen)
    public Text rollsArray;

    //-1 if the user has not selected, also holds the edge the user wants to move from
    private int allowMovementFrom;

    //Roll button text
    public Text rollText;
    private bool turnDecided;
    public GameObject turnPrompt;
    public GameObject diceRollVisual;
    public GameObject concede;

    //To determine the current scene
    private Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        //Getting active scene to tell if its demo or real 
        scene = SceneManager.GetActiveScene();

        //initilaizing values
        newRoll = true;
        completeRoll = new int[4] {-1, -1, -1, -1};
        allowMovementFrom = -1;
        turnDecided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (turnDecided)
        {
            //User clicks an edge skeleton (invisible)
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    //If a user clicks and its not an edge then reset the move 
                    if (hit.transform.name.Substring(0, 4) == null || hit.transform.name.Substring(0, 4) != "Edge")
                    {
                        //if user clicks away to deselect
                        if (allowMovementFrom != -1)
                        {
                            //Deselect top piece
                            this.gameObject.GetComponent<BoardController>().DeselectTopPiece(allowMovementFrom);
                            //reset
                            allowMovementFrom = -1;
                        }

                    }
                    //Checking if a user can move from the selected edge
                    else if (hit.transform.name.Substring(0, 4) == "Edge" && allowMovementFrom == -1 && !UsedAllMoves())
                    {
                        allowMovementFrom = int.Parse(hit.transform.name.Substring(5));
                        if (this.gameObject.GetComponent<MoveValidator>().IsValidFrom(allowMovementFrom))
                        {
                            this.gameObject.GetComponent<BoardController>().SelectTopPiece(allowMovementFrom);
                        }
                        else
                        {
                            //Else reset
                            allowMovementFrom = -1;
                        }
                    }
                    //Checking if a user can move to a specific edge
                    else if (hit.transform.name.Substring(0, 4) == "Edge" && allowMovementFrom != -1)
                    {
                        //Move piece to movementTo
                        int movementTo = int.Parse(hit.transform.name.Substring(5));
                        if (this.gameObject.GetComponent<MoveValidator>().IsValidTo(completeRoll, allowMovementFrom, movementTo))
                        {

                            //To check if we have a swap in order to skip a double move
                            bool swapOccured = false;

                            //We need to check if we swap and not move and unmove (BUG FIX)
                            if (this.gameObject.GetComponent<BoardController>().GetWhitesTurn() && this.gameObject.GetComponent<BoardController>().GetEdgeRedCount(movementTo) == 1 && allowMovementFrom == 26)
                            {
                                this.gameObject.GetComponent<BoardController>().SwapPiece(movementTo, 26);
                                swapOccured = true;
                            }
                            else if (!this.gameObject.GetComponent<BoardController>().GetWhitesTurn() && this.gameObject.GetComponent<BoardController>().GetEdgeWhiteCount(movementTo) == 1 && allowMovementFrom == 26)
                            {
                                this.gameObject.GetComponent<BoardController>().SwapPiece(movementTo, 26);
                            }
                            //Check if edge only contains other player color and seeing if a piece needs to be added to the bar
                            else if (this.gameObject.GetComponent<BoardController>().GetWhitesTurn() && this.gameObject.GetComponent<BoardController>().GetEdgeRedCount(movementTo) == 1)
                            {
                                //Move top piece to the bar
                                this.gameObject.GetComponent<BoardController>().MovePiece(movementTo, 26);
                            }
                            else if (!this.gameObject.GetComponent<BoardController>().GetWhitesTurn() && this.gameObject.GetComponent<BoardController>().GetEdgeWhiteCount(movementTo) == 1)
                            {
                                //Move top piece to the bar
                                this.gameObject.GetComponent<BoardController>().MovePiece(movementTo, 26);
                            }

                            if (!swapOccured)
                            {
                                //Move piece and deselect
                                this.gameObject.GetComponent<BoardController>().MovePiece(allowMovementFrom, movementTo);
                                this.gameObject.GetComponent<BoardController>().DeselectTopPiece(movementTo);
                            }

                            //Destroy the dice move (first greates value if baring off)
                            if (movementTo == 24)
                            {
                                SearchAndDestroyGreaterRoll(Mathf.Abs((-1) - allowMovementFrom));
                            }
                            else if (allowMovementFrom == 26 && this.gameObject.GetComponent<BoardController>().GetWhitesTurn())
                            {
                                SearchAndDestroyRoll(Mathf.Abs(movementTo - (-1)));
                            }
                            else if (allowMovementFrom == 26 && !this.gameObject.GetComponent<BoardController>().GetWhitesTurn())
                            {
                                SearchAndDestroyRoll(Mathf.Abs(movementTo - (24)));
                            }
                            else if (movementTo == 25)
                            {
                                SearchAndDestroyGreaterRoll(Mathf.Abs((24) - allowMovementFrom));
                            }
                            else
                            {
                                SearchAndDestroyRoll(Mathf.Abs(movementTo - allowMovementFrom));
                            }

                            //Changing turns if the user is out of moves or user cannot move anymore
                            if (this.gameObject.GetComponent<BoardController>().GetWhitesTurn())
                            {
                                if (UsedAllMoves())
                                {
                                    this.gameObject.GetComponent<BoardController>().RedsTurn();
                                }
                                else
                                {
                                    //Checking if user can still make a move
                                    if (!this.gameObject.GetComponent<MoveValidator>().ValidMoveExists(completeRoll))
                                    {
                                        ResetAndSwitchTurns();
                                    }
                                }
                            }
                            else
                            {
                                if (UsedAllMoves())
                                {
                                    this.gameObject.GetComponent<BoardController>().WhitesTurn();
                                }
                                else
                                {
                                    //Checking if user can still make a move
                                    if (!this.gameObject.GetComponent<MoveValidator>().ValidMoveExists(completeRoll))
                                    {
                                        ResetAndSwitchTurns();
                                    }
                                }
                            }

                            //reset
                            allowMovementFrom = -1;
                        }
                    }

                }
            }
        }
        else {
            //If turns are decided
            if (CheckRollForTurn() == true) {
                if (scene.name == "GameScene")
                {
                    //Showing turn prompt after turn choose
                    turnPrompt.SetActive(true);
                    diceRollVisual.SetActive(true);
                    concede.SetActive(true);
                }
                turnDecided = true;
                rollText.text = "Roll";
            }
        }


        //Checkinging if dice are done moving
        if (dice1.GetComponent<Rigidbody>().velocity.magnitude > 0.01f || dice2.GetComponent<Rigidbody>().velocity.magnitude > 0.01f)
        {

            //Resetiong the roll array if the dice are in movement
            completeRoll = new int[4] { -1, -1, -1, -1 };
            //It is a new roll
            newRoll = true;
        }
        else
        {
            //Checks if the dice are touching the board
            if (diceFaceChecker.GetComponent<DiceFaceCheck>().GetIsTouchingBoard())
            {
                //So we only update the array once
                if (newRoll)
                {
                    //Staring coroutine in order to wait for the dice to settle
                    StartCoroutine("UpdateDiceRolls");
                }
            }
            else
            {
                //If in the air there should always be no roll values
                completeRoll = new int[4] { -1, -1, -1, -1 };
            }
        }

        //Setting demo text
        if (scene.name == "Edge-Demo")
        {
            rollsArray.text = "Your Rolls: { " + completeRoll[0] + ", " + completeRoll[1] + ", " + completeRoll[2] + ", " + completeRoll[3] + "}";
        }
     }

    //Coroutine used in order to allow a wait in the update function
    IEnumerator UpdateDiceRolls()
    {
        //Waits half a second
        yield return new WaitForSeconds(0.5f);
        //Sets the first two dice elements
        completeRoll[0] = diceFaceChecker.GetComponent<DiceFaceCheck>().GetDice1Num();
        completeRoll[1] = diceFaceChecker.GetComponent<DiceFaceCheck>().GetDice2Num();

        //Hide dice after roll
        dropDiceLocation.GetComponent<DropDice>().HideDice();

        // check for double roll
        if (completeRoll[0] == completeRoll[1])
        {
            var val = completeRoll[0];
            completeRoll[2] = val;
            completeRoll[3] = val;
        }

        //Checking if there are no valid moves
        //Writing here means a single check of the more intense computation

        if (!this.gameObject.GetComponent<MoveValidator>().ValidMoveExists(completeRoll))
        {
            ResetAndSwitchTurns();
        }
        else {
            //It is no longer a new roll
            newRoll = false;
        }

    }

    //Checks the dice roll values to determine who plays first
    public bool CheckRollForTurn()
    {
        if (completeRoll[0] > completeRoll[1])
        {
            this.gameObject.GetComponent<BoardController>().WhitesTurn();
            return true;
        }
        else if (completeRoll[0] < completeRoll[1])
        {
            this.gameObject.GetComponent<BoardController>().RedsTurn();
            return true;
        }
        //If values are the same, reset and re-roll the dice
        //TODO - There are some scenarios where this triggers when the dice rolls are reset to -1. Those bugs should probably be fixed to determine if this requires addressing
        else
        {
            return false;
        }
    }

    //Resets values and switches turns
    private void ResetAndSwitchTurns() {

        newRoll = true;
        completeRoll = new int[4] { -1, -1, -1, -1 };

        dropDiceLocation.GetComponent<DropDice>().Roll();
        dropDiceLocation.GetComponent<DropDice>().HideDice();
        dropDiceLocation.GetComponent<DropDice>().FreezeDice();

        this.gameObject.GetComponent<BoardController>().ToggleTurn();
    }

    //Destroy the first instance of the find value
    private void SearchAndDestroyRoll(int find) { 
        for (int i = 0; i < 4; i++) {
            if (this.completeRoll[i] == find) {
                this.completeRoll[i] = -1;
                return;
            }
        }
    }

    //Destroy the first instance greater then or equal to the find value
    private void SearchAndDestroyGreaterRoll(int find)
    {
        for (int i = 0; i < 4; i++)
        {
            if (this.completeRoll[i] >= find)
            {
                this.completeRoll[i] = -1;
                return;
            }
        }
    }

    //Setter
    public void SetNewRoll(bool newRoll) {
        this.newRoll = newRoll;
    }

    //Determines if a user used all moves
    public bool UsedAllMoves() {
        for (int i = 0; i < 4; i++)
        {
            if (this.completeRoll[i] != -1)
            {
                return false;
            }
        }
        return true;
    }

    public int[] GetCompleteRoll() {
        return this.completeRoll;
    }
}
