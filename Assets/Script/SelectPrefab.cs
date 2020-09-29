using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPrefab : MonoBehaviour
{
    public GameObject selectedObject;
    private GameObject spawnedObject;

    public void SetPrefab(GameObject prefabObject)
    {
        selectedObject = prefabObject;
        

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(selectedObject) as GameObject;
                spawnedObject.transform.parent = g.transform;
                spawnedObject.transform.position = g.transform.position + Vector3.up*0.7f;
            }
        }
    }

    public void ClearPrefab()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }
    }

}
