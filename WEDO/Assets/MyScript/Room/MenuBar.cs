﻿using UnityEngine;
using System.Collections;

public class MenuBar : MonoBehaviour
{

    private bool statue = false;
    private bool isHover = false;
    public static bool isOut = false;
    private Vector3 outPos = new Vector3(-37.5f, 0, 25);
    private Vector3 inPos = new Vector3(-52f, 0, 25);
    private float outSpeed = 10f;
    private float inSpeed = 15f;
    private string barName = "menubar";
    private Color originBarColor;
    private Color barNewColor = Color.red;


    // Use this for initialization
    void Start()
    {
        originBarColor = GameObject.Find(barName).renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkStatue();
        checkPlace();
    }

    private void checkPlace()
    {
        Vector3 curPos = transform.position;
        if (statue && curPos.x <= outPos.x)
        {
            GameObject.Find(barName).renderer.material.color = barNewColor;
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * outSpeed);
        }
        else
        {
            GameObject.Find(barName).renderer.material.color = originBarColor;
        }
        if (!statue && curPos.x >= inPos.x)
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * inSpeed);
        }
        if (curPos.x > inPos.x)
        {
            isOut = true;
        }
        else
        {
            isOut = false;
        }
    }

    private void checkStatue()
    {
        if (isHover && !statue)
        {
            statue = true;
        }
        if (!isHover)
        {
            statue = false;
        }
    }

    private void checkHover()
    {
        isHover = false;

        if (RayHit.LeftHitName.Equals(name) && !LeftHandProperty.isClosed
            || RayHit.RightHitName.Equals(name) && !RightHandProperty.isClosed)
        {
            isHover = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (RayHit.LeftHitName.Equals(child.name) && !LeftHandProperty.isClosed
                    || RayHit.RightHitName.Equals(child.name) && !RightHandProperty.isClosed)
                {
                    isHover = true;
                }
                else
                {
                    foreach (Transform grandChild in child.transform)
                    {
                        if (RayHit.LeftHitName.Equals(grandChild.name))
                        {
                            if (statue)
                            {
                                isHover = true;
                            }
                        } 
                        else if (RayHit.RightHitName.Equals(grandChild.name))
                        {
                            if (statue)
                            {
                                isHover = true;
                            }
                        }
                    }
                }
            }
        }
    }

}
