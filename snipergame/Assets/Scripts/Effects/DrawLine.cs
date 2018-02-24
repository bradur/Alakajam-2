// Date   : 24.02.2018 11:57
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour
{

    [SerializeField]
    private Color color;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform endPos;

    [SerializeField]
    private Material material;

    private bool allowDrawing = false;

    public void SetVisibility(bool visibility)
    {
        allowDrawing = visibility;
    }

    void OnPostRender()
    {
        if (allowDrawing)
        {
            GL.PushMatrix();
            material.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Color(color);
            GL.Vertex(startPos.position);
            GL.Vertex(endPos.position);
            GL.End();
            GL.PopMatrix();
        }
    }
}
