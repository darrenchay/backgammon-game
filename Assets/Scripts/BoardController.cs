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
        edges = new GameObject[24];

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
        //Creating bottom right corner (needs to be rotated 180 degrees)
        for (int j = 0; j < 6; j++) { 
            edges[j + 12] = createEdge(j * 0.72f - 3.8f, 2f, 180f, j + 12);
        }
        //Creating bottom left corner (j + 1 skips the middle bar between sides)
        for (int j = 6; j < 12; j++)
        {
            edges[j + 12] = createEdge((j+1) * 0.72f - 3.8f, 2f, 180f, j + 12);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Updating the pice counter for the demo
        int number;
        if (edgeIndex.text.ToString() != "" && int.TryParse(edgeIndex.text.ToString(), out number) && number >= 0 && number < 24)
        {
            countText.text = "Edge " + int.Parse(edgeIndex.text.ToString()) + "| R: " + edges[int.Parse(edgeIndex.text.ToString())].GetComponent<Edge>().getRedCount() + "| W: " + edges[int.Parse(edgeIndex.text.ToString())].GetComponent<Edge>().getWhiteCount() + "";
        }
        }

    //Creates a new piece and adds it to the edge taken from user input
    public void addPieceToEdge(string color) {
        int number;
        //Making sure the input is an int and in range
        if (int.TryParse(edgeIndex.text.ToString(), out number) && number >= 0 && number < 24)
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

        return edge;
    }

    /**Moves a piece from user input to user input stack
     * to: toText
     * from: fromText
     * **/
    public void movePiece() {
        int to;
        int from;
        if (int.TryParse(toText.text.ToString(), out to) && to >= 0 && to < 24)
        {
            //If the input is properly formatted
            if (int.TryParse(fromText.text.ToString(), out from) && from >= 0 && from < 24 && from != to)
            {
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
