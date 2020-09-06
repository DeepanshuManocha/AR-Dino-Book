using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class A1Controller : MonoBehaviour
{
    public GameObject prefab;
    public Transform firstPersonCam;
    private GameObject spwanableObject;

    public Text noOfPlanes;

    private int count=0;

    private List<DetectedPlane> allPlanes;

    // Start is called before the first frame update
    void Start()
    {
        allPlanes = new List<DetectedPlane>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }
        Session.GetTrackables<DetectedPlane>(allPlanes, TrackableQueryFilter.All);

        noOfPlanes.text = allPlanes.Count.ToString();

        Touch touch;
        if(Input.touchCount<1 || ((touch=Input.GetTouch(0)).phase != TouchPhase.Began))
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        TrackableHit hit;
        TrackableHitFlags flags = TrackableHitFlags.PlaneWithinPolygon;
        if(Frame.Raycast(touch.position.x, touch.position.y, flags, out hit))
        {
            if((hit.Trackable is DetectedPlane) && Vector3.Dot(firstPersonCam.position-hit.Pose.position, hit.Pose.rotation*Vector3.up)>0)
            {
                if (count < 1)
                {
                    spwanableObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
                    Debug.Log("spwaned");
                    count++;
                }
                else
                {
                    spwanableObject.transform.position = hit.Pose.position;
                    Debug.Log("Shifted");
                }
            }
        }
    }
}
