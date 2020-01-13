using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : MonoBehaviour {
    private Sprite StartImage;
    public Sprite StopImage;
    private Image image;
    private VideoRecordBehaviour vrb;
	void Awake () {
        image = GetComponent<Image>();
        StartImage = image.sprite;
        GetComponent<Button>().onClick.AddListener(OnClickHandler);
        vrb = Camera.main.GetComponent<VideoRecordBehaviour>();
    }
	
	// Update is called once per frame
	void OnClickHandler () {
        if (!vrb.isRecording)
        {
            vrb.StartRecording();
            image.sprite = StopImage;
        }
        else {
            vrb.StopRecording();
            image.sprite = StartImage;
        }
	}
}
