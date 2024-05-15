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
    // Start is called before the first frame update
    void Start()
    {
    }
    void Update()
    {
        Stage = InGameManager.StageNum;

        //스테이지마다 줄이기

    }
    public IEnumerator spawnMosnter()
    {
        while (true)
        {
            float randXPos = Random.Range(-10f, 10f);
            float randZPos = Random.Range(-10f, 10f);
            Vector3 SpawnEfxPos = new Vector3(randXPos,0 , randZPos);

            //GameObject monster1111 = Instantiate(SpawnPos, SpawnEfxPos, Quaternion.identity);
            var EmySpawnEfx = PoolingManager.instance.GetGo(SpawnEfxString);

            EmySpawnEfx.transform.position = SpawnEfxPos;
            yield return new WaitForSeconds(0.5f);
            EmySpawnEfx.GetComponent<SpawnEfx>().EfxDestroy();

            var monster = PoolingManager.instance.GetGo(EnemyText);
            monster.GetComponent<EnemyController>().InitEmy();
            monster.transform.position = SpawnEfxPos;

            monster.GetComponent<EnemyController>().target = this.target;
            monster.GetComponent<EnemyController>().levelNum = Stage;
            if (IsBomb) monster.GetComponent<EnemyController>().isBomb = true;
            yield return new WaitForSeconds(level / 2);
        }
    }
    /*void EmyType(GameObject EmyObj)
    {
        switch (EnemyText)
        {
            case "Emy2":
                EmyObj.GetComponent<EmyLv2>().StartPattern();
                break;
            case "Emy3":
                EmyObj.GetComponent<EmyLv3>().StartPattern();
                break;
            case "Emy4":
                EmyObj.GetComponent<EmyLv4>().StartPattern();
                break;
        }
    }*/
}
