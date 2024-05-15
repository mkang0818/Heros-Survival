using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContorller : MonoBehaviour
{
    public GameObject InGameManager;

    GameObject target;
    public Vector3 offset;
    void Start()
    {

    }
    void Update()
    {
        target = InGameManager.GetComponent<InGameManager>().player.gameObject;

        if (target != null)
        {
            transform.position = target.transform.position + offset;
        }
    }
}
