using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstantiator : MonoBehaviour
{
    public Transform spawnTransform; // the Transform to instantiate the objects at
    public List<GameObject> objectsToInstantiate; // the list of objects to instantiate
    public float interval = 5f; // the interval between object instantiations

    void Start()
    {
        StartCoroutine(InstantiateObjects()); // start the coroutine to instantiate the objects
    }

    IEnumerator InstantiateObjects()
    {
        while (true)
        {
            // instantiate a random object from the list at the spawnTransform position
            GameObject obj = Instantiate(objectsToInstantiate[Random.Range(0, objectsToInstantiate.Count)], spawnTransform.position, Quaternion.identity);

            // wait for the interval before instantiating another object
            yield return new WaitForSeconds(interval);
        }
    }
}