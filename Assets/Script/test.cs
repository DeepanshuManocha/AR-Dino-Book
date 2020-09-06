using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject prefab;
    int cont = 0;
    public void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && cont<1)
        {
            Instantiate(prefab, prefab.transform.position, Quaternion.identity);
            cont++;
        }
    }
}
