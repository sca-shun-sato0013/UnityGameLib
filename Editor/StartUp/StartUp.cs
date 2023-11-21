using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class Startup
{
    private class StartUpData : ScriptableSingleton<StartUpData>
    {
        [SerializeField]
        private int _callCount;
        public bool IsStartUp()
        {
            return _callCount++ == 0;
        }
    }

    static Startup()
    {
        if (!StartUpData.instance.IsStartUp())
            return;

        // UnityEditorの起動時に行いたい処理を記述する

        UnityEditor.PackageManager.Client.Add("https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask");
        
    }
}