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
using TMPro;

public class text : MonoBehaviour
{
    // The text object
    public TextMeshProUGUI tex;
    
    // Sets the text of the object to a string
    public void setText(string x)
    {
        tex.text = x;
    }

    // Sets the softness of the text to a float
    public void setSoftness(float x)
    {
        tex.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineSoftness, x);
    }

    // Sets the outline of the text to a float
    public void setOutline(float x)
    {
        tex.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, x);
    }

    // Sets the size of the text to an interger
    public void setSize(int x)
    {
        tex.fontSize = x;
    }
}
