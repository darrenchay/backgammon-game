using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        diceVel = rb.velocity;

        if(Input.GetKeyDown (KeyCode.Space))
        {
            DiceNum.diceNumber = 0;

            // Setting the spin for the dice roll
            float forceX = Random.Range(100, 5000);
            float forceY = Random.Range(100, 5000);
            float forceZ = Random.Range(100, 5000);
             
            // Resetting dice position and rotation before throwing
            transform.position = new Vector3(0, 2, 0);
            transform.rotation = Quaternion.identity;

            // Setting the throw force
            rb.AddForce(transform.up * 1000);
            int direction = Random.Range(1, 3);
            if(direction == 1)
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
}
