using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Diagnostics;

public class EditorRestartUnity
{
    [MenuItem("File/Restart")]
    static void RestartUnity()
    {
        // �ʂ�Unity���N���������ƂɎ��g���I��
        Process.Start(EditorApplication.applicationPath);
        EditorApplication.Exit(0);
    }
}
