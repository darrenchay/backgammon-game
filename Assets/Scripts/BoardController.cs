using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * BOARD CONTROLLER
 * Used to control the board
 * **/


public class BoardController : MonoBehaviour
{
    public const int BACKGAMMON_EDGE_COUNT = 24;
    //Text and input used for demo
    public Text countText;
    public Text fromText;
    public Text toText;
    public Text edgeIndex;

    //Array of edges, each holding a stack
    private GameObject[] edges;

    // Start is called before the first frame update
    void Start()
    {
        //Initializing edges (24 edges per backgammon table)
        edges = new GameObject[BACKGAMMON_EDGE_COUNT];

        //Creating edges and positioning them

        //Creating top right corner
        for (int i = 0; i < 6; i++) {
            edges[i] = createEdge(i * 0.72f - 3.8f, - 2.5f, 0f, i);
        }
        //Creating top left corner (i + 1 skips the middle bar between sides)
        for (int i = 6; i < 12; i++)
        {
            edges[i] = createEdge((i + 1) * 0.72f - 3.8f, -2.5f, 0f, i);
        }
        //Creating bottom right corner (j + 1 skips the middle bar between sides)
        for (int j = 6; j < 12; j++)
        {
            edges[j + 12] = createEdge((-1 * (j + 1)) * 0.72f + 4.85f, 2f, 180f, j + 12);
        }
        //Creating bottom left corner (needs to be rotated 180 degrees)
        for (int j = 0; j < 6; j++) { 
            edges[j + 12] = createEdge((-1*j) * 0.72f + 4.8f, 2f, 180f, j + 12);
        }
    }

    public void clear_board() {
        for (int i = 0; i < BACKGAMMON_EDGE_COUNT; i++) {
            edges[i].GetComponent<Edge>().clear_edge();
        }
    }

    // adds pieces to edges according to default backgammon layout.
    public void setup_default_piece_positions() {
        clear_board();

        // place white pieces
        edges[0].GetComponent<Edge>().pushPiece("white");
        edges[0].GetComponent<Edge>().pushPiece("white");
        edges[11].GetComponent<Edge>().pushPiece("white");
        edges[11].GetComponent<Edge>().pushPiece("white");
        edges[11].GetComponent<Edge>().pushPiece("white");
        edges[11].GetComponent<Edge>().pushPiece("white");
        edges[11].GetComponent<Edge>().pushPiece("white");
        edges[16].GetComponent<Edge>().pushPiece("white");
        edges[16].GetComponent<Edge>().pushPiece("white");
        edges[16].GetComponent<Edge>().pushPiece("white");
        edges[18].GetComponent<Edge>().pushPiece("white");
        edges[18].GetComponent<Edge>().pushPiece("white");
        edges[18].GetComponent<Edge>().pushPiece("white");
        edges[18].GetComponent<Edge>().pushPiece("white");
        edges[18].GetComponent<Edge>().pushPiece("white");

        // TODO: add red pieces
    }

    // Update is called once per frame
    void Update()
    {
        //Updating the pice counter for the demo
        int number;
        if (edgeIndex.text.ToString() != "" && int.TryParse(edgeIndex.text.ToString(), out number) && number >= 0 && number < BACKGAMMON_EDGE_COUNT)
        {
            countText.text = "Edge " + int.Parse(edgeIndex.text.ToString()) + "| R: " + edges[int.Parse(edgeIndex.text.ToString())].GetComponent<Edge>().getRedCount() + "| W: " + edges[int.Parse(edgeIndex.text.ToString())].GetComponent<Edge>().getWhiteCount() + "";
        }
    }

    //Creates a new piece and adds it to the edge taken from user input
    public void addPieceToEdge(string color) {
        int number;
        //Making sure the input is an int and in range
        if (int.TryParse(edgeIndex.text.ToString(), out number) && number >= 0 && number < BACKGAMMON_EDGE_COUNT)
        {
            //Creating and pushing a piece of color "color" to the users stack (edge)
            edges[int.Parse(edgeIndex.text.ToString())].GetComponent<Edge>().pushPiece(color);
        }
    }

    //Creates an edge (visible for demo)
    private GameObject createEdge(float x, float y, float rotation, int i) {
        //Creating empty cube and changing position, scale, and rotation (for the bottom half of board)
        GameObject edge = GameObject.CreatePrimitive(PrimitiveType.Cube);
        edge.transform.position = new Vector3(x, 0, y);
        edge.transform.localScale = new Vector3(0.1f, 0.1f, 2f);
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
    public void movePiece() {
        int to;
        int from;
        if (int.TryParse(toText.text.ToString(), out to) && to >= 0 && to < BACKGAMMON_EDGE_COUNT)
        {
            //If the input is properly formatted
            if (int.TryParse(fromText.text.ToString(), out from) && from >= 0 && from < BACKGAMMON_EDGE_COUNT && from != to)
            {
                //Making sure the stack to be put onto is not full (so we do not take from and but not add to)
                if (edges[int.Parse(toText.text.ToString())].GetComponent<Edge>().getStackSize() < 30) {
                    //Poping piece from the "from" stack
                    GameObject piece = edges[int.Parse(fromText.text.ToString())].GetComponent<Edge>().popPiece();

                    //Making sure the stack is not empty
                    if (piece != null)
                    {
                        //Pushing piece to new stack
                        edges[int.Parse(toText.text.ToString())].GetComponent<Edge>().pushPiece(piece.GetComponent<Piece>().getColor());
                    }
                }
            }
        }
     }

}
