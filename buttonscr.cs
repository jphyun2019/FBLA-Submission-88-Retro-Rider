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
using UnityEngine.SceneManagement;

public class buttonscr : MonoBehaviour
{

    // Class Variables

    // The sound the button plays
    public AudioSource butsound;
    // The input text box
    public inputscr box;
    // The bike script
    public MotorcycleController bike;


    // Start is called before the first frame update

    // Called when the player exits the score screen
    public void end()
    {
        if(bike.mode == 0)
        {
            // Calls the "savescore" method
            box.savescore(bike.finalScore);
        }

        if(bike.mode == 2)
        {
            // Loads the Main Menu
            SceneManager.LoadScene(0);
        }
        butsound.Play();
        
    }
    //Quits the Game
    public void quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

}
