﻿using UnityEngine;
using System.Collections;

public class EntryStatic : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        LayRay.rayStyle = RayStyle.Ortho; 
        LeftHandProperty.HandInit();
        RightHandProperty.HandInit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
