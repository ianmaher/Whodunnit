using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic {

	private static GameLogic _instance = null;
	private static object _syncLock = new Object();

	private GameObject _wayPointsParent;
	private GameObject _suspectsParent;
	public GameObject _detective;
	public GameObject _theGame;


	private Dictionary<string, Vector3> _wayPoints;
	private Dictionary<string, IKiller> _suspects;
	private string _accused;
	private bool _isRight;

	private List<string> _waypointNames;
	private List<string> _roomNames;



	public static GameLogic Instance 
	{
		get {
			lock (_syncLock) {
				if (GameLogic._instance == null) {
					GameLogic._instance = new GameLogic();

				}
				return GameLogic._instance;
			}
		}

	}

	GameLogic() {
		theGamesAFoot = false;
	}

	private void CreateWaypointMap() {

		_wayPoints = new Dictionary<string, Vector3> ();
		_waypointNames = new List<string> ();
		_roomNames = new List<string> ();


		for (int i = 0 ; i < _wayPointsParent.transform.childCount; i++ ) {
			Transform child = _wayPointsParent.transform.GetChild(i);

			_wayPoints.Add (child.name, child.position);
			_waypointNames.Add (child.name);

			if (!child.name.Contains ("_")) {
				Debug.Log ("add " + child.name);
				_roomNames.Add(child.name);
			}

		}
	}

	private void CreateSuspectMap() {
		_suspects = new Dictionary<string, IKiller> ();

		for (int i = 0 ; i < _suspectsParent.transform.childCount; i++ ) {
			Transform child = _suspectsParent.transform.GetChild(i);

			_suspects.Add (child.name, child.GetComponent<IKiller>());

			Debug.Log (child.name + " added to suspects");
		}

	}

	private void SelectKiller() {
		int rand = Random.Range(0, _suspects.Count);
		Transform child = _suspectsParent.transform.GetChild(rand);

		child.GetComponent<IKiller> ().MakeKiller ();
		Debug.Log (child.name + " is the killer. Rand is " + rand.ToString());

	}

	public GameObject WaypointsParent {
		set { 
			_wayPointsParent = value;
			CreateWaypointMap ();
		}
	}

	public GameObject SuspectsParent {
		set {
			_suspectsParent = value;
			CreateSuspectMap ();
			SelectKiller ();	
		}
	}
	public string GetRandomRoom () {
		if (_roomNames != null) {
			int r = _roomNames.Count - 1;
			string room = _roomNames [Random.Range (0, r)];
			return room;
		} else { 
			return string.Empty;
		}
	}

	public List<string> WaypointNames {
		get {
			return _waypointNames;
		}
	}
	public Dictionary<string, Vector3> Waypoints {
		get {
			return _wayPoints;
		}
	}

	public void AccuseSuspect(string suspect) {
		if (GameLogic.Instance.theGamesAFoot == true) {
			_accused = suspect;

			_isRight = _suspects [suspect].IsKiller ();
			GameLogic.Instance.theGamesAFoot = false;

			UnityEngine.SceneManagement.SceneManager.LoadScene ("Verdict");
			;
		}
	}

	public void OutOfTime() {
		_accused = string.Empty;
		_isRight = false;
		GameLogic.Instance.theGamesAFoot=false;

		UnityEngine.SceneManagement.SceneManager.LoadScene ("Verdict");;

	}

	public string Accused() {
		return _accused;
	}
	public bool IsRight() {
		return _isRight;
	}

	public bool theGamesAFoot {
		get;
		set;
	}
}
