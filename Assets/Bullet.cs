using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 1000f;
    public int damage = 100;

    private int enemyLayer = 0;

	// Use this for initialization
	void Start () {
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    public void setDir (float dir)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float x;
        if (dir < 0)
            x = -speed;
        else
            x = speed;

        rb.velocity = new Vector2(x, 0);
        Debug.Log(rb.velocity);
    }


    public void OnTriggerEnter2D(Collider2D c) {
        Debug.Log("Trigger");
        if (c.gameObject.layer == enemyLayer) {
            Enemy e = c.gameObject.GetComponent<Enemy>();
            e.takeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
