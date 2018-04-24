using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour {
	public Texture2D hitmap;
	public float deathTime = 1;
	public Color[] colors;

	public int minXOffset = 0;
	public int maxXOffset = 0;
	public int minYOffset = 0;
	public int maxYOffset = 0;

	SpriteRenderer spriter;
	int ppu;
	float health = 1;

	void Start() {
		spriter = GetComponent<SpriteRenderer>();
		ppu = hitmap.width / 10;

		if (colors.Length > 0) {
			spriter.color = colors[Random.Range(0, colors.Length - 1)];
		}
	}

	void Update() {
		if (health <= 0) {
			if (deathTime > 0) {
				spriter.color = new Color(Random.value, Random.value, Random.value);
				deathTime -= Time.deltaTime;
			}
			else {
				Destroy(this.gameObject);
			}
		}
	}

	public Vector3 getRandomOffset() {
		return new Vector3(
			Random.Range(minXOffset, maxXOffset + 1), Random.Range(minYOffset, maxYOffset + 1), 0);
	}

	public bool fireAtMonster(int x, int y) {
		Color pixel = hitmap.GetPixel(
			(int)((x + 0.5f - transform.localPosition.x) * ppu),
			(int)((9.5f - y - transform.localPosition.y) * ppu));
		float hitValue = pixel.r * pixel.a;
		Debug.Log("hit value " + pixel + " " + hitValue);

		health -= hitValue;

		return hitValue > 0;
	}

	public bool isDestroyed() {
		return health <= 0 && deathTime <= 0;
	}
}
