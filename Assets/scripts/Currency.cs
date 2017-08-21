using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public int startingCurrency;
    [HideInInspector]
    public int currentCurrency;
	
	private Animator myAnim;

    // Use this for initialization
    void Start()
    {
        currentCurrency = startingCurrency;
		myAnim = GetComponent<Animator>();
    }

    public void Pay(int unitCost)
    {
        currentCurrency -= unitCost;
    }
    public void GetPaid(int unitBounty)
    {
		myAnim.SetTrigger("money");
        currentCurrency += unitBounty;
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "DOSH: " + currentCurrency.ToString();
    }
}
