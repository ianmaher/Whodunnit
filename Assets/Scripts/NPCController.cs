using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

	private Animator _anim;
	private List<Transform> _objsToFollow;
	public float _speed = 2.0f;
	public float _maxWait = 5.0f;

	private PathWalker _pathWalker;
	private bool _isMoving = false;
	private bool _isWaiting = false;
	private bool _isDead = false;


	void Start () {
		_anim = GetComponent<Animator> ();
		//CreateWaypointMap ();

		_pathWalker = new PathWalker (this.gameObject, _anim);

		GotoWayPoint ();

	}


	// Update is called once per frame
	void FixedUpdate () {
		if (_isDead)
			return;
		
		if (_isMoving && !_isDead)
			_isMoving = _pathWalker.Move ();
		
		else if (!_isMoving && !_isWaiting && !_isDead) {
			StartCoroutine (WaitAbit ());
		}

	}
		
	private IEnumerator  WaitAbit () {
		_isWaiting = true;
		yield return new WaitForSeconds(Random.Range (0, _maxWait)); 
		GotoWayPoint();
		_isMoving = true;
		_isWaiting = false;

	}
	public void Kill() {
		if (!_isDead) {
			Debug.Log (gameObject.name + " has been killer");
			_isDead = true;
			_anim.SetTrigger ("Death");
			_pathWalker = null;
		}
	}

	private void GotoWayPoint () {

		if (_isDead)
			return;
		
		string target = GameLogic.Instance.GetRandomRoom (); 
		if (target == string.Empty) return;
		Debug.Log(gameObject.name + " go to " + target);

		string currentWayPoint = string.Empty;

		foreach(string wp in GameLogic.Instance.WaypointNames)
		{
			if(currentWayPoint == string.Empty)
			{
				currentWayPoint = wp;
			}
			//compares distances
			if(Vector3.Distance(transform.position, GameLogic.Instance.Waypoints[wp]) <= Vector3.Distance(transform.position, GameLogic.Instance.Waypoints[currentWayPoint]))
			{
				currentWayPoint = wp;
			}
		}

		List<Vector3> objsToFollow;

		objsToFollow = new List<Vector3>();
		objsToFollow.Add(GameLogic.Instance.Waypoints[currentWayPoint]);
		objsToFollow.Add(GameLogic.Instance.Waypoints[currentWayPoint + "_Out"]);
		objsToFollow.Add(GameLogic.Instance.Waypoints[currentWayPoint+ "_In"]);


		objsToFollow.Add(GameLogic.Instance.Waypoints[target + "_In"]);
		objsToFollow.Add(GameLogic.Instance.Waypoints[target+ "_Out"]);
		objsToFollow.Add(GameLogic.Instance.Waypoints[target]);

	
		_pathWalker.SetWayPoints(objsToFollow, _speed);



	}

}
