using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject sphere;

    public void OnMouseDown()
    {
        var sphereObj = Instantiate(sphere);
        sphereObj.transform.localPosition = transform.position + Vector3.up * 1;
    }
}
