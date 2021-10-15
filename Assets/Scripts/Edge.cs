using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * EDGE CLASS
 * **/

public class Edge : MonoBehaviour
{
    //Stack used per edge
    public Stack<GameObject> pieces = new Stack<GameObject>();

    //Used to count red and white pieces in the stack
    private int redCount;
    private int whiteCount;

    //Start is called before the first frame update
    void Start()
    {
        redCount = 0;
        whiteCount = 0;
    }

    //Used to create a piece
    private GameObject createPiece(string color) 
    {
        GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        piece.transform.localScale = new Vector3(0.5f, 0.01f, 0.5f);
        piece.transform.parent = gameObject.transform;
        piece.transform.localPosition = new Vector3(0, 0.5f, this.pieces.Count * 0.26f - 0.5f);
        piece.AddComponent<Piece>();
        piece.GetComponent<Piece>().setColor(color);

        return piece;
    }

    //Pushes a new piece to the stack based on the color
    public void pushPiece(string color) 
    {
        //Making sure stack is not full (max number of backgammon pieces)
        if (this.pieces.Count == 30) {
            return;
        }

        //increasing counts
        if (color == "red")
        {
            redCount++;
        }
        else if (color == "white") {
            whiteCount++;
        }

        //Creating piece
        GameObject piece = createPiece(color);

        //Pusing piece
        this.pieces.Push(piece);
    }

    //Pops the stack and destroys the piece to be copied to another stack
    public GameObject popPiece()
    {
        //If the stack is empty return null
        if (this.pieces.Count == 0) {
            return null;
        }

        //Getting top of stack and popping
        GameObject temp = this.pieces.Pop();

        //Decreasing counters
        if (temp.GetComponent<Piece>().getColor() == "red")
        {
            redCount--;
        }
        else if (temp.GetComponent<Piece>().getColor() == "white") {
            whiteCount--;
        }

        //Destroying top of stack so it does not show on the board
        GameObject ret = temp;
        Destroy(temp);

        //Returning a copy of the piece (for movement)
        return ret;
    }

    public void clearEdge() 
    {
        foreach(GameObject piece in pieces) {
            Destroy(piece);
        }
        this.pieces = new Stack<GameObject>();
        this.redCount = 0;
        this.whiteCount = 0;
    }

    //Getter
    public int getRedCount() {
        return this.redCount;
    }
    //Getter
    public int getWhiteCount() {
        return this.whiteCount;
    }
    //Getter
    public int getStackSize() {
        return this.pieces.Count;
    }
 }
