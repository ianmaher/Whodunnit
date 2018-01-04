using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SwitchHudImage : MonoBehaviour {

	public Sprite	_newImage;

	private Image _hudIcon;  
	// Use this for initialization
	void Start () {
		_hudIcon = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	public void Switch () {
		_hudIcon.sprite  = _newImage;
		_hudIcon.color = Color.gray;
	}
}
