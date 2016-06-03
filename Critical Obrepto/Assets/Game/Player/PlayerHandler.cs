using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour
{
    public float moveSpeed = 4;
    public int ammo = 30;
    [SerializeField]
    GameObject pfBulletMark = null;
    CharacterController characterController;
    Transform cameraTr;
    Transform HeadTr;
    Animator animator;
    Transform waistTr;
    Vector3 overrideRotation;
    float cameraRotationX;
    Vector3 velocity;
    bool jumping = false;
    const float jumpPower = 6;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraTr = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();
        HeadTr = animator.GetBoneTransform(HumanBodyBones.Head);
        waistTr = animator.GetBoneTransform(HumanBodyBones.Chest);
        overrideRotation = waistTr.localEulerAngles;
    }

    void Start()
    {
        InputHandler.instance.onMove = v =>
        {
            v = Quaternion.Euler(0, 0, -transform.eulerAngles.y) * v;
            velocity.x += v.x / 2;
            velocity.z += v.y / 2;

            Vector3 xz = velocity;
            xz.y = 0;
            xz = xz.magnitude > moveSpeed ? xz.normalized * moveSpeed : xz;
            velocity.x = xz.x;
            velocity.z = xz.z;

            animator.SetBool("Run", true);
        };
        InputHandler.instance.onStop = () =>
        {
            velocity.x /= 1.5f;
            velocity.z /= 1.5f;

            animator.SetBool("Run", false);
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
            if (jumping == false && characterController.isGrounded)
            {
                velocity.y = jumpPower;
                jumping = true;

                animator.SetBool("Jump", true);
            }
        };
        InputHandler.instance.onAimMovePC = v =>
        {
            if (90 < waistTr.localEulerAngles.z + v.y && waistTr.localEulerAngles.z + v.y < 270)
                v.y = 0;
            overrideRotation += new Vector3(0, 0, v.y);
            cameraRotationX += v.y;
            transform.localEulerAngles += new Vector3(0, v.x, 0);
        };

        InputHandler.instance.onAimMoveMobile = v =>
        {
            if (90 < waistTr.localEulerAngles.z + v.x && waistTr.localEulerAngles.z + v.x < 270)
                v.x = 0;
            overrideRotation += new Vector3(0, 0, v.x);
            cameraRotationX += v.x;
            transform.localEulerAngles += new Vector3(0, v.y, 0);
        };

        InputHandler.instance.onShootEnter = () =>
        {
            if (ammo > 0)
            {
                animator.SetBool("Shoot", true);
                InvokeRepeating("Shoot", 0, 0.1f);
            }
        };
        InputHandler.instance.onShootExit = () =>
        {
            animator.SetBool("Shoot", false);
            CancelInvoke("Shoot");
        };
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Default", "Enemy")))
        {
            Ray headToHitRay = new Ray(HeadTr.position, (hit.point - HeadTr.position).normalized);
            if (Physics.Raycast(headToHitRay, out hit, float.MaxValue))
            {
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<ZombieHandler>().ApplyDamage();
                }
                else
                {
                    var bulletMark = Instantiate(pfBulletMark);
                    bulletMark.transform.position = hit.point + hit.normal * 0.01f;
                    bulletMark.transform.LookAt(bulletMark.transform.position + hit.normal);
                }
            }
        }
        if (--ammo <= 0)
            InputHandler.instance.onShootExit();
    }

    void Update()
    {
        if (characterController.isGrounded && jumping && velocity.y != jumpPower)
        {
            jumping = false;
            animator.SetBool("Jump", false);
        }

        velocity += Physics.gravity * Time.deltaTime;

        if (characterController.isGrounded&& jumping == false)
            velocity.y = 0.0f;
    }

    void LateUpdate()
    {
        characterController.Move(velocity * Time.deltaTime);
        waistTr.localEulerAngles = overrideRotation;

        Vector3 focus = new Vector3(0, 2.5f, 0) + transform.position;
        Vector3 camPos = transform.position + transform.up * 2.5f - transform.forward * 3 - focus;
        camPos = Quaternion.AngleAxis(-cameraRotationX, transform.right) * camPos;
        camPos += focus;

        Ray ray = new Ray();
        ray.origin = focus;
        ray.direction = camPos - focus;
        ray.direction.Normalize();

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, (camPos - focus).magnitude))
            camPos = hit.point + hit.normal * 0.3f;
        cameraTr.position = camPos;
        cameraTr.LookAt(focus);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;
    }
}
