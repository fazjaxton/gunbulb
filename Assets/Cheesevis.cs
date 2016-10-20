using UnityEngine;
using System.Collections;

public class Cheesevis : MonoBehaviour {

    public GameObject[] weapons;
    public float throwForceMin;
    public float throwForceMax;
    public float throwAngleMin;
    public float throwAngleMax;
    public float walkSpeed;
    public float walkDist;

    private Vector3 startPos;
    private Transform weaponSpawn;
    private float walkDir;
    private Enemy enemy;

    void throwWeapon() {
        int idx = Random.Range(0, weapons.Length);
        GameObject w = (GameObject)Instantiate(weapons[idx], weaponSpawn.position, Quaternion.identity);
        Rigidbody2D rb = w.GetComponent<Rigidbody2D>();
        float force = Random.Range(throwForceMin, throwForceMax);
        float angle = Random.Range(throwAngleMin, throwAngleMax) * Mathf.Deg2Rad;
        rb.velocity = new Vector3(-Mathf.Cos(angle), Mathf.Sin(angle)) * force;
    }

	// Use this for initialization
	void Start () {
        weaponSpawn = transform.Find("WeaponSpawn");
        startPos = transform.position;
        walkDir = 1;
        enemy = GetComponent<Enemy>();
	}

    private float time = 0f;

	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= 2.0f && !enemy.isDead()) {
            throwWeapon();
            time -= 2.0f;
        }

        if (walkDir < 0 && transform.position.x < startPos.x - walkDist) {
            walkDir = -walkDir;
        } else if (walkDir > 0 && transform.position.x >= startPos.x) {
            walkDir = -walkDir;
        }

        transform.Translate(new Vector3(walkDir * Time.deltaTime * walkSpeed, 0, 0));
	}
}
