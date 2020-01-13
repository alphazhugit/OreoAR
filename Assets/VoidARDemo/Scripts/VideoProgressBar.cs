using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class VideoProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private Image progress;
    public Action<float> onValueChanged;
    private void Awake()
    {
        progress = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        TrySeekTo(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TrySeekTo(eventData);
    }

    private void TrySeekTo(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            progress.rectTransform, eventData.position, null, out localPoint))
        {
            float val = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
            progress.fillAmount = val;
            if (onValueChanged != null) {
                onValueChanged.Invoke(val);
            }
        }
    }
}