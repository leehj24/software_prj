using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{

    public int itemID; // ������ ���� ID�� , �ߺ� �Ұ�
    public string itemName; // �ߺ� ����
    public string itemDescription;
    public int itemCount; //���� ����
    
    public Sprite itemIcon;
    public ItemType itemType;

    public enum ItemType
    {
        Use,
        Equip,
        Quest,
        ETC
    }

    public int atk;

    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType, int _atk = 0, int _itemCount = 1) //�����ڷ� ���(�� �����ڸ� �̿��� ���� �ٷ� ä��)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;
        atk = _atk;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
