using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{

    public GameObject sourcePrefab, destinationPrefab, photonPrefab;
    public GameObject source, destination, photon, canvas, blackhole;

    public Vector2 sourceLocation, destinationLocation;
    Vector2 mousePos;

    float angle = 0f, wavelengthFactor = 10f, amplitudeFactor = 0.5f;
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

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            wavelengthFactor -= 1f;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            wavelengthFactor += 1f;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            amplitudeFactor += 0.1f;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            amplitudeFactor -= 0.1f;

        if (Input.GetMouseButtonDown(0)/* && !EventSystem.current.IsPointerOverGameObject()*/)
        {
            Destroy(photon);
            photon = Instantiate(photonPrefab, sourceLocation, Quaternion.identity);
            photon.GetComponent<Photon>().direction = mousePos.normalized;
            photon.GetComponent<Photon>().amplitudeFactor = amplitudeFactor;
            photon.GetComponent<Photon>().wavelengthFactor = wavelengthFactor;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}
}
