using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Dandelion : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}


    private void OnCollisionEnter(Collision col)
    {
        if  (col.collider.name == "MLHands.Left")
        {
            Debug.Log("Collided left");
            transform.RotateAround(transform.position, transform.up, Time.deltaTime * -90f);
        }
        else if (col.collider.name == "MLHands.Right")
        {
            transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
            Debug.Log("Collided right");
        }else
        Debug.Log("Collision detected "+ col.collider.name);
    }



    // Update is called once per frame
    void Update () {
    }
}
