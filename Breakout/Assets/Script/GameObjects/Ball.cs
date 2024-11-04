using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This class controlls the ball behavior */
public class Ball : MonoBehaviour
{
    public float maxVelocity = 10f; // max speed of the ball 
    private bool trigger; // bool flag for hitting star object
    private Rigidbody2D rb;
    public static bool hitFlag; // bool flag for passing to GameManager when ball hits brick
    public static bool enemyFlag; // bool flag for passing to GameManager when ball hits enemy
    
    /* Audio */
    public AudioSource audioSource; 
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trigger = false; 
        
        // check if AudioSource is assigned in the Ispector
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        hitFlag = false; 
        enemyFlag = false;
    }

    
    void Update()
    {
        // if ball hits the star, increase the size
        if (trigger)
        {
            transform.localScale = new Vector3(2.5f,2.5f,0);
        }
        else if (!trigger) // if trigger is false, resize the ball to original
        {
            transform.localScale = new Vector3(1.2f,1.2f,0);
        }
        
        // make sure ball speed won't exceed max velocity 
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }
    
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        BallBounceSound(); // play ball bounce sound when collision occurs
        
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;
    
        // look for the "Brick" tag that was assigned to the Apple GameObject prefab
        // Destroy bricks when the balls hits them 
        if (collidedWith.CompareTag("Brick"))
        {
            Destroy(collidedWith); // destroy brick
            hitFlag = true;
        }

        if (collidedWith.CompareTag("Star"))
        {
            Destroy(collidedWith); // destroy star
            trigger = true; // turn on flag to make the ball bigger
        }

        if (collidedWith.CompareTag("Enemy"))
        {
            // pass flag to game manager and make it true 
            // handle score in game manager 
            enemyFlag = true;
        }
    }
    
    // this function gets called everytime the ball hits an object
    public void BallBounceSound()
    {
       AudioUtils.PlaySound(audioSource); // call Audio Utility class
    }
}
