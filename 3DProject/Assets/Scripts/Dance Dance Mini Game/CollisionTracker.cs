/*
 * CollisionTracker.cs
 * 3D Project
 * Hannah Seabert, Caroline Henning, Thomas Mallick, Luba Grynyshin, David Ross
 * 24-Apr-2019
 * 
 * Script to handle collisions at each of the four bottom arrows in dancedance game.
 * Checks if user input is valid and then keeps track of a personal score for that arrow.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    SphereCollider sphereCollider;
    public GameObject targetArrow;
    public GameObject coloredArrow;

    public direction dir;
    public enum direction { left, down, up, right };

    public int personalScore;


    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        personalScore = 0;
    }

    // Generate colored arrow if collision with falling arrow is detected
    private void OnTriggerStay(Collider collision)
    {

        if (dir == direction.down && (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical")<= 0))
        {
            CheckLocation();
        }
        if (dir == direction.up && (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") >= 0))
        {
            CheckLocation();
        }
        if (dir == direction.left && (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") <= 0))
        {
            CheckLocation();
        }
        if (dir == direction.right && (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") >= 0))
        {
            CheckLocation();
        }
    }

    // check how far falling arrow is from target arrow at time of input
    void CheckLocation()
    {

        if (transform.position.y >= (targetArrow.transform.position.y - 1)
         && transform.position.y <= (targetArrow.transform.position.y + 1))
        {
            personalScore++;
            Object.Destroy(Instantiate(coloredArrow, transform.position, 
            Quaternion.AngleAxis(90, Vector3.up)), 0.3f);
        }
    }
}