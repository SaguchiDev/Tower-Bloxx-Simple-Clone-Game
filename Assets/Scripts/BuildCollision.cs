using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditor.Build.Content;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BuildCollision : MonoBehaviour
{
    
   
    private HookManager hookManager;
    private CameraMovement cameraMovement;
    private HookManager HookManager;
    private GameObject underBuild;
    private SwingTower swingTower;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider;
    private float forceDirX;
    

    
    private void Awake()
    {
        
        swingTower = FindObjectOfType<SwingTower>();
        HookManager = FindObjectOfType<HookManager>();
        cameraMovement = FindObjectOfType<CameraMovement>();
        hookManager = FindObjectOfType<HookManager>();
    }
    
    private void OnCollisionEnter2D(Collision2D OtherBuild)
    {
        underBuild = OtherBuild.gameObject;
        HookManager.Build.transform.rotation = underBuild.transform.rotation;
        FallBuild();
        if (hookManager.buildCnt >= 2 && HookManager.canCameraMove)
        {
            cameraMovement.Movement();    
        }
        Debug.Log(ReturnPositionX());
        StartCoroutine(Build());
    }

    private void FallBuild()
    {
        rigidbody2D = HookManager.Build.GetComponent<Rigidbody2D>();
        boxCollider = HookManager.Build.GetComponent<BoxCollider2D>();

        if (ReturnPositionX() > 0.65f)
        {
            forceDirX = 150f;
            dropBuild();
        }
        else if (ReturnPositionX() > -0.65f)
        {
            stayBuild();
            hookManager.buildCnt++;
        }

        if (ReturnPositionX() < -0.65f)
        {
            forceDirX = -150f;
           dropBuild();
        }
        else if (ReturnPositionX() < 0.65f)
        {
           stayBuild();
        }
    }

    private void stayBuild()
    {
        HookManager.canCameraMove = true;
        HookManager.Build.transform.parent = swingTower.BottomBuild.transform;
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        hookManager.point();
    }

    private void dropBuild()
    {
        hookManager.dropCnt++;
        HookManager.canCameraMove = false;
        rigidbody2D.gravityScale = 8;
        rigidbody2D.AddForce(new Vector2(forceDirX,200) * Time.deltaTime,ForceMode2D.Impulse);
        boxCollider.enabled = false;
        StartCoroutine(DestroyBuilds());
    }
    
    public float ReturnPositionX()
    {
        Vector2 top = HookManager.Build.transform.position;
        float topX = top.x;
        Vector2 bottom = underBuild.transform.position;
        float bottomX = bottom.x;
        return topX - bottomX;
    }
    
    IEnumerator DestroyBuilds()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(HookManager.Build.gameObject);
    }
    
    IEnumerator Build()
    {
        yield return new WaitForSeconds(0.8f);
        hookManager.InstantiateAgain();
        Destroy(this);
    }
}
