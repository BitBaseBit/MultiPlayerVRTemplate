﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericVRPlayerComponents : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject oculusTouchLeft;
    public GameObject oculusTouchRight;
    GenericVRPlayerComponents instance;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
