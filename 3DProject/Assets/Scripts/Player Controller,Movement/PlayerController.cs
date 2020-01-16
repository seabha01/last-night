/*
 * PlayerController.cs
 * 3D Project
 * Luba Grynyshin, Hannah Seabert, Caroline Henning, Thomas Mallick, David Ross
 * 25-Apr-2019
 * 
 * Script to handle player and camera movement
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController PlayerControl;
    public GameObject PlayerSkin;
    public Transform TopOfHead; 
    private Animator AnimationController;
    public Camera Cam;
    private DialogueUI DialogueInPause;
    public GameObject target;

    public AudioClip audioClip;
    public AudioSource clipSource;

    float AngleX;
    float AngleY;
    public float XSensitivity;
    public float YSensitivity;
    Quaternion RotationX;
    Quaternion RotationY;

    public float Speed;
    float CurSpeed;

    public float Drunkness;
    public bool enableMovement;

    public float MinAngleY = -65;
    public float MaxAngleY = 70;


    // Start is called before the first frame update
    void Start()
    {
        //Gets the player's character controller and animation controller
        PlayerControl = GetComponent<CharacterController>();
        AnimationController = PlayerSkin.GetComponent<Animator>();

        clipSource.clip = audioClip;

        //Enables movement as well as sets the player's speed and starting drunkness value
        enableMovement = true; 
        CurSpeed = Speed;
        Drunkness = 50;

        //Set the Camera's starting rotation
        Cam.transform.rotation = new Quaternion(0.1f, 0.9f, -0.2f, 0.3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(Cam.transform.rotation);
        //called from StepParser: puts player in position for dance battle. 
        if (!enableMovement)
        {
            CurSpeed = 0; //Sets speed to 0 so they can't move
            transform.position = target.transform.position; //Moves them to position
            Cam.transform.rotation = new Quaternion(0, -0.7f, 0, -0.7f); //Rotates the camera to face the dance mini-game
        }
        else
        {
            //This sets the speed to the original value.
            CurSpeed = Speed;

            //This gets the value of the x-axis for the mouse and sets the rotation based off it
            if (!Mathf.Approximately(Input.GetAxis("Mouse X"), 0))
            {
                AngleX += Input.GetAxis("Mouse X") * ((Time.deltaTime + Time.smoothDeltaTime) / 2) * XSensitivity;
                AngleX %= 360;
                RotationX = Quaternion.AngleAxis(AngleX, new Vector3(0, 1, 0));
            }
            //This gets the value of the y-axis for the mouse and sets the rotation based off it
            if (!Mathf.Approximately(Input.GetAxis("Mouse Y"), 0))
            {
                AngleY += Input.GetAxis("Mouse Y") * Time.deltaTime * YSensitivity;
                AngleY = Mathf.Clamp(AngleY, MinAngleY, MaxAngleY);
                RotationY = Quaternion.AngleAxis(AngleY, new Vector3(-1, 0, 0));
            }
            //This rotates the camera based of the mouse's X and Ys
            Cam.transform.rotation = RotationX * RotationY;


            //This resets the move vector
            Vector3 Move = new Vector3(0, 0, 0);

            //This applies the speed value to it's corresponitng direction based off the inputs
            if (Input.GetAxis("Vertical") > 0)
            {
                Move.z += CurSpeed;
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                Move.z -= CurSpeed;
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                Move.x += CurSpeed;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                Move.x -= CurSpeed;
            }
            //This applies the rotation to the move vector
            Move = Cam.transform.rotation * Move;
            //This sends the speed to the animation controller so it knows what animation to play
            AnimationController.SetFloat("Speed", Move.magnitude);

            //This plays footsteps if the character is walking
            if (Move.magnitude > 0)
            {
                if (!clipSource.isPlaying)
                {
                    clipSource.Play();
                }
            }
            else
            {
                clipSource.Stop();
            }

            //This applies the movement vector to the player
            PlayerControl.Move(Move);
            CheckForGround();
        }
    }

    //This does a raycast down to check for the ground and pushes them down if they are ever not touching the ground
    void CheckForGround()
    {
        float dis = 0.1f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, dis) == false)
        {
            PlayerControl.Move(Vector3.down);
        }
    }

}
