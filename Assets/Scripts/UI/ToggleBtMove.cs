using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToggleBtMove : MonoBehaviour
{
    public bool IsShake;
    public bool IsAI;
    public bool IsRange;
    public bool IsFullScreen;
    bool IsOn;
    public RectTransform Handle;
    // Update is called once per frame
    private void Start()
    {
        if (IsShake)
        {
            IsOn = GameManager.Instance.IsShake;
            ToggleMove(IsOn);
            //print("isshake" + IsOn + "true¸é ÄÑÁü false¸é ²¨Áü");
        }
        else if (IsAI)
        {
            IsOn = GameManager.Instance.IsAI;
            ToggleMove(IsOn);
            //print("ai" + IsOn + "true¸é ÄÑÁü false¸é ²¨Áü");
        }
        else if (IsRange)
        {
            IsOn = GameManager.Instance.IsRange;
            ToggleMove(IsOn);
            //print("ai" + IsOn + "true¸é ÄÑÁü false¸é ²¨Áü");
        }
        else if (IsFullScreen)
        {
            IsOn = GameManager.Instance.IsScreen;
            ToggleMove(IsOn);
            //print(PlayerPrefs.GetInt("isFull", 100));
            //print("screen" + IsOn + "true¸é ÄÑÁü false¸é ²¨Áü");
        }
    }
    public void ToggleMove(bool isFull)
    {
        IsOn = isFull ? true : false;
        if (IsOn)
        {
            Handle.transform.DOMoveX(transform.position.x + 15, 0.5f);
        }
        else if (!IsOn)
        {
            Handle.transform.DOMoveX(transform.position.x - 15, 0.5f);
        }
    }
}
