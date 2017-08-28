using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int health;
	public GameObject resultScreen;
	public Sprite loseSprite;


    // Update is called once per frame
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            print("Castle is dead");
			resultScreen.SetActive(true);
			resultScreen.GetComponent<Image>().sprite = loseSprite;
            //myController.Die();
        }
    }
}
