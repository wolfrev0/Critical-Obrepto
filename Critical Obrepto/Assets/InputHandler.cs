using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    static public InputHandler instance { get; private set; }
    public Action<Vector2> onMove { get; set; }
    public Action onStop { get; set; }
    public Action onJump { get; set; }
    public Action<Vector2> onAimMove { get; set; }
    Vector3 prevMousePos;

    void Awake()
    {
        instance = this;
        onMove = v => { };
        onStop = () => { };
    }

    void Update()
    {
        bool movedW, movedS, movedA, movedD;
        if (movedW = Input.GetKey(KeyCode.W))
            onMove(Vector2.up);
        if (movedS = Input.GetKey(KeyCode.S))
            onMove(Vector2.down);
        if (movedA = Input.GetKey(KeyCode.A))
            onMove(Vector2.left);
        if (movedD = Input.GetKey(KeyCode.D))
            onMove(Vector2.right);
        if (!(movedW || movedS || movedA || movedD))
            onStop();
        if (Input.GetKeyDown(KeyCode.Space))
            onJump();
        onAimMove(Input.mousePosition - prevMousePos);
        prevMousePos = Input.mousePosition;
    }
}