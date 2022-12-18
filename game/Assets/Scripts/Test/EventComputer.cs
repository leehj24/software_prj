using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventComputer : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
 
    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private FadeManager theFade;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theFade = FindObjectOfType<FadeManager>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag && Input.GetKey(KeyCode.F) && thePlayer.animator.GetFloat("DirY") == 1f)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove(); //�̺�Ʈ ���۽� �̵��Ұ�

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move("Player", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("Player", "UP");
        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        
        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);
        theFade.Flash();

        theDM.ShowDialogue(dialogue_3);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move(); //�̺�Ʈ ����� �̵�����
    }
}
