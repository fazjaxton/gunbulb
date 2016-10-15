using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    public PylonManager pylonManager;
    public Character character;
    public GameObject victory;

    IEnumerator _bossDead() {
        yield return new WaitForSeconds(2);
        victory.GetComponent<SpriteRenderer>().enabled = true;
        pylonManager.setX(character.transform.position.x);
        pylonManager.pylonShower();
    }

    public void bossDead() {
        character.disableControl(-1);
        Rigidbody2D rb = character.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        StartCoroutine("_bossDead");
    }

	// Use this for initialization
	void Start () {
        pylonManager = FindObjectOfType<PylonManager>();
        character = FindObjectOfType<Character>();
        victory = GameObject.Find("Victory");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
