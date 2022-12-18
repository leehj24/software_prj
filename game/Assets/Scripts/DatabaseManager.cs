using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance;

    private PlayerStat thePlayerStat;
    private Inventory theInv;
    private Truth truth;

    public GameObject prefabs_Floating_Text;
    public GameObject parent;


    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton
    // 1. �� �̵� A(�̺�Ʈ ��������(true false) ����) <-> A ---> database, � ������ ���� true ( ��������)
    // 2. ���̺�� �ε�
    // 3. �̸� �����θ� ����(������)
    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;  //1��° ����ġ (event1 --> true) �� ������ �迭�� ������ ���

    public List<Item> itemList = new List<Item>();

    public void UseItem(int _itemID)
    {
        switch(_itemID)
        {
            case 20000:
                if (thePlayerStat.hp >= thePlayerStat.currentHp + 5)
                    thePlayerStat.currentHp += 5;
                else
                    thePlayerStat.currentHp = thePlayerStat.hp;
                FloatText(5);
                break;
            case 20001:
                if (thePlayerStat.hp >= thePlayerStat.currentHp + 10)
                    thePlayerStat.currentHp += 10;
                else
                    thePlayerStat.currentHp = thePlayerStat.hp;
                FloatText(10);
                break;
            case 00:
                switches[1] = true;
                break;
        }
    }

    public void FloatText(int number)
    {
        Vector3 vector = thePlayerStat.transform.position;
        vector.y += 60;

        GameObject clone = Instantiate(prefabs_Floating_Text, vector, Quaternion.Euler(Vector3.zero));

        clone.GetComponent<FloatingText>().text.color = Color.green;
        clone.GetComponent<FloatingText>().text.text = number.ToString();
        clone.GetComponent<FloatingText>().text.fontSize = 25;
        clone.transform.SetParent(parent.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        truth = FindObjectOfType<Truth>();
        theInv = FindObjectOfType<Inventory>();
        thePlayerStat = FindObjectOfType<PlayerStat>();
        itemList.Add(new Item(10000, "������ ����", "���� �𸣰�����, ���ſ� �����̴�. ������ �ϳ� ���ð� ����.", Item.ItemType.Equip,2));
        itemList.Add(new Item(10001, "�峭�� ��", "�Ѻ��⿣ �������� ��������, �峭���� �Ұ��ϴ�.", Item.ItemType.Equip,4));
        itemList.Add(new Item(10002, "���δ�", "�𼭸��� �ϳ� ��ī�ο� ���δ��̴�.", Item.ItemType.Equip,6));
        itemList.Add(new Item(20003, "�غص帵ũ", "�츮�� ���� ��ź�帵ũ, �����Բ� �帮��.", Item.ItemType.Equip));
        itemList.Add(new Item(20000, "�Ƿ�ȸ����", "������ �ణ�� ü���� ȸ���Ҽ� �ִ� ȸ�����̴�.", Item.ItemType.Use));
        itemList.Add(new Item(20001, "Ŀ��", "������ ü���� ȸ���� �� �ִ� Ŀ���̴�.", Item.ItemType.Use));
        itemList.Add(new Item(20002, "�����ؼ���", "���� ���� ���̴�.", Item.ItemType.Use));
        itemList.Add(new Item(30000, "USB1", "������ ����ִ� USB�̴�.", Item.ItemType.Use));
        itemList.Add(new Item(30001, "USB2", "������ ����ִ� USB�̴�.", Item.ItemType.Use));
        itemList.Add(new Item(30002, "USB2", "������ ����ִ� USB�̴�.", Item.ItemType.Use));
        itemList.Add(new Item(30003, "����", "��򰡿� ���� �� ���� �����̴�.", Item.ItemType.Use));
        itemList.Add(new Item(30004, "�÷���", "��ο� ������ ����� �� ���� �� �ϴ�.", Item.ItemType.Use));
        itemList.Add(new Item(30005, "�ڹ���", "��򰡿� ����� �� ���� �� ���� �ڹ����̴�.", Item.ItemType.Use));
        itemList.Add(new Item(01, "����1", "2", Item.ItemType.Quest));
        itemList.Add(new Item(02, "����2", "0", Item.ItemType.Quest));
        itemList.Add(new Item(03, "����3", "2", Item.ItemType.Quest));
        itemList.Add(new Item(04, "����4", "2", Item.ItemType.Quest));
        itemList.Add(new Item(05, "����5", "3", Item.ItemType.Quest));
        itemList.Add(new Item(06, "����6", "5", Item.ItemType.Quest));
        itemList.Add(new Item(07, "����7", "7", Item.ItemType.Quest));
        itemList.Add(new Item(08, "����8", "8", Item.ItemType.Quest));
        itemList.Add(new Item(09, "����9", "5", Item.ItemType.Quest));
        itemList.Add(new Item(10, "����10", "2", Item.ItemType.Quest));
        itemList.Add(new Item(20, "��� ����", "����ǻ��, 20180", Item.ItemType.Quest));
        itemList.Add(new Item(00, "���� ����", "�̷��� ���� ���� ���Ǽ���", Item.ItemType.Use));
    }
}
