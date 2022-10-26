using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public GameObject knife;
    public GameObject fuel;

    public Transform knifespawn1;
    public Transform knifespawn2;
    public Transform knifespawn3;

    public Transform fuelspawn1;
    public Transform fuelspawn2;
    public Transform fuelspawn3;

    public float randomNumber;
    void Start()
    {
        randomNumber = Random.Range(0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (randomNumber <= 30)
        {
            fuel.transform.position = fuelspawn1.position;
            knife.transform.position = knifespawn1.position;
        }
        else if(randomNumber <= 60)
        {
            fuel.transform.position = fuelspawn2.position;
            knife.transform.position = knifespawn2.position;
        }
        else if (randomNumber <= 90)
        {
            fuel.transform.position = fuelspawn3.position;
            knife.transform.position = knifespawn3.position;
        }

    }
}
