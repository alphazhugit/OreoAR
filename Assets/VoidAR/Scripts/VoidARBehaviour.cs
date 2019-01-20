using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class VoidARBehaviour : VoidARBase
{
    protected override void Awake()
    {
        base.Awake();
#if UNITY_STANDALONE_WIN
        String currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
        String dllPath = Application.dataPath + Path.DirectorySeparatorChar + "Plugins";
        if (currentPath.Contains(dllPath) == false)
        {
            Environment.SetEnvironmentVariable("PATH", currentPath + Path.PathSeparator + dllPath, EnvironmentVariableTarget.Process);
        }
#endif
    }
}