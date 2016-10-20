using UnityEngine;
using System.Collections;

public class DieBelow : MonoBehaviour {

    public float yPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < yPos) {
            Destroy(this.gameObject);
        }
	}
}
