using UnityEngine;
using UnityEditor;

public class UtilityWindow : EditorWindow
{
    private const int dividerHeaderStart = 13;
    private const int dividerTextLength = 60;

    private GameObject editedObject;

    private string baseName;
    private string separator = "_";
    private int startingIndex;

    private bool randPosition;
    private Vector3 radius;

    private bool randRotation;
    private Vector3 startRotation = Vector3.zero;
    private Vector3 endRotation = new Vector3(360,360,360);

    private bool randScale;
    private bool separateAxis;
    private float minScale;
    private float maxScale;

    private string hierarchySeparator;

    private bool hierarchyRootApplyScale;

    private string[] maskOptions = new string[3] { "Rename", "Randomize", "Hierarchy" };
    private int bitmask;

    private Vector2 scrollPos;

    [MenuItem("zhdx/Utility Window")]
    private static void Init()
    {
        UtilityWindow window = (UtilityWindow)GetWindow(typeof(UtilityWindow), true, "Utility", true);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        editedObject = (GameObject)EditorGUILayout.ObjectField("Edited Object", editedObject, typeof(GameObject), true);
        if(GUILayout.Button("+", GUILayout.Width(20)))
        {
            if(Selection.activeTransform != null)
            {
                editedObject = Selection.activeTransform.gameObject;
            }
            else
            {
                editedObject = null;
            }
        }
        GUILayout.EndHorizontal();

        bitmask = EditorGUILayout.MaskField("Select Toolkits", bitmask, maskOptions);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        if ((bitmask & 1) == 1)
        {
            DrawRenameGroup();
            GUILayout.Space(ZEditorStyles.LINE_HEIGHT);
        }
        if((bitmask & 2) >> 1 == 1)
        {
            DrawRandomizeGroup();
            GUILayout.Space(ZEditorStyles.LINE_HEIGHT);
        }
        if ((bitmask & 4) >> 2 == 1)
        {
            DrawHierarchyGroup();
            GUILayout.Space(ZEditorStyles.LINE_HEIGHT);
        }
        EditorGUILayout.EndScrollView();
    }

