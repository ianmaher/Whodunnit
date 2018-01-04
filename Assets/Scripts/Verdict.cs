using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Verdict : MonoBehaviour {

	void Update () {
		if (GameLogic.Instance.IsRight ()) {
			gameObject.GetComponent<Text> ().text = "Congratulations.\n\nYou have correctly accused " + GameLogic.Instance.Accused () + " of being the killer this time.";
		} else if (GameLogic.Instance.Accused () == string.Empty) {
			gameObject.GetComponent<Text> ().text = "Sorry!\n\nYou ran out of time. Better luck next time.";
		} else { 	
			gameObject.GetComponent<Text> ().text = "Bad luck!\n\nYou accused the wrong suspect. " + GameLogic.Instance.Accused () + " is innocent.";
		}
		
	}

	public void StartAgain() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
	}

}
