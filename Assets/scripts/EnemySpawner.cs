using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    public GameObject calvary;
    public GameObject soldier;
    public GameObject crossbowman;
    //public GameObject wizard;
    //public GameObject giant;

    //import file with level spawns
    public float spawnTime = 6f;

    private int soldBase;
    private int calvBase;
    private int crossBase;
    private bool soldDone;
    private bool calvDone;
    private bool crossDone;

    private int level;
   // private string levelDirectory = Directory.GetCurrentDirectory() + "\\Assets\\resources\\levelList.txt";
    private float tillSpawn;

    private int[] enemyDistro= new int[6] {1,1,1,0,0,0};

    // Use this for initialization
    void Start()
    {
        //Debug.Log(levelDirectory);
        //string temp= "\\Assets\\resources\\levelList.txt";
        //Load(temp);
        //Call Spawn function every amount of spawnTime

        soldBase = enemyDistro[0];
        crossBase = enemyDistro[1];
        calvBase = enemyDistro[2];
        tillSpawn = 0f;

    }

    void Update()
    {
        tillSpawn -= Time.deltaTime;
        if (tillSpawn <= 0f & !(soldDone & calvDone & crossDone))
        {
            for (int i = 0; i < soldBase + level; i++)
            {
                Instantiate(soldier, transform.position + new Vector3(0, UnityEngine.Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
                soldDone = true;
            }

            for (int i = 0; i < calvBase + level; i++)
            {
                Instantiate(calvary, transform.position + new Vector3(0, UnityEngine.Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
                calvDone = true;
            }

            for (int i = 0; i < crossBase + level; i++)
            {
                Instantiate(crossbowman, transform.position + new Vector3(0, UnityEngine.Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
                crossDone = true;
            }

            tillSpawn = spawnTime;

        }

    }

    private bool Load(string fileName)
    {
        try
        {
            Debug.Log(fileName);
            string line;

            StreamReader theReader = new StreamReader(fileName);
            Debug.LogError("Beep");

            using (theReader)
            {
                Debug.LogError("Beep");
                do
                {
                    line = theReader.ReadLine();

                    if (line != null)
                    {
                        string[] entries = line.Split(',');
                        if (entries.Length > 0)
                        {
                            soldBase = Int32.Parse(entries[0]);
                            crossBase = Int32.Parse(entries[1]);
                            calvBase = Int32.Parse(entries[2]);
                            Debug.Log("Soldiers: " + soldBase);
                            Debug.Log("Crossbowmen: " + crossBase);
                            Debug.Log("Cavalry: " + calvBase);
                        }
                    }
                }
                while (line != null);

                theReader.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("{0\n", e.Message);
            return false;
        }
    }

}
