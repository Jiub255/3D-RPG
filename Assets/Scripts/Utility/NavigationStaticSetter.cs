using UnityEditor;
using UnityEngine;

public class NavigationStaticSetter : EditorWindow
{
	public Transform _parentOfStaticObjects;

    [MenuItem("Window/Navigation Static Setter")]
    public static void ShowWindow()
    {
        GetWindow<NavigationStaticSetter>("Navigation Static Setter");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Set All Navigation Static"))
        {
            _parentOfStaticObjects = GameObject.Find("Navigation Static Objects").transform;
            Debug.Log($"Parent of static objects name: {_parentOfStaticObjects.name}");
            if (_parentOfStaticObjects != null)
            {
                SetAllStatic(_parentOfStaticObjects);
            }
        }
    }
	public void SetAllStatic(Transform parent)
    {
		foreach (Transform child in parent)
        {

            if(child.GetComponent<MeshRenderer>() != null)
            {
                // Set object to navigation static. 
                GameObjectUtility.SetStaticEditorFlags(child.gameObject, StaticEditorFlags.NavigationStatic);
            }

            // Recursively search all children, grandchildren, etc. 
            if(child.childCount > 0)
            {
                SetAllStatic(child);
            }
        }
    }
}

/*
public class EditModeFunctions : EditorWindow
{
    [MenuItem("Window/Edit Mode Functions")]
    public static void ShowWindow()
    {
        GetWindow<EditModeFunctions>("Edit Mode Functions");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Run Function"))
        {
            FunctionToRun();
        }
    }

    private void FunctionToRun()
    {
        Debug.Log("The function ran.");
    }
}
*/