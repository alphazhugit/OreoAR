using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;


[CustomEditor(typeof(VoidARBehaviour))]
public class VoidARBehaviourEditor : Editor
{

#if UNITY_IPHONE
    const string dllName = "__Internal";
#elif UNITY_ANDROID
    const string dllName = "VoidAR-Plugin";
#else
    const string dllName = "VoidAR-Plugin";
#endif
    [DllImport(dllName)]
    private static extern void _getCameraList(byte[] names);

    // Use this for initialization
	VoidARBehaviourEditor() {
    }

    public override void OnInspectorGUI()
    {
        VoidARBehaviour obj = target as VoidARBehaviour;

        string[] markerTypes = {"Image" , "Markerless", "ImageExtension"};
		string[] TrackNum = { "1", "2" , "3", "4", "5" };
        obj.markerType = (VoidARBehaviour.EMarkerType)EditorGUILayout.Popup("MarkerType", (int)(obj.markerType), markerTypes );

        //Debug.Log("obj.markerType  " + obj.markerType);

        if (obj.markerType == VoidARBehaviour.EMarkerType.Image) {

			obj.simultaneousTrackingMax = EditorGUILayout.Popup ("Simultaneous Tracking", obj.simultaneousTrackingMax - 1, TrackNum) + 1;
			obj.tracking = false;
			obj.shapeMatchAccuracy = 5;
			obj.sceneScale = 1;

		    obj.is_use_cloud = EditorGUILayout.Toggle ("Use Cloud", obj.is_use_cloud);
            obj.isMirror     = EditorGUILayout.Toggle("Is Mirror", obj.isMirror);

           

             if (obj.is_use_cloud)
             {
                 obj.accessKey = EditorGUILayout.TextField("Access Key", obj.accessKey);
                 obj.secretKey = EditorGUILayout.TextField("Secret Key", obj.secretKey);
                 obj.simultaneousTrackingMax = 1;
                 obj.privateCloud = EditorGUILayout.Toggle("Private Cloud", obj.privateCloud);
                 if (obj.privateCloud)
                 {
                     obj.cloudURL = EditorGUILayout.TextField("Cloud URL", obj.cloudURL);
                 }
             }


		} else if (obj.markerType == VoidARBehaviour.EMarkerType.Markerless) {
			obj.simultaneousTrackingMax = 1;
            EditorStyles.textField.wordWrap = true;
            obj.appKey = EditorGUILayout.TextField("App License Key", obj.appKey, new GUILayoutOption[] { GUILayout.MinHeight(30f), GUILayout.MaxHeight(50f) });
            obj.markerlessParent = EditorGUILayout.ObjectField ("MarkerlessNode", obj.markerlessParent, typeof(GameObject), true) as GameObject;
			obj.sceneScale = EditorGUILayout.FloatField ("SceneScale", obj.sceneScale);
		}
        else if (obj.markerType == VoidARBehaviour.EMarkerType.ImageExtension)
        {
            obj.simultaneousTrackingMax = 1;
            obj.tracking = false;
			obj.shapeMatchAccuracy = 1;
			obj.is_use_cloud = false;//EditorGUILayout.Toggle ("Use Cloud", obj.is_use_cloud);
            obj.sceneScale = 1.0f;

            EditorStyles.textField.wordWrap = true;
            obj.appKey = EditorGUILayout.TextField("App License Key", obj.appKey, new GUILayoutOption[] { GUILayout.MinHeight(30f), GUILayout.MaxHeight(50f) });

        }

#if UNITY_IPHONE || UNITY_ANDROID
        string[] deviceNames = new string[2];
        deviceNames[0] = "后置摄像头";
        if (obj.markerType == VoidARBehaviour.EMarkerType.Image)
        {
            deviceNames[1] = "前置摄像头";
        }
        obj.CameraIndex = EditorGUILayout.Popup("Camera", obj.CameraIndex, deviceNames);
        EditorUtility.SetDirty(target);
#else
        byte[] names = new byte[1024];
        _getCameraList(names);
        string result = System.Text.Encoding.UTF8.GetString(names);
        char[] delimiterChars = { ',' };
        string[] deviceNames = result.Split(delimiterChars);
        obj.CameraIndex = EditorGUILayout.Popup("Camera Device", obj.CameraIndex, deviceNames);

#endif

        //EditorUtility.SetDirty(target);
        Undo.RecordObject(target,"change property");
    }

}
