using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWheel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    Rigidbody rigidbody;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            print(Vector3.Distance(transform.position, hit.point));
            if ((Vector3.Distance(transform.position, hit.point) < 1)&&rigidbody.velocity.magnitude<25)
            {
                rigidbody.AddForce(Vector3.forward * 5, ForceMode.Acceleration);
            }
        }
        
    }
}
