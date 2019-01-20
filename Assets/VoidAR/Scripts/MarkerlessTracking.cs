using UnityEngine;
public class MarkerlessTracking : MonoBehaviour,ITricking
{
    private int lastState = -1;
    private bool isActive = false;
	private int timeDelay = 0;
    /// <summary>
    /// 跟踪反馈（每帧）
    /// </summary>
    /// <param name="stateCode"></param>
    public void UpdateTracking(int stateCode)
    {
        if (stateCode == 1099)
        {
            Debug.LogError("server error");
        }
        else if (stateCode == 501)
        {
            Debug.LogError("key error");
        }
        else if (stateCode == 101)
        {
            Debug.LogError("use time limit error");
        }
        lastState = stateCode;

		if (lastState == 2) {
			timeDelay = 100;
		}
    }

    public int GetTrackingState() {
        return lastState;
    }

    /// <summary>
    /// 设置跟踪活动状态
    /// </summary>
    /// <param name="value"></param>
    public void SetActive(bool value) {
        isActive = value;
    }

    public bool GetActive() {
        return isActive;
    }
}