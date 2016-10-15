using UnityEngine;
using System.Collections;

public class FollowCharacter : MonoBehaviour {

    public GameObject character;
    public float forward;
    public float backward;
    //public float up;
    //public float down;
    //public float maxHeight;

    private Vector3 viewportRightVect;
    private float xres;
    private float yres;

	// Use this for initialization
	void Start () {
        Camera cam = Camera.main;
        yres = cam.orthographicSize;
        xres = yres * cam.aspect;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = character.transform.position - transform.position;

        if (pos.x > xres * forward)
            transform.Translate(pos.x - (xres * forward), 0, 0);
        if (pos.x < xres * -backward)
            transform.Translate(pos.x - (xres * -backward), 0, 0);

        //if (pos.y > yres * up)
            //transform.Translate(0, pos.y - (yres * up), 0);
        //if (pos.y < yres * -down)
            //transform.Translate(0, pos.y - (yres * -down), 0);

        //float above = transform.position.y - maxHeight;
        //if (above > 0) {
            //transform.Translate(0, -above, 0);
        //}
    }
}
