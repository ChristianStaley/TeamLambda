using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Text : MonoBehaviour
{
	public Transform cam;
	public float fl_life_time = 1;
	public float fl_speed = 2;


	// Use this for initialization
	void Start()
	{
		GameObject camPos = GameObject.Find("Main Camera");
		cam = camPos.transform;
		Destroy(gameObject, fl_life_time);

	}//-----

	// Update is called once per frame
	void Update()
	{
		transform.LookAt(transform.position + cam.forward);
		transform.Translate(0, fl_speed * Time.deltaTime, 0);
	}//-----

}//==========


