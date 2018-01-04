using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathWalker {
	private GameObject _src;
	private Animator _anim;
	private List<Vector3> _waypoints;
	private Vector3 _currentWaypoints;

	private float _speed;
	int _idx = -1;
	bool _isReady = false;


	public PathWalker(GameObject src, Animator anim, float speed = 2.0f) {
		_src = src;
		_anim = anim;
	}

	public void SetWayPoints(List<Vector3> waypoints, float speed = 2.0f ) {
		_waypoints = waypoints;
		_currentWaypoints = _waypoints [0];
		_speed = speed;
		_idx = 0;
		_isReady = true;

	}

	public void Turn(float dir, float distance) {
		Debug.Log (_src.name + " has turned");
		_currentWaypoints.y -= distance;
	}

	// Update is called once per frame
	public bool Move () {
		if (!_isReady)
			return false;
		if ( _idx >= _waypoints.Count) {
			_anim.SetFloat ("vPreviousSpeed", -1f);
			_anim.SetFloat ("hPreviousSpeed", 0);
			_anim.SetBool ("IsWalking",false);	
			return false;
		}
		Vector3 myPos = _src.transform.position;
		Vector3 targetPos = _waypoints[_idx];
		_src.transform.position = Vector3.MoveTowards (myPos, targetPos, _speed * Time.deltaTime);


		// inform the animation what type of movement is happending.
		float dec = PathWalker.AngleDegrees(_src.transform.position, targetPos);

	
		if (dec >= 0f && dec < 35f)
			SetAnimation (0, 1);
		else if( dec >= 35f && dec < 55f) // north
			SetAnimation( 1, 1 );
		else if ( dec >= 55f && dec < 125f) // north west 
			SetAnimation( 1, 0 );
		else if ( dec >= 125f && dec < 145f) // south west
			SetAnimation( 1, -1 );
		else if (dec >= 145f && dec < 215f)
			SetAnimation( 0, -1 );
		else if ( dec >= 215f && dec < 235f) 
			SetAnimation( -1, -1 );
		else if ( dec >= 235 && dec < 305f) 
			SetAnimation( -1, 0 );		
		else if ( dec >= 305f && dec < 325f) 
			SetAnimation( -1, 1 );		
		else if ( dec >= 325f ) 
			SetAnimation( 0, 1 );


		CheckForNewWaypoint ();

		return true;
	}

	private void CheckForNewWaypoint () {
		if (Vector3.Distance (_src.transform.position, _waypoints [_idx]) < 0.01) {
			_anim.SetBool ("IsWalking",false);
			_idx++;
			if (_idx < _waypoints.Count)
				_currentWaypoints = _waypoints [_idx];
		}
	
	}

	private void SetAnimation( float vSpeed, float hSpeed ) {
		_anim.SetFloat ("vSpeed", vSpeed);
		_anim.SetFloat ("hSpeed", hSpeed);
		_anim.SetBool ("IsWalking",true);
	}

	public static float AngleDegrees(Vector2 centre, Vector2 target)
	{
		Vector2 dir = target - centre;
		float f = Mathf.Atan2 (dir.y, dir.x);
		float dec = f * Mathf.Rad2Deg;
		if (dec < 0)
			dec += 360;

		return dec;
	}



}
