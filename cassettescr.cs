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

public class cassettescr : MonoBehaviour
{

    // Class Variables
    // The cassete tape sound
    public AudioSource tapesound;
    // Both bike scripts
    public MotorcycleController bikescr;
    public MotorcycleController bikescr2;
    // The explosion particle effect
    public ParticleSystem boost;
    // The renderer of the object (what you can see)
    public MeshRenderer mr;
    // The respawn timer after it is collected
    public int timer;
    // The game object of the tape
    public GameObject cas;

    // When it's mesh collides with another collider
    private void OnTriggerEnter(Collider collion)
    {
        tapesound.volume = PlayerPrefs.GetFloat("volume");

        // If its respawn timer is up
        if (timer == 0)
        {
            // If it collided with the first bike
            if (collion.tag == "p1")
            {                
                // Increases its combo and plays the bonus sound
                // Pitch of sound depends on if the player collected other tapes recently
                // Creates increasing pitch effect per tape
                bikescr.tapecombo = 150;
                tapesound.panStereo = -1;
                tapesound.pitch = 0.8f + (float)bikescr.tape * 0.1f;
                tapesound.Play();
                bikescr.tape++;
                bikescr.boost += 10;

                // Plays the explosion effect
                boost.transform.position = cas.transform.position;
                boost.Play();
                timer = 300;

            }
            // If it collided with the second bike
            else if (collion.tag == "p2")
            {
                // Increases its combo and plays the bonus sound
                // Pitch of sound depends on if the player collected other tapes recently
                // Creates increasing pitch effect per tape
                bikescr2.tapecombo = 150;
                tapesound.panStereo = 1;
                tapesound.pitch = 0.8f + (float)bikescr2.tape * 0.1f;
                tapesound.Play();
                bikescr2.tape++;
                bikescr2.boost += 10;

                // Plays the explosion effect
                boost.transform.position = cas.transform.position;
                boost.Play();
                timer = 300;
            }
        }

    }

    private void Start()
    {
        timer = 0;
    }


    // Update is called once per frame
    void Update()
    {
        // If the tape is respawning
        if (timer > 0)
        {
            timer--;
            mr.enabled = false;
        }
        else
        {
            mr.enabled = true;
            cas.transform.localEulerAngles += new Vector3(0, 4, 0);
        }

    }



}
