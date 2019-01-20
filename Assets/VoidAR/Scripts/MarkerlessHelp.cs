using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerlessHelp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnGUI()
    {
        var btnHeight = Screen.height * 0.1f;
        var btnWidth = btnHeight * 3.0f;
        var gap = 20;
        GUI.skin.button.fontSize = 36;
        if (GUI.Button(new Rect(Screen.width - btnWidth, gap, btnWidth, btnHeight), "Start"))
        {
            VoidAR.GetInstance().startMarkerlessTracking();
        }
    }
}
