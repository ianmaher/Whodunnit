using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : MonoBehaviour, IDetective {

	private string _currentRoom;
	private string _accused;


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

	public void AccuseSuspect (GameObject suspect) {
		// TODO: move to HUD since this is a UI responsibility to stop the player from choosing a dead suspect.
		ISuspect sus = suspect.GetComponent<ISuspect>();

		if (sus != null && sus.IsDead () != true) {
			GameLogic.Instance.AccuseSuspect (suspect.name);
		}
	}
	#endregion
}
