﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraCtrl : MonoBehaviour
{
    public Transform Player;

    public float LimitLeftX;
    public float LimitRightX;
    public float LimitDownY = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position,
           new Vector3(4,
            Mathf.Clamp(Player.position.y + 0.5f, LimitDownY, 100),
            Mathf.Clamp(Player.position.z, LimitLeftX, LimitRightX)),
           Time.deltaTime * 5);
	}
}