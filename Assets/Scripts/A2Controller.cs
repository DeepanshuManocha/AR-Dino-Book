using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class A2Controller : MonoBehaviour
{
    public Transform gameCamera;
    private Transform currentObject;
    public Retcile retcile;
    public GameObject sphere, tile;

    // Start is called before the first frame update
    void Start()
    {
        
        retcile.duration=5f;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray (gameCamera.position, gameCamera.forward);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo))
        {
            if(currentObject != hitInfo.transform)
            {
                currentObject = hitInfo.transform;
                retcile.value = 0;
                retcile.SetActive(true);
            }
            else
            {
                if(retcile.value>=1)
                {
                    var sphereObject = Instantiate(sphere, transform);
                    sphereObject.transform.position = currentObject.transform.position + Vector3.up;
                    sphereObject.transform.parent = currentObject.transform.parent;
                    retcile.value=0;
                }
            }
        }
        else
        {
            retcile.SetActive(false);
            currentObject=null;
        
            if (Session.Status != SessionStatus.Tracking)
            {
                return;
            }
            //Session.GetTrackables<DetectedPlane>(allPlanes, TrackableQueryFilter.All);

            Touch touch;
            if(Input.touchCount > 0 && ((touch=Input.GetTouch(0)).phase == TouchPhase.Began))
            {
                TrackableHit hit;
                TrackableHitFlags flags = TrackableHitFlags.PlaneWithinPolygon;
                if(Frame.Raycast(touch.position.x, touch.position.y, flags, out hit))
                {
                    if((hit.Trackable is DetectedPlane) && Vector3.Dot(Camera.main.transform.position-hit.Pose.position, hit.Pose.rotation*Vector3.up)>0)
                    {
                        DetectedPlane plane = hit.Trackable as DetectedPlane;
                        var tileObject = Instantiate(tile, hit.Pose.position, hit.Pose.rotation);
                        tileObject.transform.localScale = new Vector3(plane.ExtentX/2, 0.05f, plane.ExtentZ/2);
                        var anchor = plane.CreateAnchor(hit.Pose);
                        tileObject.transform.parent = anchor.transform;
                    }
                }
            }       
        }
    }
}
