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

public class slider : MonoBehaviour
{
    // Slider object
    public Slider Slider;

    // Sets the value of the slider
    public void setValue(float boost)
    {
        // Lerps the slider value to the float value, creating smoothness
        Slider.value = Mathf.Lerp(Slider.value, boost, 0.1f);
    }


}
