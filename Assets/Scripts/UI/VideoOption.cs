using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoOption : MonoBehaviour
{
    FullScreenMode screenMode;
    public Toggle fullscreenBtn;
    public TMP_Dropdown resolutionsDropdown;
    List<Resolution> resolutions = new List<Resolution>();
    int resolutionNum;
    private void Start()
    {
        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
        //initUI();
    }
    void initUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
                resolutions.Add(Screen.resolutions[i]);
        }
        resolutionsDropdown.options.Clear();

        int optionNum = 0;
        foreach (Resolution item in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.width + " x " + item.height + " (" + item.refreshRate + "hz)"; 
            resolutionsDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                resolutionsDropdown.value = optionNum;
            optionNum++;
        }
        resolutionsDropdown.RefreshShownValue();
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }
    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        PlayerPrefs.SetInt("isFull", isFull ? 0 : 1);
        //print(PlayerPrefs.GetInt("isFull", 100));
        PlayerPrefs.Save();
    }
    public void OkBtnClick()
    {
        int isFull = PlayerPrefs.GetInt("isFull", 0);
        int width;
        int height;

        if (isFull == 0)
        {
            width = 1920;
            height = 1080;
            screenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            width = Screen.currentResolution.width;
            height = Screen.currentResolution.height;
            screenMode = FullScreenMode.Windowed;
        }

        Screen.SetResolution(width, height, screenMode);
    }
}
