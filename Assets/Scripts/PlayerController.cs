using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	private Animator _anim;
	//private SpriteRenderer _render;
	//private Rigidbody2D _rb;
	public LayerMask _blockingLayer;

	public float _maxSpeed = 2.0f;

//	private IDetective _detective;


	// Use this for initialization
	void Start () {
		_anim = GetComponent<Animator> ();
		//_detective = GetComponent<IDetective> ();

		//_render = GetComponent<SpriteRenderer> ();
		//_rb = GetComponent<Rigidbody2D> ();


	}
	
	// Update is called once per frame
	void Update () {
		float hMove = Input.GetAxis ("Horizontal");
		float vMove = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (_maxSpeed * hMove, _maxSpeed * vMove, 0);

		movement *= Time.deltaTime;
		transform.Translate (movement);

		_anim.SetFloat("hSpeed", hMove);
		_anim.SetFloat("vSpeed", vMove);

	}

	void FixedUpdate() {

		float hLastMove = Input.GetAxis ("Horizontal");
		float vLastMove = Input.GetAxis ("Vertical");


		if (hLastMove !=0  || vLastMove != 0) {
			_anim.SetBool ("IsWalking", true);
			_anim.SetFloat("hPreviousSpeed", hLastMove);
			_anim.SetFloat("vPreviousSpeed", vLastMove);

		} else {
			_anim.SetBool ("IsWalking", false);
		}



	}


}
