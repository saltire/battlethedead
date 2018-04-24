using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {
	public GameObject shot;

	public void createShot(int x, int y) {
		Debug.Log("create shot " + x + " " + y);
		GameObject newShot = (GameObject)Instantiate(shot,
			transform.position + new Vector3(x + 0.5f, -(y + 0.5f), -5), Quaternion.identity);

		Debug.Log(newShot);

		newShot.GetComponent<InputDisplayScript>().fadeOut(0.5f, true);
	}
}
