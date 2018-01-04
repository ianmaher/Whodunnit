using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventHandler : MonoBehaviour {

	// Use this for initialization
	public void onClick()
	{
		Debug.Log ("Load Game");
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
	}

}
