using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * BOARD CONTROLLER
 * Used to control the board
 * **/

public class BoardController : MonoBehaviour
{

    //Text and input used for demo
    public Text countText;
    public Text fromText;
    public Text toText;
    public Text edgeIndex;

    //Winning UI
    public GameObject edgeNumbers;
    public GameObject winScreen;
    public GameObject winSymbol;
    public GameObject gameUI;
    public Text winText;

    //Counter text for born off areas
    public GameObject redBornCount;
    public GameObject whiteBornCount;

    //Number of edges total + born off slots + bar = 24 + 2 + 1 = 27
    private int edgeCount = 27;

    //Array of edges, each holding a stack
    private GameObject[] edges;

    private bool whitesTurn;

    // Start is called before the first frame update
    void Start()
    {
        whitesTurn = true;

        //Initializing edges (24 edges per backgammon table and 2 born off spaces)
        edges = new GameObject[edgeCount];

        //Creating edges and positioning them

        //Creating top right corner
        for (int i = 0; i < 6; i++)
        {
            edges[i] = CreateEdge(i * 0.72f - 3.8f, -2.5f, 0f, i);
        }
        //Creating top left corner (i + 1 skips the middle bar between sides)
        for (int i = 6; i < 12; i++)
        {
            edges[i] = CreateEdge((i + 1) * 0.72f - 3.8f, -2.5f, 0f, i);
        }
        //Creating bottom right corner (j + 1 skips the middle bar between sides)
        for (int j = 6; j < 12; j++)
        {
            edges[j + 12] = CreateEdge((-1 * (j + 1)) * 0.72f + 4.85f, 2f, 180f, j + 12);
        }
        //Creating bottom left corner (needs to be rotated 180 degrees)
        for (int j = 0; j < 6; j++)
        {
            edges[j + 12] = CreateEdge((-1 * j) * 0.72f + 4.8f, 2f, 180f, j + 12);
        }

        //Creating Born Off "edges"
        edges[24] = CreateEdge(-5f, -2.5f, 180f, 24);
        edges[25] = CreateEdge(-5f, 2f, 0f, 25);

        //Creating Bar
        edges[26] = CreateEdge((-1 * (6)) * 0.72f + 4.85f, 0f, 0f, 26);


    }

    public void SetupDefaultPiecePositions()
    {
        // clear all the edges
        for (int i = 0; i < edgeCount; i++)
        {
            edges[i].GetComponent<Edge>().ClearEdge();
        }

        // place white pieces
        edges[0].GetComponent<Edge>().PushPiece("white");
        edges[0].GetComponent<Edge>().PushPiece("white");
        edges[11].GetComponent<Edge>().PushPiece("white");
        edges[11].GetComponent<Edge>().PushPiece("white");
        edges[11].GetComponent<Edge>().PushPiece("white");
        edges[11].GetComponent<Edge>().PushPiece("white");
        edges[11].GetComponent<Edge>().PushPiece("white");
        edges[16].GetComponent<Edge>().PushPiece("white");
        edges[16].GetComponent<Edge>().PushPiece("white");
        edges[16].GetComponent<Edge>().PushPiece("white");
        edges[18].GetComponent<Edge>().PushPiece("white");
        edges[18].GetComponent<Edge>().PushPiece("white");
        edges[18].GetComponent<Edge>().PushPiece("white");
        edges[18].GetComponent<Edge>().PushPiece("white");
        edges[18].GetComponent<Edge>().PushPiece("white");

        edges[5].GetComponent<Edge>().PushPiece("red");
        edges[5].GetComponent<Edge>().PushPiece("red");
        edges[5].GetComponent<Edge>().PushPiece("red");
        edges[5].GetComponent<Edge>().PushPiece("red");
        edges[5].GetComponent<Edge>().PushPiece("red");
        edges[7].GetComponent<Edge>().PushPiece("red");
        edges[7].GetComponent<Edge>().PushPiece("red");
        edges[7].GetComponent<Edge>().PushPiece("red");
        edges[12].GetComponent<Edge>().PushPiece("red");
        edges[12].GetComponent<Edge>().PushPiece("red");
        edges[12].GetComponent<Edge>().PushPiece("red");
        edges[12].GetComponent<Edge>().PushPiece("red");
        edges[12].GetComponent<Edge>().PushPiece("red");
        edges[23].GetComponent<Edge>().PushPiece("red");
        edges[23].GetComponent<Edge>().PushPiece("red");
    }

