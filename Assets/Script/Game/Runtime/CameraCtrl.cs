using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraCtrl : MonoBehaviour
{
    public Transform Player;

    public float LimitLeftX;
    public float LimitRightX;
    public float LimitDownY = 1;

    public float Z = -3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position,
           new Vector3(Mathf.Clamp(Player.position.x, LimitLeftX, LimitRightX),
            Mathf.Clamp(Player.position.y + 0.5f, LimitDownY, 100),
            Z),
           Time.deltaTime * 5);
	}
}
