using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    public float moveSpeed = 20f;
    public float jumpForce = 300f;
    public float fireRate = 5f;
    public Bullet bullet;
    public int jumps = 2;
    public int maxHealth = 5;
    public Color healthColor;

    private int surfaceLayerIdx;
    private int enemyLayerIdx;
    private int waterLayerIdx;
    private bool onGround;
    private float lastFire = 0;
    private float dirFacing = 1f;
    private int jumpsRemaining;
    private bool invincible;
    private bool controllable = true;
    private int health;
    private bool dead;

    void faceRight() {
        dirFacing = 1f;
        transform.localScale = new Vector3(-1, 1, 1);
    }

    void faceLeft() {
        dirFacing = -1f;
        transform.localScale = new Vector3(1, 1, 1);
    }

    void updateColor (float fraction) {
        Color currentColor;

        currentColor = Color.Lerp(Color.white, healthColor, fraction);
        Debug.Log(currentColor);
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        r.color = currentColor;
    }

    IEnumerator restartLevel (float after) {
        yield return new WaitForSeconds(after);
        int idx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idx, LoadSceneMode.Single);
    }

    void die() {
        if (!dead) {
            controllable = false;
            SpriteRenderer r = GetComponent<SpriteRenderer>();
            r.flipY = true;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(0, 300));
            gameObject.layer = LayerMask.NameToLayer("Dead");
            StartCoroutine("restartLevel", 2.0f);
        }
    }

    void injure(int amount) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        health--;
        updateColor((float)health / maxHealth);
        if (health <= 0)
            die();
        Debug.Log(health);
        rb.velocity = Vector3.zero;
        disableControl(.2f);
        rb.AddForce(new Vector2(-500, 500));
    }

    IEnumerator _setInvincible (float time) {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    void setInvincible (float time) {
        StartCoroutine("_setInvincible", time);
    }

    IEnumerator _disableControl (float time) {
        controllable = false;
        if (time >= 0) {
            yield return new WaitForSeconds(time);
            controllable = true;
        }
    }

    public void disableControl (float time) {
        StartCoroutine("_disableControl", time);
    }

	void Start () {
        surfaceLayerIdx = LayerMask.NameToLayer("Surface");
        enemyLayerIdx = LayerMask.NameToLayer("Enemy");
        waterLayerIdx = LayerMask.NameToLayer("Water");
        faceRight();
        updateColor(1.0f);
        health = maxHealth;
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
        } else if (c.gameObject.layer == enemyLayerIdx) {
            if (!invincible) {
                setInvincible(1.0f);
                injure(1);
            }
        } else if (c.gameObject.layer == waterLayerIdx) {
            die();
        }
    }

	void OnTriggerExit2D (Collider2D c) {
        if (c.gameObject.layer == surfaceLayerIdx) {
            onGround = false;
            jumpsRemaining = jumps - 1;
        }
    }

    void FixedUpdate () {
        if (controllable) {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            float motion = Input.GetAxis("Horizontal");
            float move = motion * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = new Vector2(move, rb.velocity.y);
            if (motion < 0)
                faceLeft();
            else if (motion > 0)
                faceRight();
        }
    }

	// Update is called once per frame
	void Update () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (controllable) {
            if (Input.GetButtonDown("Jump")) {
                if (onGround || jumpsRemaining > 0) {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(Vector2.up * jumpForce);
                    jumpsRemaining--;
                }
            }
            if (Input.GetButton("Fire1")) {
                fireAction();
            }
        }
	}
}
