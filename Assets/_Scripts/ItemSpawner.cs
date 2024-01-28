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
        SpawnObjects(spawnLocations);
        //check if the item is collected
        StartCoroutine(CheckAndRespawn());
    }

    void SpawnObjects(List<Transform> _spawnLocations)
        {
            foreach(Transform spawnTransform in _spawnLocations)
            {
                float randomProb = Random.Range(0f,1f);
                if(randomProb < waterProbablilty)
                {
                    GameObject spawnedObject = Instantiate(juicePrefab[0], spawnTransform.position, Quaternion.Euler(-90,0,0), spawnTransform);
                }
                else
                {
                    GameObject spawnedObject = Instantiate(foodPrefab[0], spawnTransform.position, Quaternion.Euler(-90,0,0), spawnTransform);
                }
            }
        }

    IEnumerator CheckAndRespawn(){
        while(true){
            foreach(Transform currentTransform in spawnLocations){
                if (!HasChildren(currentTransform))
                {
                    Debug.Log(currentTransform.name + " does not have children. Respawning in 5 seconds...");

                    // Wait for 5 seconds before respawning
                    yield return new WaitForSeconds(10f);

                    // Respawn the prefab at the current transform position
                    SpawnObjects(spawnLocations);
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
