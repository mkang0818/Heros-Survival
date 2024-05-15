using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineController : MonoBehaviour
{
    public GameObject hit;

    Material color;
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<MeshRenderer>().material;

        StartCoroutine(ChangeColor());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator ChangeColor()
    {
        while (true)
        {
            color.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            color.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Instantiate(hit, transform.position, Quaternion.identity);
        }
    }
}
