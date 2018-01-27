using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public GameObject sourcePrefab, destinationPrefab, photonPrefab;
    GameObject source, destination, photon, canvas;

    public Vector2 sourceLocation, destinationLocation;
    Vector2 mousePos;

    float angle = 0f;
    Vector2 objectPos, direction;

	// Use this for initialization
	void Start ()
    {
        sourceLocation = new Vector2(-13, 2);
        destinationLocation = new Vector2(13, -3);
        canvas = GameObject.Find("Canvas");

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
            photon.transform.position += (Vector3)direction * 10f * Time.deltaTime;
        }
	}
}
