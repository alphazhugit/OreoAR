using UnityEngine;
using System.Collections;
using System;

public class RecordHelp : MonoBehaviour {
    private VideoRecordBehaviour vrb;
    // Use this for initialization
    void Start () {
        vrb = Camera.main.GetComponent<VideoRecordBehaviour>();
    }
	

    protected virtual void OnGUI()
   {
       
       var btnHeight = Screen.height * 0.1f;
       GUI.skin.button.fontSize = (int)(btnHeight * 0.4f);
       if (GUI.Button(new Rect(5, Screen.height - btnHeight - 10, btnHeight * 1.2f, btnHeight), vrb.isRecording ? "Stop" : "REC"))
       {
            if (!vrb.isRecording)
            {
                vrb.StartRecording();
                
            }
            else
            {
                vrb.StopRecording();
               
            }
        }
       if (vrb.isRecording)
       {
           //TimeSpan nowTime = new TimeSpan(DateTime.Now.Ticks);
           TimeSpan diffTime = vrb.ElapsedTime;
           GUI.skin.label.fontSize = 32;
           GUI.color = Color.red;
           GUI.Label(new Rect(10, 2, 100, 60), (diffTime.Seconds % 2 == 0 ? "●" : "  ") + " REC");
           GUI.color = Color.black;
           var timeStr = (diffTime.Minutes < 10 ? "0" : "") + diffTime.Minutes + ":" + (diffTime.Seconds < 10 ? "0" : "") + diffTime.Seconds;
           GUI.Label(new Rect(10, 32, 100, 60), timeStr);
       }
   }
}
