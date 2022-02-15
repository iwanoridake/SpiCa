using UnityEngine;
using System.Collections;

public class MoveByMouse : MonoBehaviour 
{
    void Update() 
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10f;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
}