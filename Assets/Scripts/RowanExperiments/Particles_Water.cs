using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn a Water Particle when Entering a Water Volume Trigger
/// </summary>

public class Particles_Water : MonoBehaviour 
{
	public string tagToLookFor = "Water";

	public GameObject prefabToPool;
	public int numberToPool = 5;
	public List<GameObject> pooledObjects = new List<GameObject>();

	void Awake()
	{
		for (int i = 0; i < numberToPool; i++)
		{
			GameObject prefab = GameObject.Instantiate(prefabToPool, gameObject.transform.position, Quaternion.identity);
			//prefab.transform.SetParent(gameObject.transform);

			prefab.SetActive(false);
			pooledObjects.Add (prefab);
		}
	}

	void OnTriggerEnter(Collider a_other)
	{
		if (a_other.gameObject.tag == tagToLookFor)
		{
			SpawnAPrefab(a_other.transform.position);
		}
	}

	void SpawnAPrefab(Vector3 pos)
	{
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				pooledObjects[i].transform.position =  gameObject.transform.position;
				//pooledObjects[i].transform.rotation = Vector3.up;

				pooledObjects[i].SetActive(true);
				break;
			}
		}
	}

}
