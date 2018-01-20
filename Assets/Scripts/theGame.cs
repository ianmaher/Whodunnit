using System;
using UnityEngine;
using UnityEngine.UI;


public class theGame : MonoBehaviour {

	public GameLogic _gameLogic;

	public GameObject _wayPointsParent;
	public GameObject _suspectsParent;
	public GameObject _timerFinger;
	public GameObject _clockFace;
	public AudioSource _announceAMurder;
	public AudioSource _themeTune;

	//public Text _timer;


	public float _gameMaxTime = 60.0f;
	public float _userVolumeSetting = 0.5f;

	private float _gameTime = 0f;
	private bool _clockStarted = false;
	private const float _secondsToDegrees = -360f / 60f; 

	// Use this for initialization
	void Start () {
		GameLogic.Instance.WaypointsParent = _wayPointsParent;
		GameLogic.Instance.SuspectsParent = _suspectsParent;
		GameLogic.Instance._theGame = gameObject;

		if (!PlayerPrefs.HasKey ("VolumeOn")) {
			PlayerPrefs.SetFloat ("VolumeOn", _userVolumeSetting);
		}
		Debug.Log ("Vol = " + PlayerPrefs.GetFloat ("VolumeOn"));
		if (_themeTune != null) {

			_themeTune.volume = PlayerPrefs.GetFloat ("VolumeOn");

			if (_themeTune.volume > 0) {
				_themeTune.Play ();
			}
		}

		
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

		if (_announceAMurder != null) {
			_announceAMurder.volume = _userVolumeSetting;
			if ( _announceAMurder.volume > 0 ) {
				_announceAMurder.Play ();
			}
		}
		//AE0039FF
	}
	public void ToggleTheme() {

		if (_themeTune != null) {
			if (_themeTune.volume == 0) {
				PlayerPrefs.SetFloat ("VolumeOn", _userVolumeSetting);
				_themeTune.volume = _userVolumeSetting;
			} else {
				PlayerPrefs.SetFloat ("VolumeOn", 0);
				_themeTune.volume = 0;
			}

			if (_themeTune.volume > 0.1) {
				_themeTune.Play ();
			}
		}

	}
	
}
