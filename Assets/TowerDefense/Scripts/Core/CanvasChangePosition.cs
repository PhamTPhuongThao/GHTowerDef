using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasChangePosition : MonoBehaviour
{
    Canvas canvas;

    private static CanvasChangePosition m_Instance;

    private static readonly object m_Lock = new object();

    public static CanvasChangePosition Instance
    {
        get
        {
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = (CanvasChangePosition)FindObjectOfType(typeof(CanvasChangePosition));

                    if (FindObjectsOfType(typeof(CanvasChangePosition)).Length > 1)
                    {
                        return m_Instance;
                    }

                    if (m_Instance == null)
                    {
                        GameObject singleton = new GameObject();
                        m_Instance = singleton.AddComponent<CanvasChangePosition>();
                        singleton.name = string.Format("[------Singleton: {0}-----] ", typeof(CanvasChangePosition).Name);
                        DontDestroyOnLoad(singleton);
                    }
                }

                return m_Instance;
            }
        }
    }

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }
    public Vector3 WorldToCanvasPosition(Vector3 worldPosition, Camera camera = null)
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
        var viewportPosition = camera.WorldToViewportPoint(worldPosition);
        return this.ViewportToCanvasPosition(viewportPosition);
    }

    public Vector3 ScreenToCanvasPosition(Vector3 screenPosition)
    {
        var viewportPosition = new Vector3(screenPosition.x / Screen.width,
                                           screenPosition.y / Screen.height,
                                           0);
        return this.ViewportToCanvasPosition(viewportPosition);
    }

    public Vector3 ViewportToCanvasPosition(Vector3 viewportPosition)
    {
        var centerBasedViewPortPosition = viewportPosition - new Vector3(0.5f, 0.5f, 0);
        var canvasRect = canvas.GetComponent<RectTransform>();
        var scale = canvasRect.sizeDelta;
        return Vector3.Scale(centerBasedViewPortPosition, scale);
    }
}
