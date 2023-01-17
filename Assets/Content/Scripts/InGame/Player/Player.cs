using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject animationObj;
    [SerializeField] private CameraController cameraCont;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController charCont;
    private Animator anim;
    private GameObject nearObject; // ���� ���� �� ���� ��ȣ�ۿ� ���� ���� �ۼ�
    private Vector3 moveDir;

    private float moveSpeed = 8f;
    private float jumpPower = 3f;
    private bool interationDown;

    public string currentMapName; // portal ��ũ��Ʈ�� �ִ� mapNmae ������ ���� ����
    public string currentScene;
    public float playerDamage;
    public bool isDie = false;

    public Vector3 sceneChangeVec;

    public Vector3 mainFeidPos;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        charCont = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        sceneChangeVec = Vector3.zero;

        if (Camera.main == null)
        {
            mainFeidPos = transform.position;
            CreateMainCamera();
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Update()
    {
        if (Camera.main == null)
        {
            CreateMainCamera();
            return;
        }
            
        if(sceneChangeVec != Vector3.zero && 
            SceneManager.GetActiveScene().name.Equals("InGame"))
        {
            if(transform.position == mainFeidPos)
                sceneChangeVec = Vector3.zero;
            transform.position = mainFeidPos;
            return;
        }

        playerDamage = Random.Range(5, 20);
        playerDamage = 100;
        PlayerMove();
        PlayerKeyEvent();
        Interation();
        SceneUpdate();
    }

    void CreateMainCamera()
    {
        Instantiate(cameraCont)
            .GetComponent<CameraController>()
            .SetTarget(transform.GetChild(1).transform);
    }

    #region ĳ���� ����

    /// <summary>
    /// �÷��̾� �̵� 
    /// </summary>
    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (!charCont.isGrounded)
        {
            moveDir.y += gravity * Time.deltaTime;
        }
        else
        {
            moveSpeed = z > 0 ? 8.0f : 2.0f;
            moveDir = new Vector3(x, 0, z);
            moveDir = transform.TransformDirection(moveDir);

            // ���콺 ���¹������� ȭ�� ��ȯ
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }
        
        charCont.Move(moveDir * moveSpeed * Time.deltaTime);
        
        OnMovement(x, z);
    }

    /// <summary>
    /// Ű �̺�Ʈ �ߵ�
    /// </summary>
    void PlayerKeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAnimation("Jump");
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlayerAnimation("normalAttack");
        }

        if (Input.GetMouseButtonDown(1))
        {
            PlayerAnimation("skill_1_Attack");
        }
        interationDown = Input.GetButtonDown("Interation");
    }


    /// <summary>
    /// �÷��̾� �ִϸ��̼�
    /// </summary>
    /// <param name="animationName"></param>
    void PlayerAnimation(string animationName)
    {
        switch (animationName)
        {
            case Contents.playerJump:
                Animation("onJump");
                if (charCont.isGrounded == true)
                {
                    moveDir.y = jumpPower;
                }
                break;
            case Contents.playerNormal_Attack:
                Animation("onAttack");
                break;
            case Contents.playerSkill_1_Attack:
                Animation("onWeaponAttack");
                break;
        }
    }

    /// <summary>
    /// ĳ���� ���� ó��
    /// </summary>
    public void PlayerDIe()
    {
        if (GameManager.Instance.statusMgr.currentHp <= 0)
        {
            isDie = true;
            Animation("onDie");
        }
    }
    #endregion

    #region �ִϸ��̼� ���ú�

    public void OnMovement(float horizontal, float vertical)
    {
        anim.SetFloat("horizontal", horizontal);
        anim.SetFloat("vertical", vertical);
    }

    void Animation(string animName)
    {
        anim.SetTrigger(animName);
    }

    public void OnAttackCollision()
    {
        if (animationObj != null)
            animationObj.SetActive(true);
    }
    #endregion

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("item"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            nearObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            Shop shop = nearObject.GetComponent<Shop>();
            shop.Exit();
            nearObject = null;
        }
    }
    void Interation()
    {
        if (interationDown == true && nearObject != null)
        {
            if (nearObject.CompareTag("Shop"))
            {
                Shop shop = nearObject.GetComponent<Shop>();
                shop.Enter(this);
            }
        }
    }
    
    // player�� ���� �ִ� scene �˱� ����
    public void SceneUpdate()
    {
        if (SceneManager.GetActiveScene().name != currentScene)
        {
            currentScene = SceneManager.GetActiveScene().name;
        }
    }

    public Vector3 SetPosChange
    {
        set
        {
            Debug.Log(value);
            sceneChangeVec = value;
        }
    }
}
