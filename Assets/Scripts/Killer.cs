using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour, IKiller {

	private bool _isKiller = false;
	private IDetective _detective;
	private string _currentRoom;
	// Use this for initialization
	void Start () {
		_detective = GameObject.FindGameObjectWithTag ("Player").GetComponent<IDetective>();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (!_isKiller)
			return;

		if (coll.gameObject.tag == "waypoints") {
			CurrentRoom = coll.gameObject.name;
			Debug.Log ("Killer is now in " + CurrentRoom);
		}
		
		if (coll.gameObject.tag == "npc") {

			if (_detective.CurrentRoom == CurrentRoom) {
				Debug.Log ("Killer in same room as detecive - " + CurrentRoom);
					
				return;
			}

			Debug.Log ("npc killed" + coll.gameObject.name);
			coll.gameObject.GetComponent<ISuspect> ().Attack (gameObject);

		}
	}


	public void MakeKiller ()
	{
		_isKiller = true;
	}

	public bool IsKiller() {
		return _isKiller;
	}

	public string CurrentRoom {
		get {
			return _currentRoom;
		}
		set {
			_currentRoom = value;
		}
	}
}
