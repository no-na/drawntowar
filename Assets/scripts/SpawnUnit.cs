using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{

    public GameObject reticule;
    public GameObject unit;
    public Currency currency;

    public void Spawn()
    {
        int newCurrencyAmount = currency.GetComponent<Currency>().currentCurrency - unit.GetComponent<Unit>().cost;

        if (newCurrencyAmount >= 0)
        {
            currency.GetComponent<Currency>().Pay(unit.GetComponent<Unit>().cost);
            reticule.SetActive(true);
            reticule.GetComponent<Reticule>().ResetPosition();
            reticule.GetComponent<Reticule>().unit = unit;
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
