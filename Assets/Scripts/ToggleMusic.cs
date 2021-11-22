using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMusic : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (Input.GetKeyDown("m"))
        {
            //Debug.Log("Toggling Mute");
            audio.mute = !audio.mute;
        }
    }
}
