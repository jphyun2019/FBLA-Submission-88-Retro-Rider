/* 
 * Jonathan Hyun
 * FBLA 2022
 * Computer Game and Simulation Programming
 * Johns Creek High School
 * 1/28/2022
 */

// Imports Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voicescr : MonoBehaviour
{
    // Class Variables

    // The bike script
    public MotorcycleController bikescr;
    // Set voice over mp3
    public AudioSource voice;
    // String printed
    public string message;
    // Text box string printed to
    public text text;

    
    // When something collides with the box collider
    private void OnTriggerEnter(Collider collion)
    {

        // If the collision was from the player object
        if (collion.tag == "Player")
        {

            // Sets the volume of the audio source and plays
            voice.volume = PlayerPrefs.GetFloat("volume");
            voice.Play();

            // Sets the text of the text bos to the Sstring
            text.setText(message);
            
            // Sets the timer for the text box
            bikescr.tutTextCounter = 250;
        }

    }
}
