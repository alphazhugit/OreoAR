using UnityEngine;

public class MarkerlessUI : MonoBehaviour {
    //private VoidARBehaviour VoidAR;
    //public GameObject ARPlane;
    private int toolSelectIndex = 1;
    private int modelIndex = 0;
    private float delay = 0.020f;
    private bool  isInitOK = false;

    //public GameObject bgObj;
    private int scanMode = 1;
    private bool isStart = false;

	private int recordSelectIndex = 1;

	private string[] fileNames;
	int selectIndex = -1;
    public void OnBackHandler()
    {
        Application.LoadLevel("AllDemo");
    }

    public void startMarkerlessTracking() {
        VoidAR.GetInstance().startMarkerlessTracking();
    }
    /*void OnGUI()
    {
       var btnHeight = Screen.height * 0.1f;
        var btnWidth = btnHeight * 3.0f;
        var gap = 20;
        GUI.skin.button.fontSize = 36;
        if (GUI.Button(new Rect(Screen.width - btnWidth, gap, btnWidth, btnHeight), "Start"))
        {
            VoidAR.GetInstance().startMarkerlessTracking();
        }
        if (GUI.Button(new Rect(Screen.width - btnWidth, Screen.height - btnHeight, btnWidth, btnHeight), "返回菜单"))
        {
            Application.LoadLevel("AllDemo");
        }

    }*/

}
