using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Demo : MonoBehaviour {
    public void OnMenuSelect(string sceneName) {
        Application.LoadLevel(sceneName);
    }
}
