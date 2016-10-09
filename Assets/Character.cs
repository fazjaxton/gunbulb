using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float moveSpeed = 20f;
    public float jumpForce = 300f;
    public float fireRate = 5f;
    public Bullet bullet;
    public int jumps = 2;

    private int surfaceLayerIdx;
    private bool onGround;
    private Sprite sprite;
    private float lastFire = 0;
    private float dirFacing = 1f;
    private int jumpsRemaining;

    void faceRight() {
        dirFacing = 1f;
        transform.localScale = new Vector3(-1, 1, 1);
    }

    void faceLeft() {
        dirFacing = -1f;
        transform.localScale = new Vector3(1, 1, 1);
    }

	void Start () {
        surfaceLayerIdx = LayerMask.NameToLayer("Surface");
        sprite = GetComponent<Sprite>();
        faceRight();
	}

    void spawnBullet () {
        Transform muzzle = transform.Find("Muzzle");
        Bullet b = (Bullet)Instantiate(bullet, muzzle.position, Quaternion.identity);
        b.setDir(dirFacing);
    }

    void fireAction () {
        float now = Time.time;
        if (now - lastFire >= 1 / fireRate) {
            spawnBullet();
            lastFire = now;
        }
    }
	void OnTriggerEnter2D (Collider2D c) {
        if (c.gameObject.layer == surfaceLayerIdx) {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            onGround = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

	void OnTriggerExit2D (Collider2D c) {
        if (c.gameObject.layer == surfaceLayerIdx) {
            onGround = false;
            jumpsRemaining = jumps - 1;
        }
    }

    void FixedUpdate () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float motion = Input.GetAxis("Horizontal");
        float move = motion * moveSpeed * Time.fixedDeltaTime;
        rb.velocity = new Vector2(move, rb.velocity.y);
        if (motion < 0)
            faceLeft();
        else if (motion > 0)
            faceRight();
    }

	// Update is called once per frame
	void Update () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Input.GetButtonDown("Jump")) {
            if (onGround || jumpsRemaining > 0) {
                rb.AddForce(Vector2.up * jumpForce);
                jumpsRemaining--;
            }
        }
        if (Input.GetButton("Fire1")) {
            fireAction();
        }
	}
}
