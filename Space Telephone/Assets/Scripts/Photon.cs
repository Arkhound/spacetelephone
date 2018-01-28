using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : MonoBehaviour
{
    public Vector2 direction, newDir, newDir2;
    float offset, radians, sin, cos, tx, ty, ux, uy;
    public float wavelengthFactor = 10f, amplitudeFactor = 0.5f;
    double gravitation;


    public float minDist;
    public float maxDist;

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
        Vector3 blackholePos = Camera.main.ScreenToWorldPoint(blackhole.transform.position);

        //Photon movement
        gravitation = 10 / Mathf.Pow(Vector2.Distance(transform.position, blackholePos), 2);
        gravitation = gravitation * Mathf.Sign(Vector2.SignedAngle(direction, blackholePos - transform.position));
        direction = Quaternion.Euler(0, 0, (float)gravitation) * direction;

        ux = (blackholePos - transform.position).x;
        uy = (blackholePos - transform.position).y;
        newDir2 = new Vector2((cos * ux) - (sin * uy), (sin * ux) + (cos * uy));

        wavelengthFactor -= Mathf.Sign(Vector2.SignedAngle(direction, newDir2)) * 
             Vector2.Distance(transform.position, blackholePos) * 0.015f;

        transform.position += (Vector3)direction * wavelengthFactor * Time.deltaTime;

        //Wavelength movement
        tx = direction.x;
        ty = direction.y;

        newDir = new Vector2((cos * tx) - (sin * ty), (sin * tx) + (cos * ty));
        

        transform.GetChild(0).position = (Vector2)transform.position - newDir.normalized * amplitudeFactor * Mathf.Sin(50 * offset);
        offset += Time.deltaTime;
    }
}
