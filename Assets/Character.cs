using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public Transform point1;
    public Transform point2;
    public float castDistance = 0.1f;
    public float moveSpeed = 20f;

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
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Debug.Log("OnGround");
            onGround = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
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

    void FixedUpdate ()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float motion = Input.GetAxis("Horizontal");
        float move = motion * moveSpeed * Time.fixedDeltaTime;
        rb.velocity = new Vector2(move, rb.velocity.y);
    }
	// Update is called once per frame
	void Update () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Input.GetButtonDown("Jump"))
        {
            if (onGround)
            {
                Debug.Log("Jump");
                rb.AddForce(Vector2.up * 300);
            }
            else
            {
                Debug.Log("Not on ground");
            }
        }
	}
}
