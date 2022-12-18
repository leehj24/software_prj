using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;
    private PlayerManager thePlayer;
    private FadeManager theFade;
    private OrderManager theOrder;
    private AudioManger theAudio;

    [SerializeField]
    public string dooropen;

    public Animator anim_1;
    public Animator anim_2;

    public int door_count;

    [Tooltip("UP, DOWN, LEFT, RIGHT")]
    public string direction; //ĳ���Ͱ� ���� ����
    private Vector2 vector; //gerFloat("DirX")

    [Tooltip("���� ������: ture, ���� ������: false")]
    public bool door; //���� �ֳ� ���� üũ

    void Start()
    {
        theAudio = FindObjectOfType<AudioManger>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    public void SelectMap(string _tranfer)
    {
        string mapName = _tranfer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (door)
        {
            if (collision.gameObject.name == "Player")
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    vector.Set(thePlayer.animator.GetFloat("DirX"), thePlayer.animator.GetFloat("DirY"));
                    switch (direction)
                    {
                        case "UP":
                            if(vector.y == 1f)
                                StartCoroutine(TranferCoroutine(transferMapName));
                            break;
                        case "DOWN":
                            if (vector.y == -1f)
                                StartCoroutine(TranferCoroutine(transferMapName));
                            break;
                        case "RIGHT":
                            if (vector.x == 1f)
                                StartCoroutine(TranferCoroutine(transferMapName));
                            break;
                        case "LEFT":
                            if (vector.x == -1f)
                                StartCoroutine(TranferCoroutine(transferMapName));
                            break;
                        default:
                            StartCoroutine(TranferCoroutine(transferMapName));
                            break;
                    }   
                }                
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!door)
        {
            if(collision.gameObject.name == "Player")
            {
                StartCoroutine(TranferCoroutine(transferMapName));
            }
        }
    }


    public IEnumerator TranferCoroutine(string _tranferMap)
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();
        theFade.FadeOut();
        if(door)
        {
            anim_1.SetBool("Open", true);
            theAudio.Play(dooropen);
            if (door_count == 2)
            {
                anim_2.SetBool("Open", true);
            }
        }
        yield return new WaitForSeconds(0.5f);

        theOrder.SetTransparent("player");
        if (door)
        {
            anim_1.SetBool("Open", false);
            if (door_count == 2)
            {
                anim_2.SetBool("Open", false);
            }
        }

        yield return new WaitForSeconds(0.5f);
        theOrder.UnSetTransparent("player");

        thePlayer.currentMapName = _tranferMap;
        SceneManager.LoadScene(_tranferMap);
        theFade.FadeIn();
        theOrder.Move();
    }
}
