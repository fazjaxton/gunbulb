using UnityEngine;
using System.Collections;

public class PylonManager : MonoBehaviour {

    public GameObject pylon;
    public int count;
    public float interval;
    public float width;

    IEnumerator _pylonShower () {
        for (int i = 0; i < count; i++) {
            float xOffset = Random.Range(-(float)width / 2f, (float)width / 2f);
            Vector3 pos = new Vector3(transform.position.x + xOffset, transform.position.y);
            Instantiate(pylon, pos, Quaternion.AngleAxis(Random.Range(0, 360f), Vector3.forward));
            yield return new WaitForSeconds(interval);
        }
    }

    public void pylonShower () {
        StartCoroutine("_pylonShower");
    }

    public void setX (float x) {
        transform.position = new Vector3(x, transform.position.y, 0);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
