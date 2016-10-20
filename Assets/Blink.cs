using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Blink : MonoBehaviour {

    public float interval;

    private Scene gameScene;

    IEnumerator toggle() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        while (true) {
            yield return new WaitForSeconds(interval);
            sr.enabled = !sr.enabled;
        }
    }

    // Use this for initialization
    void Start () {
        StartCoroutine("toggle");
        Debug.Log(gameScene);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("level1", LoadSceneMode.Single);
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
	}
}
