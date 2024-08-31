using System.Collections;
using System.Collections.Generic;
//using Cinemachine.Utility;
using UnityEngine;

public class ActionUI : MonoBehaviour
{
    // Start is called before the first frame update
    private LayerMask player;
    private Vector3 DynamicPin;
    private Renderer renderer ;
    private Vector3 size;
    public GameObject hitPoint;
    private float DynamicPinCeiling;
    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        renderer = GetComponent<Renderer>();
        player=LayerMask.GetMask("player");
        StartCoroutine(CheckForOverlap());
        size = renderer.bounds.size / 2;
        aboveObject =renderer.bounds.center+ new Vector3(0, renderer.bounds.size.y, 0);
        DynamicPin = aboveObject;
        DynamicPinCeiling = (canvas.GetComponent<RectTransform>().sizeDelta.y / 2) - 30;
        print(DynamicPinCeiling);
    }

    public Canvas canvas;
    IEnumerator CheckForOverlap()
    {
        RaycastHit hit;
        Ray ray;
        float lerpBack=0;
        float startTime = 0;
        while (true)
        {
            //ray = new Ray(transform.position, (Camera.main.transform.position - transform.position).normalized);
            if(Physics.BoxCast(DynamicPin,size,(Camera.main.transform.position-DynamicPin).normalized,Quaternion.identity,1000,player))
            {
                DynamicPin += new Vector3(0, Time.deltaTime*33f, 0);
                print("up1 "+Time.time);
                if (notif.GetComponent<RectTransform>().anchoredPosition.y > DynamicPinCeiling)
                {
                    DynamicPin -= new Vector3(0, Time.deltaTime*33f, 0);
                    print("ceiling");
                }
                lerpBack = -1;

            }
            else
            {
                
                if (DynamicPin == aboveObject)
                {
                    print("equal");
                }
                else
                {
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0,DynamicPin);
                    lineRenderer.SetPosition(1,aboveObject);
                    if (lerpBack == -1f)
                    {
                        lerpBack = 0;
                        startTime = 0;
                    
                    }
                   
                    lerpBack +=0.01f;
                    var lerpPOs= Vector3.Lerp(DynamicPin, aboveObject, lerpBack);
                    if (Physics.BoxCast(DynamicPin - new Vector3(0, Time.deltaTime*33f, 0), size, (Camera.main.transform.position - lerpPOs).normalized,Quaternion.identity,1000,player))
                    {
                       
                        lerpBack -=0.01f;
                    }
                    else
                    {
                  
                        
                        DynamicPin = lerpPOs;
                        print("lerpDown "+Time.time);
                    } 
                }
            }
            yield return null;
        }
    }
    
    
    private Vector3 notifPos;

    public Transform notif;

    public Vector3 aboveObject;
    // Update is called once per frame
    void Update()
    {         
        notifPos = Camera.main.WorldToScreenPoint(DynamicPin);
       // print(notif.GetComponent<RectTransform>().anchoredPosition.y);
        notif.position = notifPos;
    }
}
