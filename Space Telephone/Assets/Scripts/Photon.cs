using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : MonoBehaviour
{
    public Vector2 direction, newDir;
    float offset, speed = 10f, radians, sin, cos, tx, ty;
    double gravitation;

    GameObject controller, blackhole;

	// Use this for initialization
	void Start ()
    {
        offset = 0;
        controller = GameObject.Find("Game Controller");
        blackhole = controller.GetComponent<Controller>().blackhole;

        radians = 90 * Mathf.Deg2Rad;
        sin = Mathf.Sin(radians);
        cos = Mathf.Cos(radians);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Photon movement
        gravitation = 10 / Mathf.Pow(Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(blackhole.transform.position)), 2);
        gravitation = gravitation * Mathf.Sign(Vector2.SignedAngle(direction, Camera.main.ScreenToWorldPoint(blackhole.transform.position) - transform.position));
        print(gravitation);
        direction = Quaternion.Euler(0, 0, (float)gravitation) * direction;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        //Wavelength movement
        tx = direction.x;
        ty = direction.y;

        newDir = new Vector2((cos * tx) - (sin * ty), (sin * tx) + (cos * ty));

        transform.GetChild(0).position = (Vector2)transform.position - newDir.normalized * 0.5f * Mathf.Sin(50 * offset);
        offset += Time.deltaTime;
    }
}
