using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementManager : MonoBehaviour
{

    //Holds the current roll from either user
    private int[] completeRoll;

    //Dice gameobject used to see if they are still moving
    public GameObject dice1;
    public GameObject dice2;

    //Dice face checker used to get dice values
    public GameObject diceFaceChecker;

    //Determines if it is a new roll
    private bool newRoll;

    //Text for the current roll values (seen on the demo screen)
    public Text rollsArray;

    //-1 if the user has not selected, also holds the edge the user wants to move from
    private int allowMovementFrom;

    // Start is called before the first frame update
    void Start()
    {
        //initilaizing values
        newRoll = true;
        completeRoll = new int[4] {-1, -1, -1, -1};
        allowMovementFrom = -1;
    }

    // Update is called once per frame
    void Update()
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
                    if (allowMovementFrom != -1) {
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
                    else {
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
                        //Move piece and deselect
                        this.gameObject.GetComponent<BoardController>().MovePiece(allowMovementFrom, movementTo);
                        this.gameObject.GetComponent<BoardController>().DeselectTopPiece(movementTo);
                    
                        //Destroy the dice move
                        SearchAndDestroyRoll(Mathf.Abs(movementTo - allowMovementFrom));

                        //Changing turns if the user is out of moves
                        if (this.gameObject.GetComponent<BoardController>().GetWhitesTurn())
                        {
                            if (UsedAllMoves()) {
                                this.gameObject.GetComponent<BoardController>().RedsTurn();
                            }
                        }
                        else {
                            if (UsedAllMoves())
                            {
                                this.gameObject.GetComponent<BoardController>().WhitesTurn();
                            }
                        }
                        //reset
                        allowMovementFrom = -1;
                    }
                }

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
        }

        //Setting demo text
        rollsArray.text = "Your Rolls: { " + completeRoll[0] + ", " + completeRoll[1] + ", " + completeRoll[2] + ", " + completeRoll[3] + "}";
    }

    //Coroutine used in order to allow a wait in the update function
    IEnumerator UpdateDiceRolls()
    {
        //Waits half a second
        yield return new WaitForSeconds(0.5f);
        //Sets the first two dice elements
        completeRoll[0] = diceFaceChecker.GetComponent<DiceFaceCheck>().GetDice1Num();
        completeRoll[1] = diceFaceChecker.GetComponent<DiceFaceCheck>().GetDice2Num();

        //*JAMES: double logic should be able to go here by setting the last two elements

        //It is no longer a new roll
        newRoll = false;
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
}
