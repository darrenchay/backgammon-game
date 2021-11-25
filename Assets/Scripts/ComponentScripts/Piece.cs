using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * PIECE CLASS
 * **/

public class Piece : MonoBehaviour
{
    //Color attribute (red or white)
    private string color;

    //Sets the color of the mesh per object (red or white)
    public void SetColor(string color)
    {
        /**
        if (color == "red")
        {
            if (gameObject.GetComponent<MeshRenderer>() != null)
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        else if (color == "white")
        {
            if (gameObject.GetComponent<MeshRenderer>() != null)
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        **/
        this.color = color;
    }

    //Getter
    public string GetColor()
    {
        return this.color;
    }
}
