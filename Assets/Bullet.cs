using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 1000f;

	// Use this for initialization
	void Start () {
	
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

    // Update is called once per frame
    void Update () {
	
	}
}
