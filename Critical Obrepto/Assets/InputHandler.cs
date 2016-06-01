using System;
using UnityEngine;

public enum UIType
{
    PC,
    Mobile,
}

public class InputHandler : MonoBehaviour
{
    static public InputHandler instance { get; private set; }

    [SerializeField]
    UIType uiType = UIType.PC;
    public UIType UIType { get { return uiType; } }
    public Action<Vector2> onMove { get; set; }
    public Action onStop { get; set; }
    public Action onJump { get; set; }
    public Action<Vector2> onAimMove { get; set; }
    public Action onShootEnter { get; set; }
    public Action onShootExit { get; set; }
    Vector3 prevMousePos;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        switch (uiType)
        {
            case UIType.PC:
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
                if (Input.GetKey(KeyCode.Space))
                    onJump();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    onShootEnter();
                if (Input.GetKeyUp(KeyCode.Mouse0))
                    onShootExit();
                onAimMove(Input.mousePosition - prevMousePos);
                prevMousePos = Input.mousePosition;
                break;
            case UIType.Mobile:



                break;
        }
    }

    public void Jump()
    {
        onJump();
    }

    public void ShootEnter()
    {
        onShootEnter();
    }

    public void ShootExit()
    {
        onShootExit();
    }
}