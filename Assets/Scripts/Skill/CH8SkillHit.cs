using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH8SkillHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        int EmyLayer = LayerMask.NameToLayer("Enemy");
        if (col.gameObject.layer == EmyLayer)
        {
            print("트리거 충돌");
            EnemyController enemyScript = col.GetComponent<EnemyController>();
            enemyScript.emydata.emyCurHP -= 100;
        }
    }
}
