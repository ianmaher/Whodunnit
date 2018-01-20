using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {


	private Animator _anim;
	//private SpriteRenderer _render;
	//private Rigidbody2D _rb;
	public LayerMask _blockingLayer;

	public float _maxSpeed = 1.0f;

	private float hMove = float.MinValue;
	private float vMove = float.MinValue;
	private float hLastMove = float.MinValue;
	private float vLastMove = float.MinValue;


	// Use this for initialization
	void Start () {
		_anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		hLastMove = hMove;
		vLastMove = vMove;

		hMove = CrossPlatformInputManager.GetAxis ("Horizontal");
		vMove = CrossPlatformInputManager.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (_maxSpeed * hMove, _maxSpeed * vMove, 0);

		movement *= Time.deltaTime;
		transform.Translate (movement);

		_anim.SetFloat("hSpeed", hMove);
		_anim.SetFloat("vSpeed", vMove);

	}

	void FixedUpdate() {
		
		if (hLastMove !=0  || vLastMove != 0) {
			_anim.SetBool ("IsWalking", true);
			_anim.SetFloat("hPreviousSpeed", hLastMove);
			_anim.SetFloat("vPreviousSpeed", vLastMove);

		} else {
			_anim.SetBool ("IsWalking", false);
		}



	}


}
