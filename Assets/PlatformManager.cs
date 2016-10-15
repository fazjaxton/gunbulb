using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour {

    public GameObject floor;
    public GameObject cheesevis;
    public int leadIn = 5;
    public int levelLen = 30;
    public int bossLen;
    public float heightInterval = 2f;
    public int platformLevels = 3;
    public EnemyManager enemyManager;

	// Use this for initialization
    void addFloor(int segment, int height) {
        Instantiate(floor, new Vector3(segment * 5f, height * heightInterval),
                                        Quaternion.identity, this.gameObject.transform);
    }

    void addCheesevis(int segment) {
        Instantiate(cheesevis, new Vector3(segment * 5f, 3 * heightInterval),
                                        Quaternion.identity);
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
        for (i = 0; i < bossLen; i++, pos++) {
            addFloor(pos, 0);
        }
        addCheesevis(pos-2);
    }

    void Start () {
        generateLevel();
        enemyManager.setSpawnMax((leadIn + levelLen) * 5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