    // Update is called once per frame
    void Update()
    {
        //Updating the pice counter for the demo
        int number;
        if (edgeIndex.text.ToString() != "" && int.TryParse(edgeIndex.text.ToString(), out number) && number >= 0 && number < edgeCount)
        {
            countText.text = "Edge " + int.Parse(edgeIndex.text.ToString()) + "| R: " + edges[int.Parse(edgeIndex.text.ToString())].GetComponent<Edge>().GetRedCount() + "| W: " + edges[int.Parse(edgeIndex.text.ToString())].GetComponent<Edge>().GetWhiteCount() + "";
        }

        //Updating red and white born off counts
        redBornCount.GetComponent<TextMesh>().text = edges[24].GetComponent<Edge>().GetStackSize().ToString();
        whiteBornCount.GetComponent<TextMesh>().text = edges[25].GetComponent<Edge>().GetStackSize().ToString();


        //Checking if red won
        if (edges[24].GetComponent<Edge>().GetStackSize() == 15)
        {
            RedWins();
        }

        //Checking if white won
        if (edges[25].GetComponent<Edge>().GetStackSize() == 15)
        {
            WhiteWins();
        }
    }

    //Screen when red wins
    public void RedWins()
    {
        //Hiding and showing needed screens
        edgeNumbers.SetActive(false);
        winScreen.SetActive(true);
        gameUI.SetActive(false);
        //Changing winning symbol color 
        winSymbol.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        winText.text = "RED WINS!!!";
    }
    //Screen when white wins
    public void WhiteWins()
    {
        //Hiding and showing needed screens
        edgeNumbers.SetActive(false);
        winScreen.SetActive(true);
        gameUI.SetActive(false);
        //Changing winning symbol color 
        winSymbol.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        winText.text = "WHITE WINS!!!";
    }

    //Restarts the scene
    public void Restart()
    {
        SceneManager.LoadScene("Edge-Demo");
    }

    //Creates a new piece and adds it to the edge taken from user input
    public void AddPieceToEdge(string color)
    {
        int number;
        //Making sure the input is an int and in range
        if (int.TryParse(edgeIndex.text.ToString(), out number) && number >= 0 && number < edgeCount)
        {
            //Creating and pushing a piece of color "color" to the users stack (edge)
            edges[int.Parse(edgeIndex.text.ToString())].GetComponent<Edge>().PushPiece(color);
        }
    }

    //Creates an edge (visible for demo)
    private GameObject CreateEdge(float x, float y, float rotation, int i)
    {
        //Creating empty cube and changing position, scale, and rotation (for the bottom half of board)
        GameObject edge = GameObject.CreatePrimitive(PrimitiveType.Cube);
        edge.transform.position = new Vector3(x, 0, y);
        edge.transform.localScale = new Vector3(0.4f, 0.1f, 2f);
        edge.transform.Rotate(0.0f, rotation, 0.0f, Space.World);

        //Making the edge a child of the board
        edge.transform.parent = gameObject.transform;
        //Adding script 
        edge.AddComponent<Edge>();

        //Naming
        edge.name = "Edge " + i;

        edge.GetComponent<Renderer>().enabled = false; //Comment to show edge skeleton

        return edge;
    }


