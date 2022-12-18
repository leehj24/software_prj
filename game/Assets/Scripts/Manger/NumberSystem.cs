using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberSystem : MonoBehaviour
{
    private AudioManger theAudio;
    public string keySound;
    public string enterSound;
    public string cancelSound;
    public string correctSound;

    [SerializeField]
    public float MovingSize;

    private int count; //�迭�� ũ��
    private int selectedTextBox; // ���õ� �ڸ���
    private int result; //�÷��̾ ������ ��(���� ��)
    private int correctNumber;

    private string tempNumber;

    public GameObject superObject; // ȭ�� ��� ����� ���� ����
    public GameObject[] panel;
    public TMP_Text[] Number_Text;

    public Animator anim;

    public bool activated;
    private bool keyInput;
    private bool correctFlag; 

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManger>();    
    }

    public void ShowNumber(int _correctNum)
    {
        correctNumber = _correctNum;
        activated = true;
        correctFlag = false;

        string temp = correctNumber.ToString();  //length�Ӽ��� �̿��ϱ� ���� ���ڿ��� ��ȯ
        for(int i = 0; i < temp.Length; i++)
        {
            count = i;
            panel[i].SetActive(true);
            Number_Text[i].text = "0";
        }

        superObject.transform.position = new Vector3(superObject.transform.position.x + (MovingSize * count), superObject.transform.position.y, superObject.transform.position.z);

        selectedTextBox = 0;
        result = 0;
        SetColor();
        anim.SetBool("Appear", true);
        keyInput = true;
    }

    public bool GetResult()
    {
        return correctFlag;
    }

    public void SetNumber(string _arrow)
    {
        int temp = int.Parse(Number_Text[selectedTextBox].text); // ���õ� �ڸ����� �ؽ�Ʈ�� int������ ������ȯ

        if(_arrow == "DOWN")
        {
            if (temp == 0)
                temp = 9;
            else
                temp--;
            
        }
        if (_arrow == "UP")
        {
            if (temp == 9)
                temp = 0;
            else
                temp++;
        }
        Number_Text[selectedTextBox].text = temp.ToString();
    }

    public void SetColor()
    {
        Color color = Number_Text[0].color;
        color.a = 0.3f;
        for(int i = 0; i <= count; i++)
        {
            Number_Text[i].color = color;
        }
        color.a = 1f;
        Number_Text[selectedTextBox].color = color;
    }
    // Update is called once per frame
    void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                theAudio.Play(keySound);
                SetNumber("DOWN");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                theAudio.Play(keySound);
                SetNumber("UP");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theAudio.Play(keySound);
                if (selectedTextBox < count)
                    selectedTextBox++;
                else
                    selectedTextBox = 0;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                theAudio.Play(keySound);
                if (selectedTextBox > 0)
                    selectedTextBox--;
                else
                    selectedTextBox = count;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.F)) //����
            {
                theAudio.Play(keySound);
                keyInput = false;
                StartCoroutine(OXCoroutine());
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) //���
            {
                theAudio.Play(keySound);
                keyInput = false;
                StartCoroutine(ExitCoroutine());
            }
        }
    }

    IEnumerator OXCoroutine()
    {
        Color color = Number_Text[0].color;
        color.a = 1f;


        for(int i = count; i >=0; i--)
        {
            Number_Text[i].color = color;
            tempNumber += Number_Text[i].text; // ������ �ƴϸ� ���� �ݴ�� ��
        }

        yield return new WaitForSeconds(1f);

        result = int.Parse(tempNumber);

        if(result == correctNumber)
        {
            theAudio.Play(correctSound);
            correctFlag = true;
        }
        else
        {
            theAudio.Play(cancelSound);
            correctFlag = false;
        }

        StartCoroutine(ExitCoroutine());
    }

    IEnumerator ExitCoroutine()
    {
        Debug.Log("�츮�� �� �� = " + result + "���� = " + correctNumber);
        Debug.Log(correctFlag);
        result = 0;
        tempNumber = "";
        anim.SetBool("Appear", false);

        yield return new WaitForSeconds(0.1f);

        for(int i =0; i<=count; i++)
        {
            panel[i].SetActive(false);
        }

        superObject.transform.position = new Vector3(superObject.transform.position.x - (MovingSize * count), superObject.transform.position.y, superObject.transform.position.z);


        activated = false;
    }
}
