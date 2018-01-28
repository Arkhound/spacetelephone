using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : MonoBehaviour
{
    float offset;

	// Use this for initialization
	void Start ()
    {
        offset = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.GetChild(0).position = new Vector2(transform.position.x, transform.position.y + 0.5f * Mathf.Sin(50*offset));
        /*transform.GetChild(0).position = new Vector2(
            (transform.position.x * Mathf.Cos(Mathf.Deg2Rad * 90)) - (transform.position.y + 0.5f * Mathf.Sin(50 * offset) * Mathf.Sin(Mathf.Deg2Rad * 90)), 
            (transform.position.x * Mathf.Sin(Mathf.Deg2Rad * 90)) + (transform.position.y + 0.5f*Mathf.Sin(50*offset) * Mathf.Cos(Mathf.Deg2Rad * 90)));*/
        offset += Time.deltaTime;
    }
}
