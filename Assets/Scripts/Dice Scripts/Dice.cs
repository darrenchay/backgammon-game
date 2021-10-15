using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 diceVel;
    public int diceNum;

    //Array holding the different faces
    private GameObject[] faceSides;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Creating the face sides
        faceSides = new GameObject[6];
        for (int i = 0; i < 6; i++)
        {
            faceSides[i] = createFaceSide(i);
        }
    }

    // Creates a face side game object with a sphere collider and dice face script
    private GameObject createFaceSide(int i)
    {
        GameObject faceSide = new GameObject();
        //Making face side a child of dice

        faceSide.transform.parent = gameObject.transform;
        // Setting scale
        faceSide.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        // Adding dice face script and setting dice num
        faceSide.AddComponent<DiceFace>();
        faceSide.GetComponent<DiceFace>().setDiceNum(diceNum);

        //Adding the sphere collider component, and setting isTrigger to true
        faceSide.AddComponent<SphereCollider>();
        faceSide.GetComponent<SphereCollider>().isTrigger = true;

        //Naming the side gameObject
        faceSide.name = "Side " + (i + 1);

        //Setting their position relative to the dice and the faceup value
        if (i == 0)
        {
            faceSide.transform.localPosition = new Vector3(0, 0, -2.0f);
            faceSide.GetComponent<DiceFace>().setFaceUp(6);
        }
        else if (i == 1)
        {
            faceSide.transform.localPosition = new Vector3(0, -2.0f, 0);
            faceSide.GetComponent<DiceFace>().setFaceUp(3);
        }
        else if (i == 2)
        {
            faceSide.transform.localPosition = new Vector3(0, 2.0f, 0);
            faceSide.GetComponent<DiceFace>().setFaceUp(2);
        }
        else if (i == 3)
        {
            faceSide.transform.localPosition = new Vector3(-2.0f, 0, 0);
            faceSide.GetComponent<DiceFace>().setFaceUp(5);
        }
        else if (i == 4)
        {
            faceSide.transform.localPosition = new Vector3(2.0f, 0, 0);
            faceSide.GetComponent<DiceFace>().setFaceUp(4);
        }
        else if (i == 5)
        {
            faceSide.transform.localPosition = new Vector3(0, 0, 2.0f);
            faceSide.GetComponent<DiceFace>().setFaceUp(1);
        }

        return faceSide;
    }

    // Update is called once per frame
    void Update()
    {
        diceVel = rb.velocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DiceNum.diceNumber1 = 0;
            DiceNum.diceNumber2 = 0;

            // Setting the spin for the dice roll
            float forceX = Random.Range(100, 5000);
            float forceY = Random.Range(100, 5000);
            float forceZ = Random.Range(100, 5000);

            // Resetting dice position and rotation before throwing
            if (diceNum == 1)
            {
                transform.position = new Vector3(-1, 3, -1);
                transform.rotation = Quaternion.identity;
            } else {
                transform.position = new Vector3(2, 2, -1);
                transform.rotation = Quaternion.identity;
            }
            // Setting the throw force
            rb.AddForce(transform.up * 1000);
            int direction = Random.Range(1, 3);
            if (direction == 1)
            {
                rb.AddForce(-transform.forward * 1000);
            }
            else if (direction == 2)
            {
                rb.AddForce(transform.right * 1000);
            }
            else if (direction == 3)
            {
                rb.AddForce(-transform.right * 1000);
            }

            // Applying spin
            rb.AddTorque(forceX, forceY, forceZ);

        }
    }

    public Vector3 getDiceVel()
    {
        return diceVel;
    }
}
