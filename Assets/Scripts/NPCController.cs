using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

	private Animator _anim;
	private List<Transform> _objsToFollow;
	public float _speed = 2.0f;
	public float _maxWait = 5.0f;
//	public GameObject _wayPointsParent;
//	private Dictionary<string, Vector3> _wayPoints;
//	private List<string> _waypointNames;
//	private List<string> _roomNames;

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
			/*
			Debug.Log("NPC Dead");
			_isDead = true;
			_anim.SetTrigger ("Death");
			*/
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
	/*
	private void CreateWaypointMap() {

		_wayPoints = new Dictionary<string, Vector3> ();
		_waypointNames = new List<string> ();
		_roomNames = new List<string> ();


		for (int i = 0 ; i < _wayPointsParent.transform.childCount; i++ ) {
			Transform child = _wayPointsParent.transform.GetChild(i);
		
			_wayPoints.Add (child.name, child.position);
			_waypointNames.Add (child.name);

			if (!child.name.Contains ("_")) {
				_roomNames.Add(child.name);
			}

		}
	}
	*/

	private void GotoWayPoint () {

		if (_isDead)
			return;
		
		string target = GameLogic.Instance.GetRandomRoom (); // _roomNames[Random.Range (0, 1)];//_roomNames[Random.Range (0, _roomNames.Count)];
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
