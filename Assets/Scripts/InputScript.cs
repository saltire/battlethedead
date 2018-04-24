using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour {
	KeyCode[] letters = {
		KeyCode.A,
		KeyCode.B,
		KeyCode.C,
		KeyCode.D,
		KeyCode.E,
		KeyCode.F,
		KeyCode.G,
		KeyCode.H,
		KeyCode.I,
		KeyCode.J,
	};

	KeyCode[] numbers = {
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9,
		KeyCode.Alpha0,
		KeyCode.Keypad1,
		KeyCode.Keypad2,
		KeyCode.Keypad3,
		KeyCode.Keypad4,
		KeyCode.Keypad5,
		KeyCode.Keypad6,
		KeyCode.Keypad7,
		KeyCode.Keypad8,
		KeyCode.Keypad9,
		KeyCode.Keypad0,
	};

	int letter = -1;
	int number = -1;
	InputDisplayScript letterDisplay;
	InputDisplayScript numberDisplay;
	MonsterSpawner spawner;
	AmmoScript ammo;
	ShotScript shot;
	SpriteRenderer reload;

	float flashReload = 0;

	void Start() {
		letterDisplay = transform.Find("letter").GetComponent<InputDisplayScript>();
		numberDisplay = transform.Find("number").GetComponent<InputDisplayScript>();
		spawner = GetComponentInChildren<MonsterSpawner>();
		ammo = GetComponentInChildren<AmmoScript>();
		shot = GetComponentInChildren<ShotScript>();
		reload = transform.Find("reload").GetComponent<SpriteRenderer>();
	}

	void Update() {
		if (flashReload > 0) {
			reload.color = reload.color == Color.white ? Color.clear : Color.white;
			flashReload -= Time.deltaTime;
			if (flashReload <= 0) {
				reload.color = Color.clear;
			}
		}

		if (Input.anyKeyDown) {
			if (letter == -1) {
				Debug.Log("letter");
				for (int i = 0; i < 10; i++) {
					if (Input.GetKeyDown(letters[i])) {
						letter = i;
						letterDisplay.setSprite(i, Color.yellow);
						numberDisplay.clearSprite();
						break;
					}
				}
			}
			else if (number == -1) {
				Debug.Log("number");
				for (int i = 0; i < 10; i++) {
					if (Input.GetKeyDown(numbers[i]) || Input.GetKeyDown(numbers[i + 10])) {
						number = i;
						numberDisplay.setSprite(i, Color.yellow);
						fire();
						break;
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.R)) {
				ammo.reload();
			}
		}
	}

	void fire() {
		Debug.Log("fire");
		if (ammo.bulletsLeft() == 0) {
			flashReload = 0.25f;
		}
		else {
			Debug.Log("go");
			Color color = spawner.fireAtMonsters(number, letter) ? Color.red : Color.white;
			Debug.Log(color);

			ammo.useBullet();
			shot.createShot(number, letter);

			letterDisplay.setColor(color);
			numberDisplay.setColor(color);
		}

		letterDisplay.fadeOut(0, false);
		numberDisplay.fadeOut(0, false);

		letter = -1;
		number = -1;

		Debug.Log("done");
	}
}
