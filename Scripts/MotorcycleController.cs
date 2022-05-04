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
using System;

[RequireComponent(typeof(Rigidbody))]

public class MotorcycleController : MonoBehaviour
{

    // Class Variables

    // The other bike script
    public MotorcycleController rival;
    // Sound effects and music
    public AudioSource countdown;
    public AudioSource comboSound;
    public AudioSource engine;
    public AudioSource screech;
    public AudioSource whoosh;
    public AudioSource point;
    public AudioSource music;
    // Which control scheme the bike uses
    public int controls;
    // The desired center of mass
    public Vector3 centerOfMass2;
    // The rigidbody and game object of the bike
    public Rigidbody r;
    public GameObject motorcycle;
    // The point the bike rotates around
    public GameObject rotatepoint;
    // The camera that follows the bike
    public Camera myCamera;
    // The camera's script
    public camFollow camscript;
    // The boost particle effect
    public ParticleSystem burst;
    // The list of ramps
    public GameObject [] ramp;
    // The parent of the ui objects
    public GameObject ui;
    // If the bike is breaking
    private bool isBreaking;
    // The finish line
    public GameObject finish;
    // The video player
    public GameObject screen;

    // Maximum speed of bike
    public int maxSpeed;
    // Engine power of bike
    public float motorForce;
    // Brake force of bike
    public float brakeForce;
    // The direction the bike is leaning
    public float direction;

    public float handleing = 2;
    public float boostStrength = 25000;


    // The wheel colliders
    public WheelCollider FrontCollider;
    public WheelCollider BackCollider;




    // The wheel meshes
    public Transform FrontWheel;
    public Transform BackWheel;

    public Transform clFrontWheel;
    public Transform clBackWheel;
    public Transform crFrontWheel;
    public Transform crBackWheel;


    // Drift trails of both wheels
    public TrailRenderer frontDrift;
    public TrailRenderer backDrift;

    // Direction the bike is steering and moving
    private int steering = 0;
    private int accelerating = 0;

    // If the bike is on the ground, just was on the ground, and just was in the air
    public Boolean grounded;
    private Boolean liftoff;
    private Boolean landing;
    // If the bike is performing a backflip of flatspin
    private Boolean vertFlip;
    private Boolean horFlip;
    // Direction the bike is rotating when backflipping
    float tilt;
    
    // How much the bike is drifting
    public float driftMag;
    // How much boost charge the bike has
    public float boost;
    // Boost created by drifting
    public float driftBoost;
    // Boost created by airttime
    public float airTimeBoost;
    // Boost created by backflips
    public float backflipBoost;
    // Boost created by flatspins
    public float flatSpinBoost;

    // If the bike is turning
    public Boolean turning;
    // How long the bike has been on one wheel
    public int wheelied;
    // What frame of the boost animation it is on
    private int boostindex;
    // The boost bar
    public slider slider;
    // Boost created passively
    public float passiveBoost;
    // If the player can play, ie not in cutscene
    public Boolean playing;
    // Current frame of initial cutscene
    public int startFrames;
    // Singleplayer, Multiplayer, or tutorial
    public int mode;
    // Lap of player
    public int lap;
    // Initial rotation state before a ramp jump
    public Vector3 initrick;
    // Whether the bike has done a full 360 for a trick
    public Boolean trickcheck;
    // Max amount of boosts per trick
    public int stopBreakingMyGame;

    // Current time of race
    public int hundersec;
    public int sec;
    public int minute;
    // String parsed from current score
    public text scoretex;
    // String parsed from current time
    public text timetex;
    // Total score
    public int score;
    // Main text box
    public text counter;
    // Air time for current trick combo
    public int airtemp;
    // Drift time for current trick combo
    public int driftTemp;
    // Flat spins for current trick combo
    public int flatTemp;
    // Backflips for current trick combo
    public int backtemp;
    // Score for currebt trick combo
    public int scoreovercount;
    // Combo text box
    public text scoreover;
    // Duration combo text box is active
    public int tempscoretime;

    // Pitch of engine sound
    public float enginesound;
    // Volume of engine sound
    public float enginevol;
    // Volume of drifting sound
    public float screechvol;

    // Timer between tape to form combos
    public int tapecombo;
    // Current tape in tape combo
    public int tape;
    // Duration tutorial instructions are active
    public int tutTextCounter;
    // Tutorial text box
    public text tutText;
    // Timer after crossing finish line to de-accelerate
    public int finishTime = 300;
    // If the finish time is done
    public Boolean end = false;

