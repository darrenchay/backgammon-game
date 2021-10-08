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
            float forceX = Random.Range(100, 5000);
            float forceY = Random.Range(100, 5000);
            float forceZ = Random.Range(100, 5000);
             
            transform.position = new Vector3(0, 2, 0);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 2000);
            rb.AddTorque(forceX, forceY, forceZ);

        }
    }
}
