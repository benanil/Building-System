
using AnilTools;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string Name;
    public string debug;

    public void Add()
    {
        string text = debug;
        Messenger.AddListener(Name, () => Debug2.Log(text));
    }

    public void Invoke()
    {
        Messenger.Listen(Name);
    }

    private void OnValidate()
    {

    }

}

[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var _target = (Test)target;

        if (GUILayout.Button("Test"))
        {
            _target.Invoke();
        }

        if (GUILayout.Button("Add"))
        {
            _target.Add();
        }

    }
}