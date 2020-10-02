using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ARController : MonoBehaviour
{
    public Transform gameCamera;
    private Transform currentObject;
    public Retcile retcile;
    public GameObject prefab, handAnim;
    private int count;
    private GameObject spwanedObject;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        retcile.duration=5f;
    }

    IEnumerator AnimationOver(float delayTime, Animator animator, string anim)
    {
        Debug.Log("before" + delayTime + " sec");
        yield return new WaitForSeconds(delayTime);
        animator.SetBool(anim, false);
        Debug.Log("after" + delayTime+ " sec");
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
                    if (hitInfo.transform.CompareTag("Trex"))
                    {
                        hitInfo.collider.GetComponent<Animator>().SetBool("TRex",true);
                        retcile.value = 0;    
                        StartCoroutine(AnimationOver(1.5f, hitInfo.collider.GetComponent<Animator>(), "TRex"));
                        
                    }
                    if (hitInfo.transform.CompareTag("Velociraptor"))
                    {
                        hitInfo.collider.GetComponent<Animator>().SetBool("Velociraptor", true);
                        retcile.value = 0;
                        StartCoroutine(AnimationOver(3.4f, hitInfo.collider.GetComponent<Animator>(), "Velociraptor"));

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
                            handAnim.SetActive(false);
                        }
                        /*else
                        {
                            spwanedObject.transform.position = hit.Pose.position;
                        }*/
                        var anchor = plane.CreateAnchor(hit.Pose);
                        spwanedObject.transform.parent = anchor.transform;
                    }
                }
            }       
        }
    }
}
