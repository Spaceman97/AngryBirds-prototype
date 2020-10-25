using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public float Period;
	public GameObject enemy;
	private float timeNextSpawn;
    // Start is called before the first frame update
    void Start()
    {
		timeNextSpawn = Random.Range(0,Period);
    }

    // Update is called once per frame
    void Update()
    {
		timeNextSpawn -=Time.deltaTime;
		if(timeNextSpawn <= 0.0f){
			timeNextSpawn = Period;
			Instantiate(enemy,transform.position, transform.rotation);
		}
    }
}
