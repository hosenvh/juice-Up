using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAuthoring : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    bool IsPlayer;
    public string PlayerName;
    public void SetAsPlayer()
    {
        IsPlayer = true;
    }
    Vector3 cachedPos;
    // Update is called once per frame
    void Update()
    {
        if(IsPlayer)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-Vector3.right * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime);
            }
            if (transform.position != cachedPos)
            {
                PosToServer();
            }
            cachedPos = transform.position;
        }
        
        

    }

    void PosToServer()
    {
       // WsTest.Instance.SendPosToServer(transform.position.x,transform.position.y,transform.position.z);    
    }
}
