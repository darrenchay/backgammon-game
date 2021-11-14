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
    private GameObject CreatePiece(string color)
    {
        GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        piece.transform.localScale = new Vector3(0.5f, 0.01f, 0.5f);
        piece.transform.parent = gameObject.transform;

        //if on bar
        if (this.gameObject.name.Substring(5) == "26")
        {
            piece.transform.localPosition = new Vector3(0, 2.7f, this.pieces.Count * 0.26f - 0.5f);
        }
        //If on born off zones
        else if (this.gameObject.name.Substring(5) == "25" || this.gameObject.name.Substring(5) == "24")
        {
            piece.transform.localPosition = new Vector3(0, 2.5f, this.pieces.Count * 0.26f - 0.5f);
        }
        else
        {
            piece.transform.localPosition = new Vector3(0, 0.5f, this.pieces.Count * 0.26f - 0.5f);
        }

        piece.AddComponent<Piece>();
        piece.GetComponent<Piece>().SetColor(color);
        //Removing collider so dice pass through
        piece.GetComponent<CapsuleCollider>().enabled = false;
        //So that the pieces dont get hit with a raycast (clicking for movement)
        piece.layer = 2;

        return piece;
    }

    //Pushes a new piece to the stack based on the color
    public void PushPiece(string color)
    {
        //Making sure stack is not full (max number of backgammon pieces)
        if (this.pieces.Count == 30)
        {
            return;
        }

        //increasing counts
        if (color == "red")
        {
            redCount++;
        }
        else if (color == "white")
        {
            whiteCount++;
        }

        //Creating piece
        GameObject piece = CreatePiece(color);

        //Pusing piece
        this.pieces.Push(piece);
    }

    public GameObject Peek()
    {
        return this.pieces.Peek();
    }

    //Pops the stack and destroys the piece to be copied to another stack
    public GameObject PopPiece()
    {
        //If the stack is empty return null
        if (this.pieces.Count == 0)
        {
            return null;
        }

        //Getting top of stack and popping
        GameObject temp = this.pieces.Pop();

        //Decreasing counters
        if (temp.GetComponent<Piece>().GetColor() == "red")
        {
            redCount--;
        }
        else if (temp.GetComponent<Piece>().GetColor() == "white")
        {
            whiteCount--;
        }

        //Destroying top of stack so it does not show on the board
        GameObject ret = temp;
        Destroy(temp);

        //Returning a copy of the piece (for movement)
        return ret;
    }

    public void ClearEdge()
    {
        foreach (GameObject piece in pieces)
        {
            Destroy(piece);
        }
        this.pieces = new Stack<GameObject>();
        this.redCount = 0;
        this.whiteCount = 0;
    }

    //Getter
    public int GetRedCount()
    {
        return this.redCount;
    }
    //Getter
    public int GetWhiteCount()
    {
        return this.whiteCount;
    }
    //Getter
    public int GetStackSize()
    {
        return this.pieces.Count;
    }
}
