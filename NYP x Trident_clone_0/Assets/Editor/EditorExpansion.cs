using UnityEngine;
using UnityEditor;

//()の中にクラス名を入力
[CustomEditor(typeof(AddComponent))]

public class EditorExpansion : Editor
{
    GameObject obj; 
    AddComponent addCom;
    //OnInspectorGUIでカスタマイズのGUIに変更する
    public override void OnInspectorGUI()
    {

        //元のInspector部分を表示する
        base.OnInspectorGUI();
        obj = GameObject.Find("AddCom");
        addCom = obj.GetComponent<AddComponent>();
        //元のInspector部分の下にボタンを表示
        if (GUILayout.Button("add component"))
        {
            addCom.AddCom();
        }
    }
}
