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
        // 绑定场景GUI渲染回调
        // 2019之前用SceneView.onSceneGUIDelegate
        SceneView.duringSceneGui += OnSceneGUI;

        // 修改Canvas的渲染模式
        canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = SceneView.lastActiveSceneView.camera;
    }

    private void OnDestroy()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView view)
    {
        // 不知道为什么，需要每次都置空一下，才会成功修改Canvas的位置
        canvas.worldCamera = null;
        canvas.worldCamera = view.camera;
    }
}
