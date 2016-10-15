using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int health = 10;

    private bool dead;

    void die() {
        dead = true;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(500, 500));
        Destroy(this.gameObject, 3.0f);
        gameObject.layer = LayerMask.NameToLayer("Dead");

        Boss b = GetComponent<Boss>();
        if (b) {
            b.bossDead();
        }
    }

    public bool isDead() {
        return dead;
    }
    public void takeDamage(int damage) {
        if (!dead) {
            health -= damage;
            if (health <= 0)
                die();
        }
    }

    void Ready () {

    }

	// Use this for initialization
	void Start () {
        dead = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
