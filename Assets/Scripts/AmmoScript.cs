using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour {
	public int maxBullets = 6;
	public float spacing = 0.5f;
	public GameObject bullet;

	List<GameObject> bullets;

	void Start() {
		bullets = new List<GameObject>();
		reload();
	}

	public int bulletsLeft() {
		return bullets.Count;
	}

	public void useBullet() {
		Destroy(bullets[bullets.Count - 1]);
		bullets.RemoveAt(bullets.Count - 1);
	}

	public void reload() {
		while (bullets.Count < maxBullets) {
			bullets.Add(Instantiate(bullet,
				transform.position + new Vector3(spacing * bullets.Count, 0, 0), Quaternion.identity));
		}
	}
}
