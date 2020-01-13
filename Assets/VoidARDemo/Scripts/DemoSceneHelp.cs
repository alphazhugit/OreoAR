using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DemoSceneHelp : MonoBehaviour {
    public string helpContent;
    private Transform bgObj;
    private int scanMode = 0;
    public bool enableScanEffect = false;


    void Start()
	{
        //StartCoroutine ("CallPluginAtEndOfFrames");
        bgObj = Camera.main.transform.GetChild(0);
        bgObj.GetComponent<Renderer>().material.SetInt("_ScanMode", scanMode);
    }

    public void OnBackHandler() {
        Application.LoadLevel("AllDemo");
    }

    void OnGUI()
    {
        /*var btnHeight = Screen.height * 0.1f;
        var btnWidth = btnHeight * 4f;
        GUI.skin.button.fontSize = (int)(btnHeight * 0.4f);
        if (GUI.Button(new Rect(Screen.width - btnWidth, Screen.height - btnHeight, btnWidth, btnHeight), "返回菜单"))
        {
            Application.LoadLevel("AllDemo");
        }

        if (enableScanEffect && GUI.Button(new Rect(10, 10, btnWidth, btnHeight), "ScanMode:"+ scanMode))
        {
            scanMode++;
            if (scanMode > 2) {
                scanMode = 0;
            }
            bgObj.GetComponent<Renderer>().material.SetInt("_ScanMode", scanMode);
        }

        GUI.skin.label.fontSize = 24;
        GUI.color = Color.black;
        helpContent = helpContent.Replace("|", "\n");
        GUI.Label(new Rect(5, 100, Screen.width * 0.5f, Screen.height * 0.5f), helpContent);*/
    }
}
