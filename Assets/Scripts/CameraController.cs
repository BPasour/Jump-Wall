using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;
	private Vector3 doubleoffset;

	// Use this for initialization
	void Start () 
	{
		offset = transform.position - player.transform.position;	
		doubleoffset = 2f * transform.position - 2f * player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.position = player.transform.position + offset;
		if (Input.GetKey ("z")) 
		{
			transform.position = player.transform.position + doubleoffset;
		}
	}
}
