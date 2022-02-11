/* 
 * Jonathan Hyun
 * FBLA 2022
 * Computer Game and Simulation Programming
 * Johns Creek High School
 * 1/28/2022
 */


// Imports Libraries
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainmenu : MonoBehaviour
{
    // Class Variables
    // The music in the main menu
    public AudioSource music;
    // The bike model
    public GameObject bike;
    // The Camera
    public Camera maincam;
    // The point the camera should move to
    private Vector3 newpos = new Vector3(0, 10, 0);
    // The main menu parent
    public GameObject mainmen;
    // If the bike is spinning
    private bool rotating = true;
    // The point the bike should move to
    public Vector3 animate = new Vector3(5f, -23f, 572f);
    // The level selected
    private int level = 0;
    // The frame the animation is on
    public int animationframe = 0;
    // The collection of loading images in the gif
    public Texture[] images;
    // The current frame it is on
    public int frames;
    // The current image is showing
    public int index;
    // The loading image script
    public loadingimagescr imagescr;
    // If it is loading
    public bool loading;
    // The animation frames
    public GameObject loadingani;
    public GameObject loadingback;
    // Sound played when clicking
    public AudioSource click;
    // Sound played in loading screen
    public AudioSource cassete;
    // Scoreboard ui parent
    public text scoreboard;


    // Called at the start
    private void Start()
    {

        // Sets the frame rate of the main menu
        if (!PlayerPrefs.HasKey("framerate"))
        {
            PlayerPrefs.SetFloat("framerate", 60f);
        }
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1f);
        }
        PlayerPrefs.Save();

        Application.targetFrameRate = (int)PlayerPrefs.GetFloat("framerate");

    }
    // Called when the player clicks play
    public void playGame()
    {
        newpos = newpos + new Vector3(0, 550, 0);
        click.pitch = 1.5f;
        click.Play();
    }
    // Called when the player clicks high scores
    public void score()
    {

        // Gets the scores from the local disk
        string message = "HighScores\n\n\n";
        string[] scorelist = PlayerPrefs.GetString("gameScores").Split('d');

        string[] namelist = PlayerPrefs.GetString("gameNames").Split('`');

        // Prints them out ordering them by value
        try
        {
            for (int i = scorelist.Length; i > 0; i--)
            {
                int greatest = 1;
                for (int j = 1; j < scorelist.Length; j++)
                {


                    if (Int32.Parse(scorelist[j]) > Int32.Parse(scorelist[greatest]))
                    {
                        greatest = j;
                    }
                }
                if (!namelist[greatest].Equals("0"))
                {
                    message += namelist[greatest];
                    message += "- " + scorelist[greatest] + "\n";
                }
                namelist[greatest] = "0";
                scorelist[greatest] = "0";


            }
        }
        catch
        {
            Debug.Log("No Scores");
        }

        // Sets the text to the score board
        scoreboard.setText(message);
    }

    // Called when the player clicks the back buttom
    public void back()
    {   
        newpos = newpos + new Vector3(0, -550, 0);
        click.pitch = 1.5f;
        click.Play();
    }
        

    // Called when the player clicks a specifiic level
    public void levelOne()
    {

        play();
        level = 1;
    }
    public void levelTwo()
    {

        play();
        level = 2;
    }
    public void levelThree()
    {

        play();
        level = 3;
    }

    // Called when the player clicks a level
    public void play()
    {
        // Plays the click sound
        click.pitch = 2f;
        click.Play();
        // Stops the bike from spinning and animates it moving towards the horizon
        rotating = false;
        bike.transform.rotation = new Quaternion(0, 0, 0, 0);
        animate = bike.transform.position + new Vector3(0, 0, 6000);
        newpos = maincam.transform.position + new Vector3(0, -300, 0);
        animationframe = 180;
    }

    // Update is called once per framec
    void FixedUpdate()
    {
        // Sets music and sound effects volumes
        music.volume = (float)(0.9 * PlayerPrefs.GetFloat("volume"));
        click.volume = PlayerPrefs.GetFloat("volume") * 0.9f;
        cassete.volume = 0.3f * PlayerPrefs.GetFloat("volume");

        // If it is not in loading screen
        if (loading == false)
        {
            if (rotating)
            {
                // Rotates the bike
                bike.transform.Rotate(bike.transform.InverseTransformDirection(0, -1.5f, 0));
            }

            if (newpos != maincam.transform.position)
            {
                // Animates the camera to the new position
                maincam.transform.position = new Vector3(maincam.transform.position.x, Mathf.Lerp(maincam.transform.position.y, newpos.y, 0.06f), maincam.transform.position.z);
            }

            if (animate != bike.transform.position)
            {
                // Animates the bike to the new position
                bike.transform.position = new Vector3(5, -23, Mathf.Lerp(572, 4572, (float)(180f-(float)animationframe)/180f));
            }

            // Waits until animation frame is 0
            if (animationframe > 0)
            {
                animationframe--;
            }
            if (animationframe == 1)
            {
                load();
            }
        }
        else
        {
            // Loops through all the loading screen frames
            if (frames > 0)
            {
                imagescr.setImage(images[index]);
                if (index == 17)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
                frames--;
            }
            else
            {
                // Loads whichever level was chosen
                SceneManager.LoadScene(level);
            }
        }


    }
    // Called when loading starts
    public void load()
    {
        // Pulls up the loading screen
        frames = UnityEngine.Random.Range(300, 400);
        index = 0;
        loadingani.SetActive(true);
        loadingback.SetActive(true);
        loading = true;
        // Stops the music and plays loading sound effect
        cassete.Play();
        music.Stop();

    }
}
