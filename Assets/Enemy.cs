using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float velocity = 5;
    public float sineHeight = 3;
    public float sineSpeed = 5;
    public int health = 10;

    private delegate void MoveFunc();
    MoveFunc move;
    private Vector3 startPos;
    private float startTime;
    private Vector3 dir;
    private Vector3 dirUp;

    void sineMove() {
        float deltaT = Time.time - startTime;
        Vector3 pos = startPos + velocity * deltaT * dir;
        pos += dirUp * sineHeight * Mathf.Sin(deltaT * sineSpeed);
        transform.position = pos;
    }

    void lineMove() {
        float deltaT = Time.time - startTime;
        transform.position = startPos + dir * velocity * deltaT;
    }

    public void setPath(Vector3 start, Vector3 end) {
        dir = (end - start).normalized;
        dirUp = new Vector3(dir.y, -dir.x, 0);

        startPos = start;
    }

    void swirlMove() {
        float deltaT = Time.time - startTime;
        Vector3 pos = startPos + dir * velocity * deltaT;
        pos += dirUp * sineHeight * Mathf.Sin(deltaT * sineSpeed);
        pos += dir * sineHeight * Mathf.Cos(deltaT * sineSpeed);
        transform.position = pos;
    }

    void die() {
        Destroy(this.gameObject);
    }

    public void takeDamage(int damage) {
        health -= damage;
        if (health <= 0)
            die();
    }

    void Ready () {

    }

	// Use this for initialization
	void Start () {
        MoveFunc[] functions = { lineMove, swirlMove, sineMove };
        startTime = Time.time;

        int moveIdx = Random.Range(0, functions.Length);
        move = functions[moveIdx];
	}
	
	// Update is called once per frame
	void Update () {
        move();
        if (transform.position.x < 0)
            die();
	}
}