    // Score when crossed finish line
    public int finalScore;
    // Time when crossed finish line
    public int finalTime;
    // End score text box
    public text endtext;
    // Time counter in end screen
    public int goalTime;
    // Exit button from end screen
    public GameObject endBut;
    // Input text box in end screen
    public GameObject inputName;
    // 1st tutorial voice over
    public AudioSource tutva;



    public GameObject racerbody;
    public GameObject classicbody;
    public GameObject cruiserbody;


    // Ran when scene loads
    void Start()
    {
        if (mode == 0)
        {
            switch (PlayerPrefs.GetString("bike"))
            {
                case "racer":

                    racerbody.SetActive(true);
                    cruiserbody.SetActive(false);
                    classicbody.SetActive(false);

                    maxSpeed = 70;
                    motorForce = 4500f;
                    handleing = 1.8f;
                    boostStrength = 20000f;



                    break;

                case "classic":
                    racerbody.SetActive(false);
                    cruiserbody.SetActive(false);
                    classicbody.SetActive(true);

                    maxSpeed = 65;
                    motorForce = 4000f;
                    handleing = 2.3f;
                    boostStrength = 25000f;

                    break;

                case "cruiser":
                    racerbody.SetActive(false);
                    cruiserbody.SetActive(true);
                    classicbody.SetActive(false);

                    maxSpeed = 60;
                    motorForce = 3700f;
                    handleing = 2.8f;
                    boostStrength = 30000f;

                    break;

                default:
                    racerbody.SetActive(true);
                    cruiserbody.SetActive(false);
                    classicbody.SetActive(false);

                    break;

            }
        }
        


        // Sets music volume
        if (mode == 2)
        {
            music.volume = 0.55f * PlayerPrefs.GetFloat("volume");
        }
        else
        {
            music.volume = 0.75f * PlayerPrefs.GetFloat("volume");
        }

        // Sets frame rate
        endBut.SetActive(false);
        Application.targetFrameRate = (int)PlayerPrefs.GetFloat("framerate");
        point.volume = PlayerPrefs.GetFloat("volume");

        Debug.Log((int)PlayerPrefs.GetFloat("framerate"));
        Debug.Log(Application.targetFrameRate);

        r = GetComponent<Rigidbody>();

        // Initiates initial cutscene
        if(mode == 0)
        {
            playing = false;
            startFrames = 900;
        }
        else if (mode == 1)
        {
            playing = false;
            startFrames = 180;

        }
        else
        {
            playing = true;
            camscript.playing = true;
        }
        lap = 1;
        counter.setSize(80);

        // Sets engine initial pitch
        enginesound = 0.5f;

    }

