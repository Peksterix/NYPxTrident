using UnityEngine;
using UnityEditor;

public class EditorWindowExpansion : EditorWindow
{
    
    [MenuItem("Editor/Sample")]
    private static void Create()
    {
        // 生成
        GetWindow<EditorWindowExpansion>("サンプル");
    }
}