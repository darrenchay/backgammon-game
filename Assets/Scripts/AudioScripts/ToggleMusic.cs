using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TOGGLEMUSIC
 * mutes music when pressing M
 */
public class ToggleMusic : MonoBehaviour
{

    public GameObject rules;

    // Update is called once per frame
    void Update()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (Input.GetKeyDown("m"))
        {
            //Debug.Log("Toggling Mute");
            audio.mute = !audio.mute;
        }

        // Hijacking this for the in game rules. -james
        if (Input.GetKeyDown("r"))
        {
            rules.SetActive(!rules.activeSelf);
        }
    }
}
