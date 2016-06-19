using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIType
{
    PC,
    Mobile,
}

public class InputHandler : MonoBehaviour
{
    static public InputHandler instance { get; private set; }

    PlayerHandler player;

    [SerializeField]
    UIType uiType = UIType.PC;
    public UIType UIType { get { return uiType; } }
    public Action<Vector2> onMove { get; set; }
    public Action onStop { get; set; }
    public Action onJump { get; set; }
    public Action<Vector2> onAimMovePC { get; set; }
    public Action<Vector2> onAimMoveMobile { get; set; }
    public Action onShootEnter { get; set; }
    public Action onShootExit { get; set; }
    Vector3 prevMousePos;

    Queue<float> XQ = new Queue<float>(8);
    float sum_x = 0;
    Queue<float> YQ = new Queue<float>(8);
    float sum_y = 0;
    static Vector3 prev;

    void Awake()
    {
        instance = this;

        for(int i=0;i<8;i++)
        {
            XQ.Enqueue(0);
            YQ.Enqueue(0);
        }

        Input.compass.enabled = true;
        player = FindObjectOfType<PlayerHandler>();
    }

    void Update()
    {
        if (player.died)
            return;

        if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");

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
                onAimMovePC(Input.mousePosition - prevMousePos);
                prevMousePos = Input.mousePosition;
                break;
            case UIType.Mobile:
                float curX = Input.acceleration.z;
                sum_x -= XQ.Dequeue();
                sum_x += curX;
                XQ.Enqueue(curX);

                float curY = Input.compass.magneticHeading;
                sum_y -= YQ.Dequeue();
                sum_y += curY;
                YQ.Enqueue(curY);

                Vector3 cur = new Vector3(sum_x * 90, sum_y, 0) / 8;
                onAimMoveMobile(cur - prev);
                prev = cur;

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