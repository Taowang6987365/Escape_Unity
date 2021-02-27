using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //private float gravity = -100;
    private void Update()
    {
        //Physics.gravity = new Vector3(0, gravity, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Letter")
        {
            Debug.Log("letter");
            //pick up letter
        }
    }

}
