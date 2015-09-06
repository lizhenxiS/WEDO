﻿using UnityEngine;
using System.Collections;
using Wedo_ClientSide;

public class Home_project : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.1f;
    public float originZ;
    public float hoverZ;
    public ClientProject projectObject = null;

    // Use this for initialization
    void Start()
    {
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
        foreach (Transform child in transform)
        {
            child.name = name + child.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                LeftHandProperty.clickUsed = true;
                WholeStatic.curProject = projectObject;
                Application.LoadLevel(Name.MAINPROJECTIONPAGENAME);
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                WholeStatic.curProject = projectObject;
                Application.LoadLevel(Name.MAINPROJECTIONPAGENAME);
            }
        }
    }

    private void checkHover()
    {
        isHover = false;
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (RayHit.LeftHitName.Equals(child.name) || RayHit.RightHitName.Equals(child.name))
                {
                    isHover = true;
                }
            }
        }
        if (isHover)
        {
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
