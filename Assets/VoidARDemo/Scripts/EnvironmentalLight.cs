using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalLight : MonoBehaviour {
    float intensity = 1.0f;
    public float baseIntensityLight = 0.5f;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        VoidAR.GetInstance().helper.GetEnvironmentalLight(ref intensity);

        transform.GetComponent<Light>().intensity = (intensity + baseIntensityLight);

        

    }
}
