using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DNA : MonoBehaviour {

    //gene for colour & sound
    
    
    bool dead = false;
    public float timeToDie = 0;

    public AudioClip AudioClip1;
    public AudioClip Audio2;
    public AudioClip Audio3;
    public AudioSource audioSource5;
    public  int moveRForce = 0;
    private Rigidbody rb;

    public float cachedPitch = 0.1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void randomMovement()
    {
        //Random movement while ambient non collision
       // moveRForce = new int;
         moveRForce = (int)Random.Range(0, 5);
        switch (moveRForce)
        {
            case 0:
                rb.AddForce(0, 0.0005f, 0);
                break;
            case 1:
                rb.AddForce(-0.004f, -0.001f, 0.010f);

                break;
            case 2:

                rb.AddForce(0.004f, -0.0002f, -0.010f);
                break;
            case 3:
                rb.AddForce(0.005f, 0.0021f, -0.0010f);

                break;
            case 4:
                rb.AddForce(-0.005f, -0.0010f, 0);

                break;
        }
    }

    //Interaction randomizer

    private void OnCollisionEnter(Collision col)
    {
        dead = true;
        timeToDie = PopulationManager.elapsed;
        //Debug.Log("Dead At: " + timeToDie);

        if (col.collider.name == "ML.Hands.Left")
        {
            Debug.Log("Collided left");
            //Random Audio trigger

            audioSource5.clip = AudioClip1;

            if (audioSource5 != null)
            {
                Debug.Log("audio is playing left");
                audioSource5.Play();
            }
            else
            {
                Debug.Log("No audio source present");
            }
            //Random movement on collide
            transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        }
        else if (col.collider.name == "ML.Hands.Right")
        {

            Debug.Log("Collided right");
            //Random Audio trigger

            audioSource5.clip = AudioClip1;

            if (audioSource5 != null)
            {
                Debug.Log("audio is playing right");
                audioSource5.Play();
            }
            else
            {
                Debug.Log("No audio source present");
            }
            //Random movement on collide
            transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);

        }
        else {
            Debug.Log("Collision detected " + col.collider.name);
        }
    }
    // Use this for initialization
   
    
    // Update is called once per frame
    void Update () {
        randomMovement();
      //  Debug.Log("force :"  + moveRForce);
    }
}
