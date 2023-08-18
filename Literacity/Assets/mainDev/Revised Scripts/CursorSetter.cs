using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetter : MonoBehaviour
{
    public Texture2D cursorTex;
    public Vector2 cursorHotSpot;
    void Awake()
    {
        Cursor.SetCursor(cursorTex, cursorHotSpot, CursorMode.Auto);
    }
}
