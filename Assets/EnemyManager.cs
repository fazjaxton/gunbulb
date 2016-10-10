using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    public Enemy[] enemyPrefabs = new Enemy[0];
    private Vector3 upperRight = new Vector3(1.0f, 1.0f);
    private Vector3 upperLeft =  new Vector3(0f, 1.0f);
    private Vector3 lowerRight = new Vector3(1.0f, 0f);
    private float lastSpawn;
    private float spawnInterval = 5.0f;

    float getRightPos() {
        Vector3 point = Camera.main.ViewportToWorldPoint(upperRight);
        return point.x;
    }

    float getLeftPos() {
        Vector3 point = Camera.main.ViewportToWorldPoint(upperLeft);
        return point.x;
    }

    float getRandY() {
        float top = Camera.main.ViewportToWorldPoint(upperRight).y;
        float bottom = Camera.main.ViewportToWorldPoint(lowerRight).y;
        return Random.Range(bottom, top);
    }

    Enemy pickRandomEnemy() {
        int idx = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[idx];
    }

    void spawnEnemy() {
        Enemy enemyPrefab = pickRandomEnemy();
        Sprite s = enemyPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;

        float startX = getRightPos() + s.border.y / 2;
        float endX   = getLeftPos() - s.border.y / 2;
        float startY = getRandY();
        float endY =   getRandY();

        Vector3 pos = new Vector3(startX, startY, 0);
        Enemy e = (Enemy)Instantiate(enemyPrefab);
        e.gameObject.transform.SetParent(this.gameObject.transform, true);
        e.setPath(new Vector3(startX, startY), new Vector3(endX, endY, 0));
    }

    void checkSpawn() {
        if (getRightPos() >= lastSpawn + spawnInterval) {
            spawnEnemy();
            lastSpawn += spawnInterval;
        }
    }

	// Use this for initialization
	void Start () {
        lastSpawn = getRightPos();
	}
	
	// Update is called once per frame
	void Update () {
        checkSpawn();
	}
}
