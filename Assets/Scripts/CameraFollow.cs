using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour {
	
	public GameObject _target;
	public float _speed = 2.0f;

	// Update is called once per frame
	public void Update () {

		
		Vector3 targetPos = _target.transform.position;
		float interp = _speed * Time.deltaTime;

		Vector3 newPos = this.gameObject.transform.position;

		newPos.x = Mathf.Lerp (newPos.x, targetPos.x, interp);
		newPos.y = Mathf.Lerp (newPos.y, targetPos.y, interp);

		this.gameObject.transform.position = newPos;

	}

}
