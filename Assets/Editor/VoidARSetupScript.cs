using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class VoidARSetupScript {
	static VoidARSetupScript()
	{
        var demoSceneFolder = new DirectoryInfo(Application.dataPath + "/VoidARDemo");
        if (EditorBuildSettings.scenes.Length == 0 && demoSceneFolder.Exists)
        {
            EditorBuildSettings.scenes = new EditorBuildSettingsScene[]
            {
              new EditorBuildSettingsScene("Assets/VoidARDemo/Scenes/SplashVideo.unity", true),
              new EditorBuildSettingsScene("Assets/VoidARDemo/Scenes/AllDemo.unity", true),
              new EditorBuildSettingsScene("Assets/VoidARDemo/Scenes/ImageDemo.unity", true),
              new EditorBuildSettingsScene("Assets/VoidARDemo/Scenes/VideoDemo.unity", true),
              new EditorBuildSettingsScene("Assets/VoidARDemo/Scenes/CloudDemo.unity", true),
              new EditorBuildSettingsScene("Assets/VoidARDemo/Scenes/DynamicLoadDemo.unity", true),
              new EditorBuildSettingsScene("Assets/VoidARDemo/Scenes/RECDemo.unity", true),
              new EditorBuildSettingsScene("Assets/VoidARDemo/Scenes/MarkerlessDemo.unity", true),
			  new EditorBuildSettingsScene("Assets/VoidARDemo/Scenes/ImageExtension.unity", true)
            };
        }

        GraphicsDeviceType[] apis_ios = { GraphicsDeviceType.OpenGLES2, GraphicsDeviceType.Metal };
		PlayerSettings.SetGraphicsAPIs ( BuildTarget.iOS, apis_ios );
		PlayerSettings.SetUseDefaultGraphicsAPIs (BuildTarget.iOS, false);

		GraphicsDeviceType[] apis_android = { GraphicsDeviceType.OpenGLES2 };
		PlayerSettings.SetGraphicsAPIs ( BuildTarget.Android, apis_android );
		PlayerSettings.SetUseDefaultGraphicsAPIs (BuildTarget.Android, false);
#if UNITY_2017_2_OR_NEWER
        PlayerSettings.SetMobileMTRendering(BuildTargetGroup.Android, false);
#endif
    }
}

