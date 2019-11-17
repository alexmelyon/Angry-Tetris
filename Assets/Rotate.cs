using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Sling sling;
    public enum Mode
    {
        RIGHT, LEFT
    }
    public Mode mode = Mode.RIGHT;
    private void OnMouseDown()
    {
        if (mode == Mode.RIGHT)
        {
            sling.RotateRight();
        }
        else if (mode == Mode.LEFT)
        {
            sling.RotateLeft();
        }
    }
}