    /**Moves a piece from user input to user input stack
     * to: toText
     * from: fromText
     * **/
    public void MovePiece()
    {
        int to;
        int from;
        if (int.TryParse(toText.text.ToString(), out to) && to >= 0 && to < edgeCount)
        {
            //If the input is properly formatted
            if (int.TryParse(fromText.text.ToString(), out from) && from >= 0 && from < edgeCount && from != to)
            {
                //Making sure the stack to be put onto is not full (so we do not take from but not add to)
                if (edges[int.Parse(toText.text.ToString())].GetComponent<Edge>().GetStackSize() < 30)
                {
                    //Poping piece from the "from" stack
                    GameObject piece = edges[int.Parse(fromText.text.ToString())].GetComponent<Edge>().PopPiece();

                    //Making sure the stack is not empty
                    if (piece != null)
                    {
                        //Pushing piece to new stack
                        edges[int.Parse(toText.text.ToString())].GetComponent<Edge>().PushPiece(piece.GetComponent<Piece>().GetColor());
                    }
                }
            }
        }
    }

    /**Moves a piece from dice roll to user input stack
     * to: toText
     * from: fromText
     * **/
    public void MovePiece(int from, int to)
    {
        if (to >= 0 && to < edgeCount)
        {
            //If the input is properly formatted
            if (from >= 0 && from < edgeCount && from != to)
            {
                //Making sure the stack to be put onto is not full (so we do not take from but not add to)
                if (edges[to].GetComponent<Edge>().GetStackSize() < 30)
                {
                    //Poping piece from the "from" stack
                    GameObject piece = edges[from].GetComponent<Edge>().PopPiece();

                    //Making sure the stack is not empty
                    if (piece != null)
                    {
                        //Pushing piece to new stack
                        edges[to].GetComponent<Edge>().PushPiece(piece.GetComponent<Piece>().GetColor());
                    }
                }
            }
        }
    }

    //Gets whos turn it is
    public bool GetWhitesTurn() {
        return this.whitesTurn;
    }

    //Returns the top color of a certain stack
    public string GetTopColorOnEdge(int edgeIndex) {

        //Checking that the stack is not empty
        if (this.edges[edgeIndex].GetComponent<Edge>().GetStackSize() == 0) {
            return "";
        }

        //Peeking the stack
        GameObject tmp = this.edges[edgeIndex].GetComponent<Edge>().Peek();
        
        //returning the color
        return tmp.GetComponent<Piece>().GetColor();
    }

    //Tints the top piece of a stack to show the user selected it
    public void SelectTopPiece(int edgeIndex) {
        //Making sure the stack is not empty (we have a piece to select)
        if (this.edges[edgeIndex].GetComponent<Edge>().GetStackSize() == 0)
        {
            return;
        }
        GameObject tmp = this.edges[edgeIndex].GetComponent<Edge>().Peek();

        if (tmp.GetComponent<Piece>().GetColor() == "white")
        {
            tmp.GetComponent<Renderer>().material.color = new Color(200f / 255f, 200f / 255f, 200f / 255f); 
        }
        else if (tmp.GetComponent<Piece>().GetColor() == "red")
        {
            tmp.GetComponent<Renderer>().material.color = new Color(200f / 255f, 0f, 0f);
        }
    }

    //Untints the top piece of a stack to show the user deselected it
    public void DeselectTopPiece(int edgeIndex) {
        //Making sure the stack is not empty (we have a piece to deselect)
        if (this.edges[edgeIndex].GetComponent<Edge>().GetStackSize() == 0)
        {
            return;
        }
        GameObject tmp = this.edges[edgeIndex].GetComponent<Edge>().Peek();

        if (tmp.GetComponent<Piece>().GetColor() == "white") {
            tmp.GetComponent<Renderer>().material.color = Color.white;
        } else if (tmp.GetComponent<Piece>().GetColor() == "red")
         {
            tmp.GetComponent<Renderer>().material.color = Color.red;
         }
    }

    //Making it whites turn
    public void WhitesTurn() {
        this.whitesTurn = true;
    }

    //Making it reds turn
    public void RedsTurn()
    {
        this.whitesTurn = false;
    }

}
