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

public class mod : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // If the escape key is clicked ever
        if (Input.GetKey("escape"))
        {
            // Quits the application
            Debug.Log("quit");
            Application.Quit();
        }
    }
}
