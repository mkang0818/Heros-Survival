using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CFXR_Demo_RandomText : MonoBehaviour
{
    public ParticleSystem particles;
    public CartoonFX.CFXR_ParticleText dynamicParticleText;

    [HideInInspector] public float damage;
    void OnEnable()
    {
        //InvokeRepeating("SetRandomText", 0f, 1.5f);
        //Invoke("SetRandomText", 0.1f);

        //SetRandomText();
    }

    void OnDisable()
    {
        CancelInvoke("SetRandomText");
        particles.Clear(true);
    }

    public void SetRandomText()
    {
        // set text and properties according to the random damage:
        // - bigger damage = big text, red to yellow gradient
        // - lower damage = smaller text, fully red
        //float damage = GameObject.Find("ingamemanager").GetComponent<ingame>().hero;

        string text = ((int)damage).ToString();
        float intensity = ((int)damage) / 1000f;
        float size = Mathf.Lerp(0.8f, 1.3f, intensity);
        Color color1 = Color.Lerp(Color.red, Color.yellow, intensity);
        dynamicParticleText.UpdateText(text, size, color1);

        particles.Play(true);
    }
}
