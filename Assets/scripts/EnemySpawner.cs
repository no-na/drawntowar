using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    public GameObject calvary;
    public GameObject soldier;
	public GameObject crossbowman;
	public GameObject giant;
	public GameObject wizard;
    public float spawnTime = 6f;
	
	public GameObject resultScreen;
	public Sprite winSprite;

    private int soldBase = 3;
    private int calvBase = 1;
	private int crossBase = -2;
	private int giantBase = -5;
	private int wizardBase = -10;
    private int level = 1;

    private float tillSpawn;

	// Use this for initialization
	void Start () {
        //Call Spawn function every amount of spawnTime
        tillSpawn = 0f;
		
	}
	
	void Update()
    {
        tillSpawn -= Time.deltaTime;
        if(tillSpawn <= 0f)
        {
            for(int i = 0; i < soldBase + level; i ++)
            {
                Instantiate(soldier, transform.position + new Vector3(0, Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
            }

            for (int i = 0; i < calvBase + level; i++)
            {
                Instantiate(calvary, transform.position + new Vector3(0, Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
            }
			
			for (int i = 0; i < crossBase + level; i++)
            {
                Instantiate(crossbowman, transform.position + new Vector3(0, Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
            }
			for (int i = 0; i < giantBase + level; i++)
            {
                Instantiate(giant, transform.position + new Vector3(0, Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
            }
			for (int i = 0; i < wizardBase + level; i++)
            {
                Instantiate(wizard, transform.position + new Vector3(0, Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
            }

            tillSpawn = spawnTime;
			level++;
        }
		if(level > 20){
			resultScreen.SetActive(true);
			resultScreen.GetComponent<Image>().sprite = winSprite;
		}
        
    }
}