    // Called when driving off a ramp
    public void trickstart()
    {
        scoreovercount = 50;
        // If it is a flat spin
        if ((Input.GetKey(KeyCode.D) & (controls == 0)) ^ (Input.GetKey(KeyCode.L) & (controls == 1)))
        {
            // Adds initial spin in the direction and saves initial rotation state
            r.angularVelocity = r.transform.TransformDirection(0, +10, 0);
            horFlip = true;
            initrick = motorcycle.transform.eulerAngles;

        }
        else if (((Input.GetKey(KeyCode.A) & (controls == 0)) ^ (Input.GetKey(KeyCode.J) & (controls == 1))) & (((Input.GetKey(KeyCode.D) & (controls == 0)) ^ (Input.GetKey(KeyCode.L) & (controls == 1))) == false))
        {
            // Adds initial spin in the direction and saves initial rotation state
            r.angularVelocity = r.transform.TransformDirection(0, -10, 0);
            horFlip = true;
            initrick = motorcycle.transform.eulerAngles;

        }
        // If it is a backflip
        else if (((Input.GetKey(KeyCode.W) & (controls == 0)) ^ (Input.GetKey(KeyCode.I) & (controls == 1))))
        {
            // Saves initial tilt
            vertFlip = true;
            initrick = motorcycle.transform.eulerAngles;
            stopBreakingMyGame = 2;
        }

        Debug.Log("liftoff");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.Log((int)PlayerPrefs.GetFloat("framerate"));
        Debug.Log(Application.targetFrameRate);

        // Playes tutorial voice over
        if (mode == 2)
        {
            tutva.volume = PlayerPrefs.GetFloat("volume");
        }


        // Counts down then hides the instructions
        if(tutTextCounter > 0)
        {
            tutTextCounter--;
        }
        else
        {
            tutText.setText("");
        }

        // Times duration between tapes, ending combo
        if (tapecombo > 0)
        {
            tapecombo--;
        }
        else
        {
            tape = 0;
        }
        
        // Sets the engine volume and pitch
        // Lerps for sound smoothness
        engine.pitch = Mathf.Lerp(engine.pitch, enginesound, 0.2f);
        engine.volume = Mathf.Lerp(engine.volume, enginevol, 0.2f);
        screech.volume = Mathf.Lerp(screech.volume, screechvol, 0.2f);

        // If game playable, ie not cutscene or end screen
        if (playing)
        {
            
            if(finishTime == 300)
            {
                // Counts by hundreths of seconds, update rate is 0.05 seconds
                hundersec += 2;
                // Converts hundreths of seconds to seconds
                if (hundersec == 100)
                {
                    sec++;
                    hundersec = 0;
                }
                // Converts seconds to minutes
                if (sec == 60)
                {
                    minute++;
                    sec = 0;
                }

                // Creates a time string based off time values
                timetex.setText(minute.ToString() + ":" + ((sec > 9) ? sec.ToString() : "0" + sec.ToString()) + ":" + ((hundersec > 9) ? hundersec.ToString() : "0" + hundersec.ToString()));
                // Shows lap count if multiplayer
                if ((mode == 1))
                {
                    scoretex.setText("Lap " + lap.ToString());
                }
                // Shows score if singleplayer
                else
                {
                    scoretex.setText(score.ToString());
                }
            }

            // If the player moves to lap 3 in single or multiplayer
            if ((lap > 2) & ((mode == 1)|(mode == 0)))
            {
                // Start the finish deacceleration
                finishTime--;
                motorForce = 0;
                tempscoretime = 150;
                // Turn of sounds and print finish
                counter.setText("FINISH");
                ui.SetActive(false);
                engine.Stop();
                screech.Stop();
                music.volume -= (float)(1f / 300f) * PlayerPrefs.GetFloat("volume");
                if(mode == 1)
                {
                    rival.engine.Stop();
                    rival.screech.Stop();
                    rival.music.volume -= (float)(1f / 300f) * PlayerPrefs.GetFloat("volume");
                }

                scoreover.setText("");
                

            }
            // If the player moves to lap to in the tutorial
            if ((lap > 1) & (mode == 2))
            {
                // Start the finish deacceleration
                finishTime--;
                motorForce = 0;
                tempscoretime = 150;
                // Turn of sounds and print finish
                tutText.setText("FINISH");
                ui.SetActive(false);
                engine.Stop();
                screech.Stop();
                music.volume -= (float)(1f / 300f) * PlayerPrefs.GetFloat("volume");
                scoreover.setText("");
            }
            // When the deacceleration phase ends
            if (finishTime == 0)
            {
                // Converts time to seconds and disables the bike
                finalTime = (minute * 60) + sec;
                playing = false;
                camscript.playing = false;
                if (mode == 1)
                {
                    // Disables the rival as well
                    rival.playing = false;
                    rival.camscript.playing = false;
                    controls++;
                    // Prints if this bike wins
                    counter.setText("Player " + controls.ToString() + " Wins");
                }
                end = true; 
            }
            // Increases boost charge by set amount
            boost += passiveBoost;
            // If both wheel colliders are touching the ground
            if ((FrontCollider.isGrounded) | (BackCollider.isGrounded))
            {

                // If this is the first frame it became grounded
                if (grounded == false)
                {
                    landing = true;
                }
                else
                {
                    landing = false;
                }
                // Sets the bike as grounded
                grounded = true;

                // If only one wheel is touching the ground
                if (!((FrontCollider.isGrounded) & (BackCollider.isGrounded)))
                {
                    wheelied--;
                }
                else
                {
                    // Sets a timer which the bike can be on one wheel
                    wheelied = 80;
                }
            }


            else
            {
                // If the bike was grounded last frame
                if (grounded)
                {
                    liftoff = true;
                }
                else
                {
                    liftoff = false;
                }
                grounded = false;
                wheelied = 80;

            }


            if (grounded)
            {
                // Finds the perpendictular momentum from the bike's local z axis, ie sideways velocity
                driftMag = (Mathf.Sqrt(Mathf.Pow(r.transform.InverseTransformDirection(r.velocity).x, 2f) + Mathf.Pow(r.transform.InverseTransformDirection(r.velocity).y, 2f)));
            }
            else
            {
                driftMag = 0;
            }



            
            steering = 0;
            accelerating = 0;

            // If the S or K key is pressed
            isBreaking = ((Input.GetKey(KeyCode.S) & (controls == 0)) ^ (Input.GetKey(KeyCode.K) & (controls == 1)));

            // If the W or I key is pressed
            if (((Input.GetKey(KeyCode.W) & (controls == 0)) ^ (Input.GetKey(KeyCode.I) & (controls == 1))))
            {
                // If the current speed is below a set amount and the bike is on the ground
                if ((r.velocity.magnitude < maxSpeed) & (grounded))
                {
                    // Sets acceleration
                    accelerating= 1;
                }
            }

            // Moves the back drift parallel to the ground not the bike
            backDrift.transform.rotation = Quaternion.Euler(r.transform.eulerAngles.x + 90, 0, 0);
            // If the bike is drifting enough
            if (driftMag > 32)
            {
                // Plays the drift sound
                screechvol = 0.7f * PlayerPrefs.GetFloat("volume");
                // Emmits the back drift trail
                backDrift.emitting = true;
                // Increases boost charge
                boost += driftBoost;
                driftTemp += 2;
                // Resets combo timer
                scoreovercount = 50;
                // If not in deacceleration sequence
                if(finishTime == 300)
                {
                    score += 2;
                }
            }
            // If the bike is not drifting enough
            else
            {
                // Stops the drift sound and trail effect
                screechvol = 0;
                frontDrift.emitting = false;
                backDrift.emitting = false;
                // Counts down combo timer
                if (grounded)
                {
                    scoreovercount--;
                    
                }
                // When the combo timer runs out
                if (scoreovercount == 0)
                {
                    // Creates sets the temporary score for the trick combo
                    int tempscore = ((driftTemp > 30) ? driftTemp: 0) + ((airtemp > 30) ? airtemp : 0) + (flatTemp * 300) + (backtemp * 400);
                    if(mode != 1)
                    {
                        
                        if (tempscore > 0)
                        {
                            // If in single player, plays combo sound effect
                            if(mode == 0)
                            {
                                comboSound.volume = PlayerPrefs.GetFloat("volume");
                                comboSound.Play();
                            }
                            // Sets the text to the combo score
                            string congrats;

                            if(tempscore < 150)
                            {
                                congrats = "good";
                            }
                            else if (tempscore < 300)
                            {
                                congrats = "nice";
                            }
                            else if (tempscore < 600)
                            {
                                congrats = "great";
                            }
                            else if (tempscore < 1000)
                            {
                                congrats = "awesome";
                            }
                            else
                            {
                                congrats = "amazing";
                            }
      

                            counter.setText(congrats + "\n" + tempscore.ToString());
                        }
                    }
                    // Resets temporary combo trick counters
                    // Sets timer the text shows the score
                    tempscoretime = 50;
                    driftTemp = 0;
                    airtemp = 0;
                    flatTemp = 0;
                    backtemp = 0;
                }

            }
            
            // When that timer runs out
            if (tempscoretime < 1)
            {
                // Hides the text
                counter.setSize(40);
                counter.setText("");
            }
            
            else
            {
                // Counts down the text timer
                tempscoretime--;
                // If not in deacceleration sequence
                if (finishTime == 300)
                {
                    // Sets the softness of the text to fade in and out
                    counter.setSoftness(Mathf.Pow((float)((float)tempscoretime / 25f) - 1f, 6f));
                }
                else
                {
                    // Resets softness
                    counter.setSoftness(0);
                }
            }

            // If this is the frame the bike becomes grounded
            if (landing)
            {
                // Stops the vertical flip combo
                vertFlip = false;
            }
            
            // Sets the boost slider to the boost
            slider.setValue((float)boost);

            // If the player presses shift or semicolon
            if (((Input.GetKey(KeyCode.LeftShift) & (controls == 0)) ^ (Input.GetKey(KeyCode.Semicolon) & (controls == 1))) & (boost > 15))
            {
                // If the bike is on the ground
                if (grounded)
                {
                    // Adds engine force to back wheel
                    BackCollider.motorTorque = accelerating * motorForce + (float)boost * 0.01f;
                    Debug.Log(boost);
                    // Calls the combo boost method
                    comboBoost((float)boost * boostStrength);
                    // Resets boost charge
                    boost = 0;
                }

                
            }
            // Adds engine force to back wheel
            else
            {
                BackCollider.motorTorque = accelerating * motorForce;
            }

            // Spins the front wheel
            FrontCollider.motorTorque = accelerating * motorForce;

            // If this is the first frame in the air after being grounded
            if (liftoff)
            {
                // Checks for each ramp
                foreach (GameObject i in ramp)
                {
                    // If in tutorial
                    if (mode == 2)
                    {
                        // If within a certain distance to a ramp
                        if (Vector3.Distance(motorcycle.transform.position, i.transform.position) < 50)
                        {
                            // Calls the trickstart method
                            trickstart();
                        }
                    }
                    // If in single or multi player
                    else
                    {
                        // If within a certain distance to a ramp
                        if (Vector3.Distance(motorcycle.transform.position, i.transform.position) < 30)
                        {
                            // Calls the trickstart method
                            trickstart();
                        }
                    }
                }
            }

            // If braking, slow the wheels
            brakeForce = isBreaking ? 10000f : 0f;
            FrontCollider.brakeTorque = brakeForce;
            BackCollider.brakeTorque = brakeForce;

            // Calls the update wheel method, moves and rotates the wheel models to the actual wheel colliders' positions and rotations

            if(mode == 0)
            {
                UpdateWheel(BackCollider, crBackWheel);
                UpdateWheel(FrontCollider, crFrontWheel);
                UpdateWheel(BackCollider, clBackWheel);
                UpdateWheel(FrontCollider, clFrontWheel);
            }





            UpdateWheel(BackCollider, BackWheel);
            UpdateWheel(FrontCollider, FrontWheel);

            // Finds the roll axis
            direction = (transform.eulerAngles.z > 180) ? (transform.eulerAngles.z - 360) : transform.eulerAngles.z;

            turning = false;
            // If not in a back flip
            if (vertFlip == false)
            {
                if (((Input.GetKey(KeyCode.A) & (controls == 0)) ^ (Input.GetKey(KeyCode.J) & (controls == 1))))
                {
                    steering--;
                    turning = true;
                }
                if (((Input.GetKey(KeyCode.D) & (controls == 0)) ^ (Input.GetKey(KeyCode.L) & (controls == 1))))
                {
                    steering++;
                    turning = true;
                }
            }


            float balance = 0;

            // Rotate the bike's "yaw" to turn
            motorcycle.transform.Rotate(0, steering * handleing, 0, Space.World);
       
            if (grounded)
            {
                // Sets the drag coefficient to the bike rigidbody
                r.drag = 0.1f;
                trickcheck = false;
                horFlip = false;

                // If the bike is rolling too much
                if ((direction > 40) | (direction < -40))
                {
                    if (direction > 40)
                    {
                        // Sets a counter lean value that will prevent futher leaning
                        balance = -(direction - 40);
                    }
                    if (direction < -30)
                    {
                        // Sets a counter lean value that will prevent futher leaning
                        balance = -(direction + 40);
                    }

                }
                else
                {
                    // Sets a counter lean value to re balance the bike
                    balance = -direction / 15;
                }
                // Setes the engines pitch and volume by the bike's speed
                enginesound = 0.4f + ((float)r.velocity.magnitude) * 0.0077778f;
                enginevol = (0.2f + ((float)r.velocity.magnitude) * 0.0077778f) * PlayerPrefs.GetFloat("volume");

                // If not on one wheel for a set amount of time
                if (wheelied > 0)
                {
                    // Reduce all angular momentum to maintaign stability
                    r.angularVelocity = Vector3.Scale(r.angularVelocity, new Vector3(0.5f, 0.5f, 0.5f));
                }

                // Tilts the bike to re-level its roll
                motorcycle.transform.RotateAround(rotatepoint.transform.position, transform.forward, balance);

                // If the bike's roll is over 40 degrees
                if ((direction < 40) & (steering == 1))
                {

                    double counterforce;
                    counterforce = (direction + 40) / 10;

                    // Stops the bike from tilting further
                    motorcycle.transform.RotateAround(rotatepoint.transform.position, transform.forward, -(float)counterforce);
                }
                if ((direction > -40) & (steering == -1))
                {
                    double counterforce;
                    counterforce = (-direction + 40) / 10;

                    // Stops the bike from tilting further
                    motorcycle.transform.RotateAround(rotatepoint.transform.position, transform.forward, (float)counterforce);
                }

            }
            
            // If in the air
            if (grounded == false)
            {
                // Sets the bike's drag coeffecient to 0
                r.drag = 0;
                // Plays different engine sound
                enginesound = -1.5f;
                enginevol = 0.2f * PlayerPrefs.GetFloat("volume");
                // Resets score combo timer
                scoreovercount = 50;
                // Increases boost charge by set amount
                boost += airTimeBoost;
                // If not in deacceleration sequence
                if(finishTime == 300)
                {
                    // Increases score
                    score += 1;
                }
                // Increases air time counter for this combo
                airtemp += 1;

                // If doing backflips
                if (vertFlip)
                {
                    // If W or I is being pressed
                    if (((Input.GetKey(KeyCode.W) & (controls == 0)) ^ (Input.GetKey(KeyCode.I) & (controls == 1))))
                    {
                        // Resets angular velocity
                        r.angularVelocity = new Vector3(0, 0, 0);
                        // Adds pitch rotation upwards
                        motorcycle.transform.RotateAround(rotatepoint.transform.position, transform.right, -5f);
                    }
                    // If not holding W or I
                    else
                    {
                        // Current pitch of the bike
                        tilt = (transform.eulerAngles.x > 180) ? (transform.eulerAngles.x - 360) : transform.eulerAngles.x;

                        // If the pitch is not level
                        if ((tilt > 0))
                        {
                            // Adds angular speed to reallign the bike with the horizon
                            r.angularVelocity = r.transform.TransformDirection(-tilt / 30, 0, 0);
                        }
                        else
                        {
                            // Adds angular speed to reallign the bike with the horizon
                            r.angularVelocity = r.transform.TransformDirection(-tilt / 30, 0, 0);
                        }
                    }
                    // If the bike is upside down
                    if((Mathf.Abs(motorcycle.transform.localRotation.eulerAngles.x - 180) > 150) & (Mathf.Abs(motorcycle.transform.localRotation.eulerAngles.z - 180) < 30))
                    {
                        trickcheck = true;
                    }
                    // If the bike becomes "close enough" to being level and it already was upside down
                    if(trickcheck & (Mathf.Abs(motorcycle.transform.localRotation.eulerAngles.x - 180) > 150) & (Mathf.Abs(motorcycle.transform.localRotation.eulerAngles.z - 180) > 150))
                    {
                        // If the bike had not already done 3 flips
                        if(stopBreakingMyGame > 0)
                        {
                            // Increases stored boost by set amount
                            boost += backflipBoost;
                            stopBreakingMyGame--;
                        }
                        // Sets the point sound effect's pitch by the amount fo flips the bike has done in current combo
                        point.pitch = 1f + 0.1f * (float)backtemp;
                        // Plays the sound effect
                        point.Play();
                        // Resets upside down counter
                        trickcheck = false;
                        // Increases score and backflip count for current combo
                        score += 400;
                        backtemp += 1;
                    }
                }
                // If doing a flat spin
                else if (horFlip)
                {
                    // If the bike has rotated 180 degrees from its initial position when initiating flat spins
                    if(Mathf.Abs(Mathf.Abs(motorcycle.transform.eulerAngles.y - initrick.y) -180) < 20)
                    {
                        trickcheck = true;
                    }

                    // If the bike has rotated 180 degrees and is "close enough" to original rotation
                    if ((trickcheck == true) & (Mathf.Abs(motorcycle.transform.eulerAngles.y - initrick.y) < 90))
                    {
                        // Sets the point sound effect's pitch by the amount fo flips the bike has done in current combo
                        point.pitch = 1f + 0.1f * (float)flatTemp;
                        // Plays the sound effect
                        point.Play();
                        // Increases stored boost by a set amount
                        boost += flatSpinBoost;
                        // resets 180 check
                        trickcheck = false;
                        // Increases score and flatspin count for current combo
                        score += 300;
                        flatTemp += 1;

                    }

                }
                // If neither flatspinning nor backflipping
                if ((vertFlip == false) & (horFlip == false))
                {
                    // Current pitch of the bike
                    tilt = (transform.eulerAngles.x > 180) ? (transform.eulerAngles.x - 360) : transform.eulerAngles.x;

                    // If the pitch is not level
                    if ((tilt > 0))
                    {
                        // Adds angular speed to reallign the bike with the horizon
                        r.angularVelocity = r.transform.TransformDirection(-tilt / 30, 0, 0);
                    }
                    else
                    {
                        // Adds angular speed to reallign the bike with the horizon
                        r.angularVelocity = r.transform.TransformDirection(-tilt / 30, 0, 0);
                    }
                }
            }

            // Sets the bike's center of mass
            r.centerOfMass = centerOfMass2;

            // Sets the camera's field of view based off speed of bike
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, (int)(70 + ((r.velocity.magnitude < 80)? (r.velocity.magnitude*1.1):88)), 0.02f);

            // If in boost animation
            if (boostindex > 0)
            {
                // Zooms the camera out
                myCamera.fieldOfView += Mathf.Lerp(0, 20, 0.02f);
                boostindex--;
            }

            // Keeps stored boost at a max of 100
            if (boost > 100)
            {
                boost = 100;
            }

            if(finishTime == 300)
            {
                // Creates string printing current combo tricks
                String message = ((driftTemp > 30) ? "Drift x" + driftTemp : "") + "\n" + ((airtemp > 30) ? "Airtime x" + airtemp : "") + "\n" + ((flatTemp > 0) ? "Flatspin x" + flatTemp : "") + "\n" + ((backtemp > 0) ? "Backflip x" + backtemp : "");
                scoreover.setText(message);
            }
        }
        

