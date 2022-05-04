/* 
 * Jonathan Hyun
 * FBLA 2022
 * Computer Game and Simulation Programming
 * Johns Creek High School
 * 1/28/2022
 */


// Imports Libraries
using UnityEngine;
using System.Collections;

public class camFollow : MonoBehaviour
{

	// Class Variables
	
	// The bike script
	public MotorcycleController motorcycle;
	// Where the camera points at
	public Transform target;
	public Transform target2;
	// Distance to bike
	public float distance = 10.0f;
	public float height = 5.0f;
	// Smoothening factor of movements
	public float heightDamping = 2.0f;
	public float rotationDamping = 1.0f;
	
	public bool playing = false;

	[AddComponentMenu("Camera-Control/Smooth Follow")]

	// Called after every frame
	void LateUpdate()
	{
		if (playing)
		{
			float wantedRotationAngle = target.eulerAngles.y;
			float wantedHeight = target.position.y + height;

			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;

			// Only follows rotation when on the ground
			if (motorcycle.grounded == false)
			{
				rotationDamping = 0;
			}
			else
			{
				rotationDamping = 3.5f;
			}

			// Lerps to the desired angle and position to create smoothness
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Moves to the correct position relative to the bike
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;
			transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

			// Looks at different points of the bike if airborne
			if (motorcycle.grounded == false)
			{
				transform.LookAt(target2);
			}
			else
			{
				transform.LookAt(target);
			}
		}
	}
}