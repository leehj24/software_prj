using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveNLoad : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public float playerX;
        public float playerY;
        public float playerZ;

        public int playerHP;
        public int playerCurrentHP;

        public int playerATK;

        public List<int> playerItemInventory;
        public List<int> playerItemInventoryCount;

        public string mapName;
        public string sceneName;

        public List<bool> swList;
        public List<string> swNameList;
        public List<string> varNameList;
        public List<float> varNumberList;
    }

    private PlayerManager thePlayer;
    private PlayerStat thePlayerStat;
    private DatabaseManager theDatabase;
    private Inventory theInven;
    private FadeManager theFade;

    private Data data;

    private Vector3 vector;


    public void CallSave()
    {
        theDatabase = FindObjectOfType<DatabaseManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        thePlayerStat = FindObjectOfType<PlayerStat>();
        theInven = FindObjectOfType<Inventory>();

        data.playerX = thePlayer.transform.position.x;
        data.playerY = thePlayer.transform.position.y;
        data.playerZ = thePlayer.transform.position.z;


        data.playerHP = thePlayerStat.hp;
        data.playerCurrentHP = thePlayerStat.currentHp;
        data.playerATK = thePlayerStat.atk;

        data.sceneName = thePlayer.currentMapName;

        Debug.Log("���� ������ ����");

        for (int i = 0; i < theDatabase.var_name.Length; i++)
        {
            data.varNameList.Add(theDatabase.var_name[i]);
            data.varNumberList.Add(theDatabase.var[i]);
        }
        for (int i = 0; i < theDatabase.switch_name.Length; i++)
        {
            data.swNameList.Add(theDatabase.switch_name[i]);
            data.swList.Add(theDatabase.switches[i]);
        }
        List<Item> itemList = theInven.Saveitem();

        for(int i = 0; i < itemList.Count; i++)
        {
            Debug.Log("�κ��丮 ������ ���� �Ϸ� : " + itemList[i].itemID);
            data.playerItemInventory.Add(itemList[i].itemID);
            data.playerItemInventoryCount.Add(itemList[i].itemCount);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");

        bf.Serialize(file, data);
        file.Close();

        Debug.Log(Application.dataPath + "�� ��ġ�� ����Ϸ�.");
    }
    public void CallLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/SaveFile.dat",FileMode.Open);
        
        if(file != null && file.Length > 0)
        {
            data = (Data)bf.Deserialize(file);

            theDatabase = FindObjectOfType<DatabaseManager>();
            thePlayer = FindObjectOfType<PlayerManager>();
            thePlayerStat = FindObjectOfType<PlayerStat>();
            theInven = FindObjectOfType<Inventory>();
            theFade = FindObjectOfType<FadeManager>();

            theFade.FadeOut();

            thePlayer.currentMapName = data.sceneName;

            vector.Set(data.playerX, data.playerY, data.playerZ);
            thePlayer.transform.position = vector;

            thePlayerStat.hp = data.playerHP;
            thePlayerStat.atk = data.playerATK;
            thePlayerStat.currentHp = data.playerCurrentHP;

            theDatabase.var = data.varNumberList.ToArray();
            theDatabase.var_name = data.varNameList.ToArray();
            theDatabase.switches = data.swList.ToArray();
            theDatabase.switch_name = data.swNameList.ToArray();

            List<Item> itemList = new List<Item>();

            for(int i = 0; i < data.playerItemInventory.Count; i++)
            {
                for (int j = 0; j < theDatabase.itemList.Count; j++)
                {
                    if(data.playerItemInventory[i] == theDatabase.itemList[j].itemID)
                    {
                        itemList.Add(theDatabase.itemList[j]);
                        Debug.Log("�κ��丮 ������ �ε�:" + theDatabase.itemList[j].itemID);
                        break;
                    }
                }
            }

            for(int i = 0; i < data.playerItemInventoryCount.Count; i++)
            {
                itemList[i].itemCount = data.playerItemInventoryCount[i];
            }

            theInven.LoadItem(itemList);

            StartCoroutine(WaitCoroutine());
            theFade.FadeIn();
        }
        else
        {
            Debug.Log("����� ���̺� ������ ����.");
        }

        file.Close();
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(2f);

        GameManager theGM = FindObjectOfType<GameManager>();
        theGM.LoadStart();

        SceneManager.LoadScene(data.sceneName);
    }
}
