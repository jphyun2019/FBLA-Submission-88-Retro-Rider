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

public class finishscr : MonoBehaviour
{

    // Class Variables
    // Both bike scripts
    public MotorcycleController bikescr;
    public MotorcycleController bikescr2;
    // The finish line barrier
    public GameObject barrier;
    // The leading bike
    public int leader = 0;


    // When it's mesh collides with another collider
    private void OnTriggerEnter(Collider collion)
    {
        // If it collided with the first bike
        if (collion.tag == "Player")
        {
            // Increase the lap count of this bike
            bikescr.lap++;
            barrier.SetActive(false);
        }
        else if (collion.tag == "p1")
        {
            bikescr.lap++;
            barrier.SetActive(false);
            // If this is the first bike to cross
            if (leader == 0)
            {
                // Gives the opponent comeback advantage
                leader = 1;
                bikescr2.motorForce = 5000;
            }

        }
        // If it collided with the second bike
        else if (collion.tag == "p2")
        {
            // Increase the lap count of this bike
            bikescr2.lap++;
            barrier.SetActive(false);
            // If this is the first bike to cross
            if (leader == 0)
            {
                // Gives the opponent comeback advantage
                leader = 2;
                bikescr.motorForce = 5000;
            }
        }
    }
    //Re-enables the barrier
    private void OnTriggerExit(Collider other)
    {
        barrier.SetActive(true);
    }
}
