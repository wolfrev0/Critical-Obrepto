using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour
{
    public float moveSpeed = 4;
    CharacterController _characterController;
    Animator _animator;
    Transform _waistTr;
    Vector3 _overrideRotation;
    Vector3 _velocity;
    bool _jumping = false;
    const float _kJumpPower = 6;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _waistTr = _animator.GetBoneTransform(HumanBodyBones.Chest);
        _overrideRotation = _waistTr.localEulerAngles;
    }

    void Start()
    {
        InputHandler.instance.onMove = v =>
        {
            v = Quaternion.Euler(0, 0, -transform.eulerAngles.y) * v;
            _velocity.x += v.x / 2;
            _velocity.z += v.y / 2;

            Vector3 xz = _velocity;
            xz.y = 0;
            xz = xz.magnitude > moveSpeed ? xz.normalized * moveSpeed : xz;
            _velocity.x = xz.x;
            _velocity.z = xz.z;

            _animator.SetBool("Run", true);
        };
        InputHandler.instance.onStop = () =>
        {
            _velocity.x /= 1.5f;
            _velocity.z /= 1.5f;

            _animator.SetBool("Run", false);
        };
        InputHandler.instance.onJump = () =>
        {
            //CharacterController.isGrounded값이 계속 true/false로 튄다.
            //그 이유는 LateUpdate에서 계속 CharacterController.Move를 호출하기 때문인 것으로 사료되는데,
            //해결방법으로 2가지 정도를 생각해봤다.
            //1. 계속 true/false되므로 버튼 누르고 있으면 계속 점프명령 내려주기(점프되고 나머지 쓸모없는 명령은 _jumping으로 걸러짐)
            //2. isGrounded를 직접 구현하기(캐릭터에서 바닥에 수직으로 플레이어 다리길이만한 Raycast를 하면 땅인지 공중인지 알 수 있음)
            //2번 방법은 레이캐스트하기 귀찮기도 하고, 부하도 많이 걸리고, 부하 줄이려면 Layer작업도 해야하기 떄문에 PASS.
            //현재 1번 방법을 사용중이며, 추후에 2번으로 변경해야할 경우가 생길수도 있으니 주석 달아둠.
            if (_jumping == false && _characterController.isGrounded)
            {
                _velocity.y = _kJumpPower;
                _jumping = true;

                _animator.SetBool("Jump", true);
            }
        };
        InputHandler.instance.onAimMove = v =>
        {
            if (90 < _waistTr.localEulerAngles.z + v.y && _waistTr.localEulerAngles.z + v.y < 270)
                v.y = 0;
            _overrideRotation += new Vector3(0, 0, v.y);
            transform.localEulerAngles += new Vector3(0, v.x, 0);
        };
        InputHandler.instance.onShootEnter = () =>
        {
            _animator.SetBool("Shoot", true);
        };
        InputHandler.instance.onShootExit = () =>
        {
            _animator.SetBool("Shoot", false);
        };
    }

    void Update()
    {
        if (_characterController.isGrounded && _jumping && _velocity.y != _kJumpPower)
        {
            _jumping = false;
            _animator.SetBool("Jump", false);
        }

        _velocity += Physics.gravity * Time.deltaTime;

        if (_characterController.isGrounded&& _jumping == false)
            _velocity.y = 0.0f;
    }

    void LateUpdate()
    {
        _characterController.Move(_velocity * Time.deltaTime);
        _waistTr.localEulerAngles = _overrideRotation;
    }
}
