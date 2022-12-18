using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{

    [Tooltip("NPCMove�� üũ�ϸ� NPC�� ������")]
    public bool NPCmove;

    public string[] direction; // NPC�� ������ ���� ����
    [Range(1,5)] // inspectorâ�� ��ũ���� �޾���
    public int frequency; // NPC�� �󸶳� ���� �ӵ��� ������ ������(��ĭ�� ���ٸ����)
}
public class NPCManger : MovingObject
{
 
    [SerializeField]
    public NPCMove npc;
    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        StartCoroutine(MoveCoroutine());
    }

    public void SetMove()
    {
       
    }
    public void SetNotMove()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for(int i = 0; i < npc.direction.Length; i++)
            {

                yield return new WaitUntil(() => queue.Count < 2); //npcCanMove�� ture�� �ɶ����� ��ٸ�
                base.Move(npc.direction[i], npc.frequency);

                if (i == npc.direction.Length - 1)
                    i = -1;
            }
        }
    }

}