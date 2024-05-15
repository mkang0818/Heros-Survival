using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoxController : MonoBehaviour
{
    [HideInInspector] public float curHP;
    public GameObject target;
    public GameObject CH4hitEffect;

    int HPPer = 10;
    public int CoinPer = 10;    
    int DiaPer = 10;

    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpUpdate();
    }
    void hpUpdate()
    {
        if (curHP <= 0)
        {
            Destroy(gameObject);
            int HPNum = Random.Range(0,100);
            int CoinNum = Random.Range(0,100);
            int DiaNum = Random.Range(0,100);
            if (HPNum > HPPer)
            {
                Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(-1, 1), 0, transform.position.z + Random.Range(-1, 1));
                var DropObj = PoolingManager.instance.GetGo("HealthLight");
                DropObj.GetComponent<DropLightController>().target = target;
                DropObj.transform.position = spawnPos;
                DropObj.GetComponent<DropLightController>().CreateDropObj();
            }
            if (CoinNum > CoinPer)
            {
                int CoinCount = Random.Range(1, 4);
                for (int i = 0; i < CoinCount; i++)
                {
                    Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(-1, 1), 0, transform.position.z + Random.Range(-1, 1));
                    var DropObj = PoolingManager.instance.GetGo("CoinLight");
                    //print(DropObj.GetComponent<DropLightController>().target);
                    //print(target);
                    DropObj.GetComponent<DropLightController>().target = target;
                    DropObj.transform.position = spawnPos;
                    DropObj.GetComponent<DropLightController>().CreateDropObj();
                }
            }
            if (DiaNum > DiaPer)
            {
                Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(-1, 1), 0, transform.position.z + Random.Range(-1, 1));
                var DropObj = PoolingManager.instance.GetGo("DiamondLight");
                DropObj.GetComponent<DropLightController>().target = target;
                DropObj.transform.position = spawnPos;
                DropObj.GetComponent<DropLightController>().CreateDropObj();
            }
            
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Bullet"))
        {
            curHP -= 1;
            SoundManager.Instance.SoundPlay("RandomBoxAt", audioClip);
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("AttackPos"))
        {
            curHP -= 3;

            SoundManager.Instance.SoundPlay("RandomBoxAt", audioClip);
            GameObject hiteffect = Instantiate(CH4hitEffect, transform.position, Quaternion.identity);
            Destroy(hiteffect, 0.2f);
        }
    }
}
