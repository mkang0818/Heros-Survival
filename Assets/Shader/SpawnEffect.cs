using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public AudioClip CharappearSound;

    [Header("Body")]
    [SerializeField] private Renderer _Body_renderer;
    [SerializeField] private Material Body_mtrlOrg;
    [SerializeField] private Material Body_mtrlDissolve;
    [SerializeField] private Material Body_mtrlPhase;

    [Header("Weapon")]
    [SerializeField] private Renderer[] _Weapon_renderer = new Renderer[2];
    [SerializeField] private Material Weapon_mtrlOrg;
    [SerializeField] private Material Weapon_mtrlDissolve;
    [SerializeField] private Material Weapon_mtrlPhase;

    [Header("Head")]
    [SerializeField] private Renderer _Head_renderer;
    [SerializeField] private Material Head_mtrlOrg;
    [SerializeField] private Material Head_mtrlDissolve;
    [SerializeField] private Material Head_mtrlPhase;



    [SerializeField] private float BodyfadeTime = 2f;
    [SerializeField] private float WeaponfadeTime = 1f;
    [SerializeField] private float HeadfadeTime = 2f;


    // Start is called before the first frame update
    void Start()
    {
        _Body_renderer.material = Body_mtrlPhase;
        _Head_renderer.material = Head_mtrlPhase;
        for (int i=0;i< _Weapon_renderer.Length; i++)  _Weapon_renderer[i].material = Weapon_mtrlPhase;

        HeadDofade(0, 2, HeadfadeTime);
        BodyDofade(0, 2, BodyfadeTime);
        WeaponDofade(0, 2, WeaponfadeTime);

    }

    //BodyFade
    void BodyDofade(float start, float dest, float time)
    {
        if(CharappearSound!=null) Invoke("SoundPlay",0.6f);
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", start, "to", dest, "time", time, "onupdatetarget", gameObject,
            "onupdate", "BodyTweenOnUpdate", "oncomplete", "BodyTweenOnComplte",
            "easetype", iTween.EaseType.easeInOutCubic));
    }
    void SoundPlay()
    {
        SoundManager.Instance.SoundPlay("AppearChar", CharappearSound);
    }
    void BodyTweenOnUpdate(float value)
    {
        _Body_renderer.material.SetFloat("_Split_Value", value);
    }
    void BodyTweenOnComplte()
    {
        _Body_renderer.material = Body_mtrlOrg;
    }

    //WeaponFade
    void WeaponDofade(float start, float dest, float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", start, "to", dest, "time", time, "onupdatetarget", gameObject,
            "onupdate", "WeaponTweenOnUpdate", "oncomplete", "WeaponTweenOnComplte",
            "easetype", iTween.EaseType.easeInOutCubic));
    }
    void WeaponTweenOnUpdate(float value)
    {
        for (int i = 0; i < _Weapon_renderer.Length; i++)  _Weapon_renderer[i].material.SetFloat("_Split_Value", value);
    }
    void WeaponTweenOnComplte()
    {
        for (int i = 0; i < _Weapon_renderer.Length; i++) _Weapon_renderer[i].material = Body_mtrlOrg;
    }

    //HeadFade
    void HeadDofade(float start, float dest, float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", start, "to", dest, "time", time, "onupdatetarget", gameObject,
            "onupdate", "HeadTweenOnUpdate", "oncomplete", "HeadTweenOnComplte",
            "easetype", iTween.EaseType.easeInOutCubic));
    }
    void HeadTweenOnUpdate(float value)
    {
        _Head_renderer.material.SetFloat("_Split_Value", value);
    }
    void HeadTweenOnComplte()
    {
        _Head_renderer.material = Head_mtrlOrg;
        if(GetComponent<PlayerController>() != null) GetComponent<PlayerController>().isStart = true;
    }
}
