using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : MonoBehaviour, IDetective {

	private string _currentRoom;
	private string _accused;


	void Update() {

		if (Input.GetKeyDown (KeyCode.E)) {
			GameObject suspect = FindClosestSuspect();

			Debug.Log ("Detective next to " + suspect.name);
			AccuseSuspect (suspect.name);
		}

	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		if (coll.gameObject.tag == "waypoints") {
			CurrentRoom = coll.gameObject.name;
			Debug.Log ("Detective is now in " + CurrentRoom);
		}

	}


	private GameObject FindClosestSuspect(string tag = "npc")
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag(tag);
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos)
		{
			if (go.GetComponent<ISuspect> ().IsDead() == false) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
			}
		}
		return closest;
	}

	#region IDetective implementation

	public string CurrentRoom {
		get {
			return _currentRoom;
		}
		set {
			_currentRoom = value;
		}
	}

	public void AccuseSuspect (string suspect) {
		GameLogic.Instance.AccuseSuspect(suspect);

	}
	#endregion
}
