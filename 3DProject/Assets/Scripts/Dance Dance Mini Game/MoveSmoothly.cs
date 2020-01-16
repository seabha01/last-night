/*
 * MoveSmoothly.cs
 * 3D Project
 * Hannah Seabert, Caroline Henning, Thomas Mallick, Luba Grynyshin, David Ross
 * 25-Apr-2019
 * This script handles the movement of the arrows in the rhythm mini-game challenge.
 * The script specifically allows smooth movement down the screen to fix jerky movement
 */


using UnityEngine;
using System.Collections;

public class MoveSmoothly : MonoBehaviour
{
    public Vector3 destination;
    private Rigidbody rigidbody;
    public float speed = 2f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;
        movement.y = speed;

        rigidbody.velocity = movement;
        transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);

        if (transform.position.y <= 0)
        {
            Object.Destroy(this.gameObject);
        }
    }
}