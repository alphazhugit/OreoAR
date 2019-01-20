using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLoadHelp : MonoBehaviour {

    private string markerName = "";

    public void AddTarget()
    {
        //添加Marker1(识别图路径默认为StreamingAssets)
        var imageTarget1 = new GameObject("ImageTarget1");
        imageTarget1.transform.localPosition = Vector3.zero;
        imageTarget1.transform.localEulerAngles = Vector3.zero;
        imageTarget1.transform.localScale = Vector3.one;
        ImageTargetBehaviour marker1 = imageTarget1.AddComponent<ImageTargetBehaviour>();


        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            marker1.storageType = VoidAR.StorageType.Assets;

            markerName = "1yuan.jpg";
        }
        else
        {
            //PC只支持绝对路径
            marker1.storageType = VoidAR.StorageType.Absolute;
            markerName = Application.streamingAssetsPath + "/1yuan.jpg";
        }

        marker1.SetPath(markerName);






        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Material mat = (Material)Resources.Load("dynamicMaterial");
        cube.GetComponent<Renderer>().material = mat;
        cube.transform.localPosition = new Vector3(0.0f, 0.25f, 0.0f);
        cube.transform.localEulerAngles = Vector3.zero;
        cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        cube.transform.SetParent(imageTarget1.transform);

        VoidAR.GetInstance().addTargetNew(marker1, imageTarget1);

        //识别图支持绝对路径方式，比如Application.persistentDataPath)
        /*string markerPath = Path.Combine(Application.persistentDataPath, "test.jpg");
        marker2.storageType = VoidAR.StorageType.Absolute;*/

        //完成添加
        VoidAR.GetInstance().FinishAddImageTarget();
    }

    public void OnFind(string metadata, bool isFind)
    {
        Debug.Log("LocalFind = " + metadata);
    }

    void OnGUI()
    {
        var btnHeight = Screen.height * 0.1f;
        var btnWidth = btnHeight * 3f;
        var gap = 20;
        if (GUI.Button(new Rect(Screen.width - btnWidth, gap, btnWidth, btnHeight), "Add Target"))
        {
            //Debug.Log(" add marker");
            AddTarget();
        }

        if (GUI.Button(new Rect(Screen.width - btnWidth, gap * 2 + btnHeight, btnWidth, btnHeight), "Clear Target"))
        {
            if (VoidAR.GetInstance().isMarkerExist(markerName))
            {
                VoidAR.GetInstance().removeTarget(markerName);
            }

        }
    }

    public void RemoveTarget()
    {
        if (VoidAR.GetInstance().isMarkerExist(markerName))
        {
            VoidAR.GetInstance().removeTarget(markerName);
        }
    }
}
