using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    public Derp[] derpPrefabs = new Derp[0];
    public float spawnInterval = 5.0f;

    private Vector3 upperRight = new Vector3(1.0f, 1.0f);
    private Vector3 upperLeft =  new Vector3(0f, 1.0f);
    private Vector3 lowerRight = new Vector3(1.0f, 0f);
    private float lastSpawn;
    private float maxSpawn;

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

    Derp pickRandomDerp() {
        int idx = Random.Range(0, derpPrefabs.Length);
        return derpPrefabs[idx];
    }

    void spawnDerp() {
        Derp derpPrefab = pickRandomDerp();
        Sprite s = derpPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;

        float startX = getRightPos() + s.border.y / 2;
        float endX   = getLeftPos() - s.border.y / 2;
        float startY = getRandY();
        float endY =   getRandY();

        Vector3 start = new Vector3(startX, startY, 0);
        Vector3 end = new Vector3(endX, endY, 0);
        Derp e = (Derp)Instantiate(derpPrefab, start, Quaternion.identity);
        e.gameObject.transform.SetParent(this.gameObject.transform, true);
        e.setPath(start, end);
    }

    void checkSpawn() {
        float pos = getRightPos();

        if (pos >= lastSpawn + spawnInterval && pos < maxSpawn) {
            spawnDerp();
            lastSpawn += spawnInterval;
        }
    }

    public void setSpawnMax(float pos) {
        maxSpawn = pos;
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
