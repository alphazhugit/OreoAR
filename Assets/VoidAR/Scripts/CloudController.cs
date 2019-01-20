using UnityEngine;
using UnityEngine.UI;
public class CloudController : CloudControllerBase {
    private int targetModel;
    void Awake()
    {
        targetModel = 0;
        //侦听云识别目标创建完成事件（开始跟踪）
        AddEventListener(VoidAREvent.COMPLETE, OnComplete);
        //AB资源下载进度
        AddEventListener(VoidAREvent.PROGRESS, OnDownload);
        //异常通知
        AddEventListener(VoidAREvent.ERROR, OnError);
    }

    void OnComplete(VoidAREvent evt) {
        //GameObject target = evt.data as GameObject;
        //Debug.Log("OnComplete target :" + target.name);
    }

    void OnDownload(VoidAREvent evt)
    {
        int progress = (int)evt.data; //下载进度值0-100
       // Debug.Log("OnDownload progress :" + progress);
        
    }

    void OnError(VoidAREvent evt) {
     //   int errorCode = (int)evt.data;
    //    Debug.LogError("cloud error :" + errorCode);
    }

    /// <summary>
    /// 云识别数据成功响应
    /// </summary>
    /// <param name="cloudData">云识别数据</param>
    protected override void OnSuccess(VoidARCloudData cloudData)
    {
        base.OnSuccess(cloudData);
        if (string.IsNullOrEmpty(cloudData.url) && !string.IsNullOrEmpty(cloudData.metadata))
        {
            targetModel = 1;
        }
        else
        {
            targetModel = 0;
        }
    }

    private GameObject videoLoadObj;
    protected override IMarker SetCloudVideoComponent(GameObject markerTarget, GameObject videoPlayTarget, string markerName, string videoURL)
    {
        var itb = markerTarget.AddComponent<ImageTargetBase>();
        //itb.AddEventListener(VoidAREvent.FIND, OnFind);
        itb.SetPath(markerName);
        var vpb = videoPlayTarget.AddComponent<VideoPlayBehaviour>();
        vpb.AddEventListener(VoidAREvent.READY, OnVideoReady);
        vpb.loop = true;
        vpb.withoutDelay = true;
        videoLoadObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        videoLoadObj.name = "videoLoading";
        videoLoadObj.transform.SetParent(markerTarget.transform);
        videoLoadObj.transform.localScale = new Vector3(0.4f, 0.0001f, 0.4f);
        videoLoadObj.transform.localEulerAngles = Vector3.zero;
        videoLoadObj.transform.localPosition = Vector3.zero;
        videoLoadObj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("AnimMaterial");
        vpb.SetPlayerSource(videoURL);
        return itb;
    }

    private void OnVideoReady(VoidAREvent evt) {
        videoLoadObj.SetActive(false);
    }
}