using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour {

    public GameObject floor;
    public int leadIn = 5;
    public int levelLen = 30;
    public float heightInterval = 2f;
    public int platformLevels = 3;

	// Use this for initialization
    void addFloor(int segment, int height) {
        Instantiate(floor, new Vector3(segment * 5f, height * heightInterval),
                                        Quaternion.identity, this.gameObject.transform);
    }

    void generateLevel() {
        int i;
        int pos = 0;

        for (i = 0; i < leadIn; i++, pos++) {
            addFloor(pos, 0);
        }
        for (i = 0; i < levelLen; i++, pos++) {
            int min = 0;
            int max = (1 << platformLevels) - 1;

            int bitfield = Random.Range(min, max);
            for (int l = 0; l < platformLevels; l++) {
                if ((bitfield & (1 << l)) > 0)
                    addFloor(pos, l);
            }
        }
    }

    void Start () {
        generateLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
