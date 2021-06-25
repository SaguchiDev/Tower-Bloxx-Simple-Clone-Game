using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  
    public GameObject followObject;
    public float speed;
    
    public void Movement()
    { 
        followObject.transform.Translate(Vector2.up * speed);
    }
  
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, followObject.transform.position, 0.010f);
    }
}
