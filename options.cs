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
using UnityEngine.UI;

public class options : MonoBehaviour
{
    // Class Variables

    // Frame rate interactive bar
    public Scrollbar framerate;
    // Volume interactive Bar
    public Scrollbar volume;
    // Prints the current frame rate
    public text framerateText;
    // Prints volume
    public text volumeText;


    // Called when scene loads
    private void Start()
    {
        // Gets the current framerate from local disk
        float frame = PlayerPrefs.GetFloat("framerate");

        // Prints the rate
        framerateText.setText("Frame Rate- " + frame);
        // Moves the bar to the set frame rate value
        if (frame == 30)
        {
            framerate.value = 0;
        }
        else if (frame == 60)
        {
            framerate.value = 0.5f;
        }
        else if (frame == 120)
        {
            framerate.value = 1f;
        }


        // Gets the current value from local disk and sets the bar to it
        volume.value = PlayerPrefs.GetFloat("volume");
    }


    // Changes the frame rate
    public void setFrameRate()
    {
        // Gets the new frame rate
        float frame = framerate.value;

        // Sets the frame rate in local disk to set position in the bar
        if(frame<= 0.25f)
        {
            PlayerPrefs.SetFloat("framerate", 30);
        }

        else if (frame >= 0.75)
        {
            PlayerPrefs.SetFloat("framerate", 120);
        }

        else
        {
            PlayerPrefs.SetFloat("framerate", 60);
        }

        PlayerPrefs.Save();
        // Updates the text bos to show the new frame rate
        framerateText.setText("Frame Rate- " + PlayerPrefs.GetFloat("framerate"));
    }

    // Changes the volume
    public void setVolume()
    {
        // Sets the volume in the local disk to set position in the bar
        PlayerPrefs.SetFloat("volume", volume.value);
        PlayerPrefs.Save();
    }


    // Resets the game scores and names highscores in the local disk
    public void resetScores()
    {
        PlayerPrefs.SetString("gameScores", "");
        PlayerPrefs.SetString("gameNames", "");
        PlayerPrefs.Save();
    }








}
