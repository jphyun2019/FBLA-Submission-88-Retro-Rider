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

public class loadingimagescr : MonoBehaviour
{
    // The image frame
    public RawImage image;

    // Sets the frame to a set image
    public void setImage(Texture frame)
    {
        image.texture = frame;
    }


}
