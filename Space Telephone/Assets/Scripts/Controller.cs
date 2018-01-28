using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public GameObject sourcePrefab, destinationPrefab, photonPrefab;
    GameObject source, destination, photon, canvas, blackhole;

    public Vector2 sourceLocation, destinationLocation;
    Vector2 mousePos;

    float angle = 0f, speed = 10f;
    double gravitation = 0;
    Vector2 objectPos, direction;

	// Use this for initialization
	void Start ()
    {
        sourceLocation = new Vector2(-7, 2);
        destinationLocation = new Vector2(7, -3);
        canvas = GameObject.Find("Canvas");
        blackhole = GameObject.Find("Blackhole");

        source = Instantiate(sourcePrefab, canvas.transform);
        source.transform.position = Camera.main.WorldToScreenPoint(sourceLocation);
        destination = Instantiate(destinationPrefab, canvas.transform);
        destination.transform.position = Camera.main.WorldToScreenPoint(destinationLocation);
    }
	
	// Update is called once per frame
	void Update ()
    {
        mousePos = Input.mousePosition;
        objectPos = source.transform.GetChild(0).position;
        mousePos = mousePos - objectPos;
        angle = (Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg) - 90f;
        source.transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButtonDown(0))
        {
            Destroy(photon);
            photon = Instantiate(photonPrefab, sourceLocation, Quaternion.identity);
            direction = mousePos.normalized;
        }

        if (photon != null)
        {
            gravitation = 10/Mathf.Pow(Vector2.Distance(photon.transform.position, Camera.main.ScreenToWorldPoint(blackhole.transform.position)),2);
            gravitation = gravitation * Mathf.Sign(Vector2.SignedAngle(direction, Camera.main.ScreenToWorldPoint(blackhole.transform.position) - photon.transform.position));
            print(gravitation);
            direction = Quaternion.Euler(0, 0, (float)gravitation) * direction;
            photon.transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
	}
}
