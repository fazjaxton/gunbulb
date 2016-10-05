using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public Transform point1;
    public Transform point2;
    public float castDistance = 0.1f;

    private int surfaceLayerIdx;
    private bool onGround;

    bool isOnGround()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(point1.position, Vector2.down, castDistance);
        RaycastHit2D hit2 = Physics2D.Raycast(point1.position, Vector2.down, castDistance);

        return hit1 || hit2;
    }

	// Use this for initialization
	void Start () {
        surfaceLayerIdx = LayerMask.NameToLayer("Surface");
	}

	void OnTriggerEnter2D (Collider2D c)
    {
        if (c.gameObject.layer == surfaceLayerIdx)
        {
            Debug.Log("OnGround");
            onGround = true;
        }
    }

	void OnTriggerExit2D (Collider2D c)
    {
        if (c.gameObject.layer == surfaceLayerIdx)
        {
            Debug.Log("InAir");
            onGround = false;
        }
    }

	// Update is called once per frame
	void Update () {
        if (onGround && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
        }
	}
}