        // If not playing
        else
        {
            // Stops bike
            r.isKinematic = true;
            // If at end screen
            if (end)
            {
                // Shows the exit button
                endBut.SetActive(true);
                
                // If it is not multiplayer
                if(mode != 1)
                {
                    // Prints the final score counter
                    endtext.setOutline(0.15f);
                    // Score counts up from 0 to actual score
                    finalScore = (int)Mathf.Lerp(finalScore, score + (goalTime - finalTime) * 100, 0.05f);
                    // Prints the time and final score of run
                    endtext.setText("Time-  " + (minute.ToString() + ":" + ((sec > 9) ? sec.ToString() : "0" + sec.ToString()) + ":" + ((hundersec > 9) ? hundersec.ToString() : "0" + hundersec.ToString())) +  "\n\nScore-  " + finalScore);

                }
                // If single player
                if(mode == 0)
                {
                    // Shows the name input box
                    inputName.SetActive(true);
                }
            }


            // If not in end, ei initial cutscene
            else
            {
                // Resets the engine power
                BackCollider.motorTorque = 0;
                finish.SetActive(false);
                ui.SetActive(false);

                // If in single player
                if (mode == 0)
                {
                    // First cutscene shot
                    if (startFrames > 720)
                    {
                        // Moves camera through map
                        myCamera.transform.eulerAngles = new Vector3(0f, -90.98f, 0f);
                        myCamera.transform.position = new Vector3(Mathf.Lerp(-938.5f, -1200f, (float)(900f - (float)startFrames) / 180), 108.1f, -47.6f);
                    }
                    // Second cutscene shot
                    else if (startFrames > 540)
                    {
                        // Moves camera through map
                        myCamera.transform.eulerAngles = new Vector3(-4.621f, 145.317f, -0.2f);
                        myCamera.transform.position = new Vector3(Mathf.Lerp(-629f, -864.5f, (float)(720f - (float)startFrames) / 180), Mathf.Lerp(108, 83, (float)(720f - (float)startFrames) / 180), Mathf.Lerp(100f, 386.4f, (float)(720f - (float)startFrames) / 180));
                    }
                    // Third cutscene shot
                    else if (startFrames > 360)
                    {
                        // Moves camera through map
                        myCamera.transform.eulerAngles = new Vector3(0f, 352.63f, 0f);
                        myCamera.transform.position = new Vector3(-1568f, Mathf.Lerp(110, 85, (float)(540f - (float)startFrames) / 180), Mathf.Lerp(-389f, 443f, (float)(540f - (float)startFrames) / 180));
                    }
                    // Fourth cutscene shot
                    else if (startFrames > 180)
                    {
                        // Moves camera through map
                        myCamera.transform.eulerAngles = new Vector3(0, Mathf.Lerp(79.325f, -27.114f, (float)(360f - (float)startFrames) / 180), 0);
                        myCamera.transform.position = new Vector3(Mathf.Lerp(-288.1f, -273, (float)(360f - (float)startFrames) / 180), 90, Mathf.Lerp(19.8f, 15.5f, (float)(360f - (float)startFrames) / 180));
                    }
                    // If in countdown
                    else if (startFrames > 0)
                    {
                        // Sets countdown sound effect volume
                        countdown.volume = PlayerPrefs.GetFloat("volume");
                        // Prints 3
                        if (startFrames == 150)
                        {
                            counter.setText("3");
                            countdown.Play();
                        }
                        // Prints 2
                        if (startFrames == 100)
                        {
                            counter.setText("2");
                            countdown.Play();
                        }
                        // Prints 1
                        if (startFrames == 50)
                        {
                            counter.setText("1");
                            countdown.Play();
                        }
                        // Prints Go!
                        if (startFrames == 1)
                        {
                            counter.setText("Go!");
                            // Changes countdown pitch by an octave higher
                            countdown.pitch = 2f;
                            countdown.Play();
                        }
                        // Changes countdown text to fade in and out
                        counter.setSoftness((float)Mathf.Pow((float)(startFrames % 50f), 3f) * 0.000008f);
                        // Moves camera towards bike
                        myCamera.transform.eulerAngles = new Vector3(10.204f, 267.85f, 0f);
                        myCamera.transform.position = new Vector3(Mathf.Lerp(-235.6f, -267.4438f, (float)(180f - (float)startFrames) / 180), Mathf.Lerp(96.9f, 91.17408f, (float)(180f - (float)startFrames) / 180), Mathf.Lerp(23.6f, 22.36565f, (float)(180f - (float)startFrames) / 180));
                    }

                }
                
                // If in multiplayer
                else if (mode == 1)
                {
                    // Prints 3
                    if (startFrames == 150)
                    {
                        counter.setText("3");
                        countdown.Play();
                    }
                    // Prints 2
                    if (startFrames == 100)
                    {
                        counter.setText("2");
                        countdown.Play();
                    }
                    // Prints 1
                    if (startFrames == 50)
                    {
                        counter.setText("1");
                        countdown.Play();
                    }
                    // Prints Go!
                    if (startFrames == 1)
                    {
                        counter.setText("Go!");
                        // Changes countdown pitch by an octave higher
                        countdown.pitch = 2f;
                        countdown.Play();
                    }
                    // Changes countdown text to fade in and out
                    counter.setSoftness((float)Mathf.Pow((float)(startFrames % 50f), 3f) * 0.000008f);
                    // Sets camera's rotation
                    myCamera.transform.eulerAngles = new Vector3(12.7f, 269, 0);

                    // If player 1
                    if (controls == 0)
                    {
                        // Move camera to bike
                        myCamera.transform.position = new Vector3(Mathf.Lerp(-273f, -330f, (float)(180f - (float)startFrames) / 180), Mathf.Lerp(105, 92, (float)(180f - (float)startFrames) / 180), 13);
                    }
                    // If player 2
                    else
                    {
                        // Move camera to bike
                        myCamera.transform.position = new Vector3(Mathf.Lerp(-273f, -330f, (float)(180f - (float)startFrames) / 180), Mathf.Lerp(105, 92, (float)(180f - (float)startFrames) / 180), 40);
                    }
                }
                // When coundown finishes
                if (startFrames == 0)
                {
                    // Enables the bike
                    playing = true;
                    camscript.playing = true;
                    r.isKinematic = false;
                    ui.SetActive(true);
                    finish.SetActive(true);
                    lap = 1;
                    tempscoretime = 50;
                }
                // Counts down cutscene frames
                startFrames--;
            }
        }
    }


    // Called when using shift to boost
    private void comboBoost(float mag)
    {
        // Plays whoosh sound effect
        whoosh.volume = PlayerPrefs.GetFloat("volume");
        whoosh.Play();
        // If the bike is moving slower than 90
        if(r.velocity.magnitude < 90)
        {
            // Add force forwards based off stored boost
            r.AddRelativeForce(new Vector3(0, 0, mag));
            // Plays boost particle effect
            burst.Play();
            boostindex = 50;
        }
        else
        {
            // Add force forwards based off stored boost
            r.AddRelativeForce(new Vector3(0, 0, mag/2));
            // Plays boost particle effect
            burst.Play();
            boostindex = 50;
        }
    }
  

    // Moves the wheel 3d model to the actual wheel collider
    private void UpdateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

}
