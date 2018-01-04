using System;
using UnityEngine;
using UnityEngine.UI;


public class theGame : MonoBehaviour {

	//public static GameLogic _gameLogic;

	public GameObject _wayPointsParent;
	public GameObject _suspectsParent;
	public GameObject _timerFinger;
	public GameObject _clockFace;
	public AudioSource _music;
	//public Text _timer;


	public float _gameMaxTime = 60.0f;
	private float _gameTime = 0f;
	private bool _clockStarted = false;
	private const float _secondsToDegrees = -360f / 60f; 

	// Use this for initialization
	void Start () {
		GameLogic.Instance.WaypointsParent = _wayPointsParent;
		GameLogic.Instance.SuspectsParent = _suspectsParent;
		GameLogic.Instance._theGame = gameObject;
		
	}
	void Update() {
		if (GameLogic.Instance.theGamesAFoot) {
			if (!_clockStarted)
				StartTheClock ();
			_gameTime += Time.deltaTime;

			if (_gameTime > _gameMaxTime) {
				GameLogic.Instance.OutOfTime ();
			}
		
			_timerFinger.transform.localRotation = Quaternion.Euler (0, 0, (int)_gameTime * _secondsToDegrees);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			ToggleTheme ();
		}

	}

	private  string SecondsToMinutes(int seconds)
	{
		var ts = new TimeSpan(0, 0, seconds);                
		return new DateTime(ts.Ticks).ToString(seconds >= 3600 ? "hh:mm:ss" : "mm:ss");
	}

	private void StartTheClock() {
		_clockStarted = true;
		_clockFace.GetComponent<Image> ().color = new Color (140, 0, 0);
		_music.Play ();
		Debug.Log("Play the music");

		//AE0039FF
	}
	private void ToggleTheme() {
		AudioSource theme = GameObject.FindGameObjectWithTag ("theme").GetComponent<AudioSource>();

		if (theme.volume == 0)
			theme.volume = 0.1f;
		else
			theme.volume = 0;

	}
	
}
