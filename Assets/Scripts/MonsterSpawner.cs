using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {
	public int maxMonsters = 1;
	public float spawnChance = 0.01f;
	public GameObject[] monsters;

	List<MonsterScript> spawned;

	void Start() {
		spawned = new List<MonsterScript>();
	}

	void Update() {
		foreach (MonsterScript monster in spawned) {
			if (monster.isDestroyed()) {
				spawned.Remove(monster);
			}
		}

		if (spawned.Count < maxMonsters && Random.value < spawnChance) {
			GameObject monsterObj = Instantiate(monsters[Random.Range(0, monsters.Length)], transform);
			MonsterScript monster = monsterObj.GetComponent<MonsterScript>();
			monster.transform.localPosition += monster.getRandomOffset();
			spawned.Add(monster);

			for (int i = 0; i < spawned.Count; i++) {
				spawned[i].transform.position = new Vector3(
					spawned[i].transform.position.x, spawned[i].transform.position.y, spawned.Count - i);
			}
		}
	}

	public bool fireAtMonsters(int x, int y) {
		foreach (MonsterScript monster in spawned) {
			Debug.Log("test " + monster);
			if (!monster.isDestroyed() && monster.fireAtMonster(x, y)) {
				return true;
			}
		}

		return false;
	}
}
