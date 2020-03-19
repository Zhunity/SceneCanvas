using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[RequireComponent(typeof(Canvas))]
public class SceneCanvas : MonoBehaviour
{
    Canvas canvas;
    private void Awake()
    {
        SceneView.onSceneGUIDelegate += OnSceneGUI;
        canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = SceneView.lastActiveSceneView.camera;
    }

    private void OnSceneGUI(SceneView view)
    {
        canvas.worldCamera = null;
        canvas.worldCamera = view.camera;
    }
}
