﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Croshire : MonoBehaviour
{
    [Header("Прицел")]
    [SerializeField] public Slider croshire;
    private Vector3 croshirePos;
    
    

    void Update()
    {
        // Debug.Log(Input.mousePosition);
        croshire.transform.position = Input.mousePosition;
    }
}
