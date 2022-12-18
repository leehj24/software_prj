using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{

    // �� �̵� �ݺ��� ĳ�����ߺ����� ������ ���� ���� ����
    static public PlayerManager instance;

    public string currentMapName; // �� ��ũ��Ʈ�� �ִ� ���� �� ����


    // �޸��⸦ ���� ����
    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = true;

    public string walkSound_1;
    public string walkSound_2;
    public string walkSound_3;
    public string walkSound_4;

    private AudioManger theAudio;
    private SaveNLoad theSaveNLoad;

    private bool canMove = true;

    public bool notMove = false;
    public bool rightTargetVector;

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); //�ٸ� ������ �Ѿ �� �ı� X
            theAudio = FindObjectOfType<AudioManger>();
            theSaveNLoad = FindObjectOfType<SaveNLoad>();
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator MoveCoroutine() // Ÿ���̵��� �ε巯���� ���� ����ó�� �ڵ�
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0 && !notMove)
        {

            // Using shift to run
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }



            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            // ���ÿ� Ű�� �Է½� ����� �̻������� ���� �����ϴ� ���ǹ�
            if (vector.x != 0)
                vector.y = 0;


            //��Ű�� ������ -1���� ������vector.x�� Dirx�� ����
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            bool checkCollsionFlag = base.CheckCollsion();
            if (checkCollsionFlag)
                break;
            // ��ȯ�Ǵ� ���� true �� �� �Ʒ��ڵ� ���� X (�̵� �ڵ� ���� X)

            animator.SetBool("Walking", true);

            int temp = Random.Range(1, 4);
            switch (temp)
            {
                case 1:
                    theAudio.Play(walkSound_1);
                    break;
                case 2:
                    theAudio.Play(walkSound_2);
                    break;
                case 3:
                    theAudio.Play(walkSound_3);
                    break;
                case 4:
                    theAudio.Play(walkSound_4);
                    break;
            }

            boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag) //����Ʈ�� ������� ��ĭ�� �����̴� ���� ����
                    currentWalkCount++;
                currentWalkCount++;
                if (currentWalkCount == 12)
                    boxCollider.offset = Vector2.zero;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
        }
        animator.SetBool("Walking", false);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            theSaveNLoad.CallSave();
        }
        if(Input.GetKeyDown(KeyCode.F9))
        {
            theSaveNLoad.CallLoad();
        }
        if (canMove && !notMove)
        {
            // moving input and apply
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());

            }
        }
        if (Input.GetAxisRaw("Horizontal") == 1)
            rightTargetVector = true;
        else if (Input.GetAxisRaw("Horizontal") == -1)
            rightTargetVector = false;
    }

}
