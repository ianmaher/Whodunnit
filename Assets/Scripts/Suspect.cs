using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspect : MonoBehaviour, ISuspect {

	private NPCController _controller;
	private bool _isDead = false;
	private SpriteRenderer _sprite;
	private GameObject _hud;
	private SwitchHudImage _hudIcon;


	// Use this for initialization
	void Start () {
		_controller = GetComponent<NPCController> ();
		_sprite = GetComponent<SpriteRenderer> ();
		_hud = GameObject.FindGameObjectWithTag ("hud");

		foreach (SwitchHudImage sw in _hud.GetComponentsInChildren<SwitchHudImage>()) {
			if (sw.name == gameObject.name) {
				_hudIcon = sw;
				break;
			}
		}

	}

	public void Attack(GameObject killer) {
		if (!IsDead ()) {
			_controller.Kill ();
			_isDead = true;
			_hudIcon.Switch ();
			GameLogic.Instance.theGamesAFoot = true;
			_sprite.sortingOrder -= 10;
			_sprite.color = Color.gray;
		}

	}

	public bool IsDead() {
		return _isDead;
	}
}
