using UnityEngine;
using UnityEditor;

//()�̒��ɃN���X�������
[CustomEditor(typeof(AddComponent))]

public class EditorExpansion : Editor
{
    GameObject obj; 
    AddComponent addCom;
    //OnInspectorGUI�ŃJ�X�^�}�C�Y��GUI�ɕύX����
    public override void OnInspectorGUI()
    {

        //����Inspector������\������
        base.OnInspectorGUI();
        obj = GameObject.Find("AddCom");
        addCom = obj.GetComponent<AddComponent>();
        //����Inspector�����̉��Ƀ{�^����\��
        if (GUILayout.Button("add component"))
        {
            addCom.AddCom();
        }
    }
}
