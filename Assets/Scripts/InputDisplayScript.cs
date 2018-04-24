using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDisplayScript : MonoBehaviour {
	public float fadeTime = 1;
	public bool letters = false;
	public bool numbers = false;

	Sprite[] sprites;
	SpriteRenderer spriter;
	float fadeDelay = 0;
	bool fading = false;
	bool destroyAfterFade = false;

	void Start() {
		spriter = GetComponent<SpriteRenderer>();

		if (letters || numbers) {
			Sprite[] fontSprites = Resources.LoadAll<Sprite>("font");
			sprites = new Sprite[10];
			for (int i = 0; i < 10; i++) {
				sprites[i] = fontSprites[numbers ? (i + 1) % 10 : i + 10];
			}
			spriter.sprite = null;
		}
	}

	void Update() {
		if (fading) {
			if (fadeDelay > 0) {
				fadeDelay -= Time.deltaTime;
				return;
			}

			Color nextColor = spriter.color;
			nextColor.a -= Time.deltaTime / fadeTime;
			spriter.color = nextColor;

			if (destroyAfterFade && nextColor.a <= 0) {
				Destroy(gameObject);
			}
		}
	}

	public void setSprite(int index) {
		spriter.sprite = sprites[index];
		fading = false;
	}

	public void setSprite(int index, Color color) {
		spriter.sprite = sprites[index];
		spriter.color = color;
		fading = false;
	}

	public void clearSprite() {
		spriter.sprite = null;
	}

	public void setColor(Color color) {
		spriter.color = color;
	}

	public void fadeOut(float delay, bool destroyAfter) {
		fadeDelay = delay;
		fading = true;
		destroyAfterFade = destroyAfter;
	}
}
