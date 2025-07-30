using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//记住，这个脚本挂载在“Setting”对象上
public class SettingButtons : MonoBehaviour
{
    //分辨率列表
    List<string> resolutions = new List<string>();

    //显示模式列表
    List<string> displays = new List<string>();

    public TMP_Text currentDisplay;

    public TMP_Text currentResolution;


    private void Awake()
    {
        //初始化分辨率列表
        resolutions.Add("1280*720");
        resolutions.Add("1920*1080");
        resolutions.Add("2560*1440");

        //初始化显示模式列表
        displays.Add("窗  口");
        displays.Add("全  屏");
    }

    public void LeftDisplaySelection()
    {
        //得到正确的索引
        int selectionIndex = 0;
        for (int i = 0; i < displays.Count; i++)
        {
            if (displays[i] == currentDisplay.text)
            {
                selectionIndex = i;
                break;
            }
        }

        //索引减一后更改数字
        int targetIndex = selectionIndex - 1;
        if(targetIndex >= 0 && targetIndex < displays.Count)
        {
            currentDisplay.text = displays[targetIndex];
        }

    }
    public void RightDisplaySelection() 
    {
        int selectionIndex = 0;
        for (int i = 0; i < displays.Count; i++)
        {
            if (displays[i] == currentDisplay.text)
            {
                selectionIndex = i;
                break;
            }
        }

        //索引加一后更改数字
        int targetIndex = selectionIndex + 1;
        if (targetIndex >= 0 && targetIndex < displays.Count)
        {
            currentDisplay.text = displays[targetIndex];
        }
    }

    public void LeftResolutionSelection()
    {
        //得到正确的索引
        int selectionIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (resolutions[i] == currentResolution.text)
            {
                selectionIndex = i;
                break;
            }
        }

        //索引减一后更改数字
        int targetIndex = selectionIndex - 1;
        if (targetIndex >= 0 && targetIndex < resolutions.Count)
        {
            currentResolution.text = resolutions[targetIndex];
        }
    }

    public void RightResolutionSelection()
    {
        int selectionIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (resolutions[i] == currentResolution.text)
            {
                selectionIndex = i;
                break;
            }
        }

        int targetIndex = selectionIndex + 1;
        if (targetIndex >= 0 && targetIndex < resolutions.Count)
        {
            currentResolution.text = resolutions[targetIndex];
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveChange()
    {
        if(currentResolution.text == "1280*720")
        {
            Screen.SetResolution(1280, 720, true);
        }
        else if(currentResolution.text == "1920*1080")
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if(currentResolution.text == "2560*1440")
        {
            Screen.SetResolution(2569, 1440, true);
        }

        if(currentDisplay.text == "窗  口")
        {
            Screen.fullScreen = false;
        }
        else if(currentDisplay.text == "全  屏")
        {
            Screen.fullScreen = true;
        }
    }
}
