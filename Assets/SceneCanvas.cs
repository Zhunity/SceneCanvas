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
        CanvasRefresh(view);

        SceneGUI(view);
        DoEvent();
    }

    /// <summary>
    /// 刷新Canvas
    /// </summary>
    /// <param name="view"></param>
    private void CanvasRefresh(SceneView view)
    {
        // 不知道为什么，需要每次都置空一下，才会成功修改Canvas的位置
        canvas.worldCamera = null;
        canvas.worldCamera = view.camera;
    }

    private void SceneGUI(SceneView view)
    {
        Handles.BeginGUI();
        GUILayout.BeginArea(view.position); // 规定显示区域为屏幕大小

        if(GUILayout.Button("Hello World"))
        {
            Debug.Log("Hello world");
        }

        GUILayout.EndArea();
        Handles.EndGUI();
    }

    /// <summary>
    /// 处理事件
    /// </summary>
    private void DoEvent()
    {
        Event e = Event.current;
        ///
        /// 这里中间一系列操作
        ///

        ShieldSecneEvent(e);
    }

    private void ShieldSecneEvent(Event e)
    {
        // 屏蔽鼠标滚轮、右键、中间键
        if(e.isScrollWheel || e.IsRightMouseButton() || e.IsMiddleMouseButton())
        {
            e.Use();
        }

        if (e.isKey)
        {
            // 屏蔽箭头操作
            if(e.keyCode == KeyCode.DownArrow || e.keyCode == KeyCode.LeftArrow || e.keyCode == KeyCode.RightArrow || e.keyCode == KeyCode.UpArrow)
            {
                e.Use();
            }
        }

        // 屏蔽左键操作导致的重绘
        if(e.type == EventType.Layout)
        {
            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            HandleUtility.AddDefaultControl(controlID);
        }
    }
}
