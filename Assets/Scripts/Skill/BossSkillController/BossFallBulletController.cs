using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFallBulletController : MonoBehaviour
{
    public float damage;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        Invoke("CapsuleColOn",0.5f);
        Destroy(gameObject,1.2f);
    }
    void CapsuleColOn()
    {
        GetComponent<CapsuleCollider>().enabled = true;
    }
}
