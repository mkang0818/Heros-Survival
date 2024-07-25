using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public InGameManager InGameManager;
    int Stage = 0;

    [SerializeField] public GameObject target;
    public GameObject SpawnPos;
    public string EnemyText;
    private string SpawnEfxString = "SpawnEfx";

    public bool IsBomb = false;
    public float level = 22;

    private PoolingManager poolingManager;

    void Awake()
    {
        poolingManager = PoolingManager.instance;
    }

    void Update()
    {
        //스테이지마다 줄이기
        Stage = InGameManager.StageNum;
    }

    public IEnumerator spawnMosnter()
    {
        while (true)
        {
            float randXPos = Random.Range(-10f, 10f);
            float randZPos = Random.Range(-10f, 10f);
            Vector3 SpawnEfxPos = new Vector3(randXPos,0 , randZPos);

            var EmySpawnEfx = poolingManager.GetGo(SpawnEfxString);

            EmySpawnEfx.transform.position = SpawnEfxPos;
            yield return new WaitForSeconds(0.5f);
            EmySpawnEfx.GetComponent<SpawnEfx>().EfxDestroy();

            var monster = poolingManager.GetGo(EnemyText);
            EnemyController enemyController = monster.GetComponent<EnemyController>(); // 캐싱된 변수 사용 Getcomponent 호출 줄이기
            enemyController.InitEmy();
            monster.transform.position = SpawnEfxPos;

            enemyController.target = this.target;
            enemyController.SkillBulletTime = 3;
            enemyController.levelNum = Stage;
            if (IsBomb) enemyController.isBomb = true;

            yield return new WaitForSeconds(level / 2);
        }
    }
}
