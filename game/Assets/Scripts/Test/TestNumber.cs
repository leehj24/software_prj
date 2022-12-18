using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNumber : MonoBehaviour
{

    private OrderManager theOrder;
    private NumberSystem theNumber;

    public bool flag;
    public int correctNumber;

    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        theNumber = FindObjectOfType<NumberSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag && collision.gameObject.name == "Player")
        {
            StartCoroutine(ACoroutine());
        }
    }

    IEnumerator ACoroutine()
    {
        flag = true;
        theOrder.NotMove();
        theNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(() => !theNumber.activated);
        theOrder.Move();
    }
}
