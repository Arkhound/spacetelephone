using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioControler : MonoBehaviour {
    public AudioSource source;
    float[] freqData = new float[1026];
    public GameObject g;
    public int timer = 10;
    public int i = 0;
    public int k = 0;
	// Use this for initialization
	void Start () {
        source = GameObject.Find("AudioObject").GetComponent<AudioSource>();
        g = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        source.clip.GetData(freqData, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (i == 60)
        {
            source.clip.GetData(freqData, 0);
        }
        if (i % 12 == 0)
        {
            g.transform.position = new Vector2(0, freqData[i]);
            k++;
            Debug.Log(freqData[i]);
        }
        if (i > freqData.Length)
        {
            i = 0;
        }
        i++;
	}
}