    private void DrawRenameGroup()
    {
        Rect body = EditorGUILayout.BeginVertical();
        EditorGUI.DrawRect(body, ZEditorStyles.ACTIVE_BOX_COLOR);

        Rect header = EditorGUILayout.BeginHorizontal();
        EditorGUI.DrawRect(header, Color.black);
        GUILayout.Label("RENAME", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        baseName = EditorGUILayout.TextField("Base Name", baseName);
        separator = EditorGUILayout.TextField("Separator", separator);
        startingIndex = EditorGUILayout.IntField("Starting Index", startingIndex);

        GUILayout.Space(ZEditorStyles.LINE_HEIGHT);

        EditorGUI.BeginDisabledGroup(editedObject == null);
        if(GUILayout.Button("RENAME"))
        {
            Rename();   
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.EndVertical();
    }

    private void Rename()
    {
        for (int i = 0; i < editedObject.transform.childCount; ++i)
        {
            var child = editedObject.transform.GetChild(i);
            child.name = baseName + separator + (startingIndex + i);
            EditorUtility.SetDirty(child.gameObject);
        }
    }

    private void DrawRandomizeGroup()
    {
        Rect body = EditorGUILayout.BeginVertical();
        EditorGUI.DrawRect(body, ZEditorStyles.ACTIVE_BOX_COLOR);

        Rect header = EditorGUILayout.BeginHorizontal();
        EditorGUI.DrawRect(header, Color.black);
        GUILayout.Label("RANDOMIZE", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        randPosition = EditorGUILayout.BeginToggleGroup("Randomize Position", randPosition);
        radius = EditorGUILayout.Vector3Field("Radius", radius);
        EditorGUILayout.EndToggleGroup();

        GUILayout.Space(ZEditorStyles.LINE_HEIGHT);
        
        randRotation = EditorGUILayout.BeginToggleGroup("Randomize Rotation", randRotation);
        startRotation = EditorGUILayout.Vector3Field("Start Rotation", startRotation);
        endRotation = EditorGUILayout.Vector3Field("End Rotation", endRotation);
        EditorGUILayout.EndToggleGroup();
        
        GUILayout.Space(ZEditorStyles.LINE_HEIGHT);

        randScale = EditorGUILayout.BeginToggleGroup("Randomize Scale", randScale);
        separateAxis = EditorGUILayout.Toggle("Separate Axis", separateAxis);
        minScale = EditorGUILayout.FloatField("Min", minScale);
        maxScale = EditorGUILayout.FloatField("Max", maxScale);
        EditorGUILayout.EndToggleGroup();
        
        GUILayout.Space(ZEditorStyles.LINE_HEIGHT);

        EditorGUI.BeginDisabledGroup(editedObject == null);
        if (GUILayout.Button("RANDOMIZE"))
        {
            if(randPosition)
            {
                RandomizePosition();
            }
            if(randRotation)
            {
                RandomizeRotation();
            }
            if(randScale)
            {
                RandomizeScale();
            }
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.EndVertical();
    }

    private void RandomizeRotation()
    {
        for (int i = 0; i < editedObject.transform.childCount; ++i)
        {
            var child = editedObject.transform.GetChild(i);
            child.localRotation = Quaternion.Euler(Random.Range(startRotation.x, endRotation.x), Random.Range(startRotation.y, endRotation.y), Random.Range(startRotation.z, endRotation.z));
            EditorUtility.SetDirty(child.gameObject);
        }
    }

    private void RandomizePosition()
    {
        for (int i = 0; i < editedObject.transform.childCount; ++i)
        {
            var child = editedObject.transform.GetChild(i);
            child.localPosition = new Vector3(Random.Range(-radius.x, radius.x), Random.Range(-radius.y, radius.y), Random.Range(-radius.z, radius.z));
            EditorUtility.SetDirty(child.gameObject);
        }
    }

    private void RandomizeScale()
    {
        for (int i = 0; i < editedObject.transform.childCount; ++i)
        {
            var child = editedObject.transform.GetChild(i);
            if(separateAxis)
            {
                child.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
            }
            else
            {
                var rand = Random.Range(minScale, maxScale);
                child.localScale = Vector3.one * rand;
            }
            
            EditorUtility.SetDirty(child.gameObject);
        }
    }

    private void DrawHierarchyGroup()
    {
        Rect body = EditorGUILayout.BeginVertical();
        EditorGUI.DrawRect(body, ZEditorStyles.ACTIVE_BOX_COLOR);

        Rect headerDiv = EditorGUILayout.BeginHorizontal();
        EditorGUI.DrawRect(headerDiv, Color.black);
        GUILayout.Label("HIERARCHY", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("DIV", EditorStyles.boldLabel);

        hierarchySeparator = EditorGUILayout.TextField("Name", hierarchySeparator);

        GUILayout.Space(ZEditorStyles.LINE_HEIGHT);

        if(GUILayout.Button("CREATE DIV"))
        {
            CreateHierarchyDivider();
        }

        GUILayout.Space(ZEditorStyles.LINE_HEIGHT);

        GUILayout.Label("ROOT", EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(editedObject == null);
        hierarchyRootApplyScale = EditorGUILayout.Toggle("Apply scale", hierarchyRootApplyScale);
        if (GUILayout.Button("CREATE ROOT"))
        {
            CreateHierarchyRoot(editedObject);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.EndVertical();
    }

    private void CreateHierarchyDivider()
    {
        var div = dividerTextLength - dividerHeaderStart - hierarchySeparator.Length - 1;

        GameObject go = new GameObject();
        go.name = new string('-', dividerHeaderStart) + " " + hierarchySeparator + " " + new string('-', div);
        Undo.RegisterCreatedObjectUndo(go, "(Utility) Create object");
    }

    private void CreateHierarchyRoot(GameObject target)
    {
        Undo.RegisterCompleteObjectUndo(target, "(Utility) Creating Hierarchy Root");
        
        GameObject go = new GameObject();
        go.name = "root_" + target.name;
        go.transform.parent = target.transform;
        
        go.transform.localPosition = Vector3.zero;
        if (hierarchyRootApplyScale)    go.transform.localScale = Vector3.one;
        go.transform.parent = null;

        Undo.RegisterCreatedObjectUndo(go, "(Utility) Create object");

        Undo.SetTransformParent(target.transform, go.transform, "(Utility) Setting transform");
        
        EditorUtility.SetDirty(target);
    }
}
