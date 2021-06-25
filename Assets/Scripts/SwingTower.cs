using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SwingTower : MonoBehaviour
{
    private HookManager hookManager;
    public Transform BottomBuild;

    private void Start()
    {
        hookManager = FindObjectOfType<HookManager>();
    }

    private void Update()
    {
        if (hookManager.buildCnt >= 8)
        {
            transform.localRotation = Quaternion.Euler(Quaternion.identity.x,Quaternion.identity.y,Mathf.Sin(Time.time * 1.5f) * 1.5f);
        }
    }
}
