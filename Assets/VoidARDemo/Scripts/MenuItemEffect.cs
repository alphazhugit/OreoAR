using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuItemEffect : MonoBehaviour {
    private RectTransform mTransform;
    private Image mBgImage;
    private Image mItemImage;
    Color color = new Color(1, 1, 1, 0);
    Vector2 pos = new Vector2(-1080, 0);
    public float delay = 0;
    void Start () {
        mTransform = transform as RectTransform;
        mTransform.anchoredPosition = pos;
        mBgImage = GetComponent<Image>();
        mItemImage = transform.GetChild(0).GetComponent<Image>();
        mBgImage.color = color;
        mItemImage.color = color;
        StartCoroutine(EffectCoroutine(0.5f));
    }

    IEnumerator EffectCoroutine(float duration)
    {
        if (delay > 0) {
            yield return new WaitForSeconds(delay);
        }
        float start = Time.time;
        float val = 0;
        while (val < 1.0f)
        {
            val = Mathf.Clamp01((Time.time - start) / duration);  //0-1
            color.a = val;
            mBgImage.color = color;
            mItemImage.color = color;
            pos.x = -1080 * (1.0f - val);
            mTransform.anchoredPosition = pos;
            yield return new WaitForEndOfFrame();
        }
    }
}
