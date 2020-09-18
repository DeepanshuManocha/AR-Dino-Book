using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ARController : MonoBehaviour
{
    public Transform gameCamera;
    private Transform currentObject;
    public Retcile retcile;
    public GameObject prefab;
    private int count;
    private GameObject spwanedObject;
    public List<Animator> animators = new List<Animator>();

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
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
                    foreach (Animator animators in animators)
                    {
                        if (animators.CompareTag("Trex") && hitInfo.transform.CompareTag("Trex"))
                        {
                            Debug.Log("_____________Trex Working_________");
                            //animators.SetBool("TRex",true);
                            retcile.value = 0;
                        }
                        else if (animators.CompareTag("Velociraptor") && hitInfo.transform.CompareTag("Velociraptor"))
                        {
                            Debug.Log("_____________Velociraptor Working_________");
                            animators.SetBool("Velociraptor", true);
                            retcile.value = 0;
                        }
                    }
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
                        if(count<1)
                        {
                            spwanedObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
                            count++;
                        }
                        else
                        {
                            spwanedObject.transform.position = hit.Pose.position;
                        }

                        var anchor = plane.CreateAnchor(hit.Pose);
                        spwanedObject.transform.parent = anchor.transform;
                    }
                }
            }       
        }
    }
}
