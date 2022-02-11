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

public class speedometer : MonoBehaviour
{
 
    // Class Variables

    // The pointer object
    public GameObject speedmeter;
    // The bike rigidbody
    public Rigidbody bike;
    // Angle of the the speedometer
    public float angle;

    // Update is called once per frame
    void Update()
    {

        // If the bike's speed is less than 90
        if( bike.velocity.magnitude <= 90)
        {
            angle = bike.velocity.magnitude * 9 / 4;
        }
        else
        {
            angle = 202f;
        }

        // Sets the pointer's angle to the angle value, using Lerp for smoothness
        speedmeter.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(speedmeter.transform.eulerAngles.z, -angle, 0.05f));
    }
}
