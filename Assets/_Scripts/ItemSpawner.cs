using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> juicePrefab;
    public List<GameObject> foodPrefab;
    [Range(0,1)]
    public float waterProbablilty;
    // Add texture material for drinks
    public Material drinkMaterial;
    public Material foodMaterial;
    // List of spawn locations
    public List<Transform> spawnLocations;  

    // Start is called before the first frame update
    void Start()
    {
        
        foreach (Transform spawn in spawnLocations)
        {
            SpawnObjects(spawn);
        }
        //check if the item is collected
        StartCoroutine(CheckAndRespawn());
    }

    void SpawnObjects(Transform _spawnLocation)
        {
            float randomProb = Random.Range(0f,1f);
            if(randomProb < waterProbablilty)
            {
                GameObject spawnedObject = Instantiate(juicePrefab[0], _spawnLocation.position, Quaternion.Euler(-90,0,0), _spawnLocation);
            }
            else
            {
                GameObject spawnedObject = Instantiate(foodPrefab[0], _spawnLocation.position, Quaternion.Euler(-90,0,0), _spawnLocation);
            }
        }

    IEnumerator CheckAndRespawn()
    {
        while(true)
        {
            foreach(Transform currentTransform in spawnLocations)
            {
                if (!HasChildren(currentTransform))
                {
                    Debug.Log(currentTransform.name + " does not have children. Respawning in 5 seconds...");

                    // Wait for 5 seconds before respawning
                    yield return new WaitForSeconds(10f);

                    // Respawn the prefab at the current transform position
                    SpawnObjects(currentTransform);
                }
                else
                {
                    //Debug.Log(currentTransform.name + " has children.");
                }
            }

            //add a delay between checking each transform
            yield return new WaitForSeconds(1f);
        }
    }

    bool HasChildren(Transform parentTransform)
    {
        return parentTransform.childCount > 0;
    }
    
}
