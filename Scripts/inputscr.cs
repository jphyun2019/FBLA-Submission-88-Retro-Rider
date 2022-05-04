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
using TMPro;
using UnityEngine.SceneManagement;

public class inputscr : MonoBehaviour
{
    // The input box
    public TMP_InputField box;

    // Called when the player exits the score screen
    public void savescore(int score)
    {

        // Pulls the score list from the local disk and adds the new score
        string scores = PlayerPrefs.GetString("gameScores") + "d" + score;
        PlayerPrefs.SetString("gameScores", scores);
        Debug.Log(scores);

        // Pulls the name list from the local disk and adds the new score
        string names = PlayerPrefs.GetString("gameNames") + "`" + box.text;
        PlayerPrefs.SetString("gameNames", names);
        Debug.Log(names);

        // Saves the scores and loads the main menu
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

}
