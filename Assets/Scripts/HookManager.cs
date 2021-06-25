
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;


public class HookManager : MonoBehaviour
{
    
    [SerializeField] private Transform pointText;
    [SerializeField] private GameObject deadPanel;
    public Transform Parent;
    public GameObject[] instantiateBuild;
    private Rigidbody2D rigidBody;
    public bool canDrop;
    public int buildCnt;
    public int dropCnt;
    public GameObject Build;
    public bool canCameraMove;
    
    void Start()
    {
        addList();
        canDrop = true;
    }
    void Update()
    {
        HookMovement();
        DropBuild();
        LoseGame();
    }
    
    
    private void HookMovement()
    {
        float MoveX = Mathf.Sin(Time.time * 2.5f);
        float MoveY = Mathf.Cos(Time.time * 2.5f) + 3.3f; 
        float MoveZ = Mathf.Sin(Time.time * 2.5f) * 6;
        
        transform.rotation = Quaternion.Euler(Quaternion.identity.x,Quaternion.identity.y,MoveZ);
        transform.localPosition = new Vector3(MoveX,MoveY,10);
    }
  
    private void DropBuild()
    {
        if (Input.GetMouseButtonDown(0) && canDrop)
        {
            canDrop = false;
            Build.GetComponent<Rigidbody2D>().gravityScale = 3f;
            Build.transform.SetParent(null);
            Build.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void point()
    {
        pointText.GetComponent<Text>().text = "Point: " + buildCnt * 10f;
    }

    private void LoseGame()
    {
        if (dropCnt == 3)
        {
            canDrop = false;
            deadPanel.SetActive(true);
        }
    }

    public void tryAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void addList()
    {
        Build = Instantiate(instantiateBuild[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y - 0.6f ,transform.position.z),transform.rotation);
        Build.transform.parent = Parent.transform;
    }
    
    public void InstantiateAgain()
    {
        addList();
        canDrop = true;
    }

} 
