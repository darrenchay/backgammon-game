using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public List<Vector3> directions;
    public List<int> sideValue;


    void Awake()
    {
        if(directions.Count == 0)
        {
            directions.Add(Vector3.up);
            sideValue.Add(6);

            directions.Add(Vector3.down);
            sideValue.Add(1);

            directions.Add(Vector3.left);
            sideValue.Add(2);

            directions.Add(Vector3.right);
            sideValue.Add(3);

            directions.Add(Vector3.forward);
            sideValue.Add(4);

            directions.Add(Vector3.back);
            sideValue.Add(5);

        }

        if (directions.Count != sideValue.Count)
        {
            Debug.LogError("Error initializing dice: directions != side values");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current Side Up: ");
        GetDiceCount();
    }

    public int GetFaceUp (Vector3 referenceVector, float epsilonDeg = 5f)
    {
        if(directions.Count == 6 && sideValue.Count == 6)
        {
            // Transform reference up to object space
            Vector3 referenceObjectSpace = transform.InverseTransformDirection(referenceVector);

            // Find smallest difference to object space direction
            float min = float.MaxValue;
            int mostSimilarDirectionIndex = -1;
            for (int i = 0; i < directions.Count; ++i)
            {
                float a = Vector3.Angle(referenceObjectSpace, directions[i]);
                if (a <= epsilonDeg && a < min)
                {
                    min = a;
                    mostSimilarDirectionIndex = i;
                }
            }

            // -1 as error code for not within bounds
            return (mostSimilarDirectionIndex >= 0) ? sideValue[mostSimilarDirectionIndex] : -1;
        } else
        {
            return -1;
        }
    }
    void GetDiceCount()
    {
        int diceCount = -1;
        if (Vector3.Dot(transform.forward, Vector3.up) > 1)
            diceCount = 5;
        if (Vector3.Dot(-transform.forward, Vector3.up) > 1)
            diceCount = 2;
        if (Vector3.Dot(transform.up, Vector3.up) > 1)
            diceCount = 3;
        if (Vector3.Dot(-transform.up, Vector3.up) > 1)
            diceCount = 4;
        if (Vector3.Dot(transform.right, Vector3.up) > 1)
            diceCount = 6;
        if (Vector3.Dot(-transform.right, Vector3.up) > 1)
            diceCount = 1;
        Debug.Log("diceCount :" + diceCount);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
