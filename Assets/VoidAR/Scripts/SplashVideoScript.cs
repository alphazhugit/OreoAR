using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashVideoScript : MonoBehaviour {
	void Awake () {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<VoidVideoPlayer>().AddEventListener(VoidAREvent.END, OnEnd);
        GetComponent<VoidVideoPlayer>().AddEventListener(VoidAREvent.READY, OnReady);
    }

    void OnEnd(VoidAREvent evt) {
        SceneManager.LoadScene(1);
    }

    void OnReady(VoidAREvent evt) {
        GetComponent<MeshRenderer>().enabled = true;
    }

    void Update() {
        float ratio = (float)Screen.width / Screen.height;
        if (ratio < 1)
        {
            Camera.main.orthographicSize = 2.6f;
        }
        else {
            Camera.main.orthographicSize = 2.6f / ratio;
        }
    }
}