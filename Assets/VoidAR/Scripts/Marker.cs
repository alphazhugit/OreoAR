using UnityEngine;
/// <summary>
/// 旧版本云识别兼容，新版本废弃，改为ImageTargetBehaviour
/// </summary>
public class Marker : ImageTargetBase
{
    void Start() {
        GameObject quad = transform.Find("MarkerQuad").gameObject;
        if (quad) {
            quad.SetActive(false);
        }
    }
}
