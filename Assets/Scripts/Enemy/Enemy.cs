using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public EmyStatScriptale emydata;

    private string DiaText = "DiamondLight";
    private string CoinText = "CoinLight";

    public virtual void initSetting(EmyStatScriptale emydata)
    {
        this.emydata = emydata;
    }

    public abstract void Attack(GameObject target);

    public virtual void Dead(GameObject target, bool isBomb, GameObject BombEffect)
    {
        PlayerController playerController = target.GetComponent<PlayerController>();

        if (emydata.emyCurHP <= 0)
        {
            StopAllCoroutines();
            float DiamondPer = Random.Range(0, 100);
            if (DiamondPer <= playerController.herodata.diamondPer)
            {
                Vector3 DiaPos = new Vector3(transform.position.x + Random.Range(-1, 1), transform.position.y, transform.position.z + Random.Range(-1, 1));
                var DiaObj = PoolingManager.instance.GetGo(DiaText);
                DropLightController diaController = DiaObj.GetComponent<DropLightController>();

                DiaObj.transform.position = DiaPos;
                diaController.target = target;
                diaController.CreateDropObj();
            }
            Vector3 CoinPos = new Vector3(transform.position.x + Random.Range(-1, 1), transform.position.y, transform.position.z + Random.Range(-1, 1));
            
            var CoinObj = PoolingManager.instance.GetGo(CoinText);
            DropLightController coinController = CoinObj.GetComponent<DropLightController>();

            CoinObj.transform.position = CoinPos;
            coinController.target = target;
            coinController.CreateDropObj();
            playerController.herodata.curExp += playerController.herodata.hasExp;

            int rand = Random.Range(0,100);
            if (isBomb && rand>=50) Instantiate(BombEffect,transform.position,Quaternion.identity);
            GameManager.Instance.Score += emydata.emyCost;

            if (emydata.emyCode != 8)
            {
                gameObject.GetComponent<EnemyController>().DeadEvent();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public virtual void LevelUp(float levelNum)
    {
        int EmyLevel = emydata.emyCode switch
        {
            1 => EmyLevel = 0,
            2 => EmyLevel = 2,
            3 => EmyLevel = 6,
            4 => EmyLevel = 10,
            5 => EmyLevel = 13,
            6 => EmyLevel = 15,
            7 => EmyLevel = 19,
            8 => EmyLevel = 19,
            _ => EmyLevel = 0
        };
        float dd = levelNum - (1 + EmyLevel);

        for (int i = 0; i < levelNum - (1 + EmyLevel); i++)
        {
            emydata.emyMaxHP *= 1.05f;
            emydata.emyCurHP *= 1.05f;
            emydata.emyAttack *= 1.03f;
        }
    }
}
