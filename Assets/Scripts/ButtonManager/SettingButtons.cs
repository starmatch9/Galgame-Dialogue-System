using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//��ס������ű������ڡ�Setting��������
public class SettingButtons : MonoBehaviour
{
    //�ֱ����б�
    List<string> resolutions = new List<string>();

    //��ʾģʽ�б�
    List<string> displays = new List<string>();

    public TMP_Text currentDisplay;

    public TMP_Text currentResolution;


    private void Awake()
    {
        //��ʼ���ֱ����б�
        resolutions.Add("1280*720");
        resolutions.Add("1920*1080");
        resolutions.Add("2560*1440");

        //��ʼ����ʾģʽ�б�
        displays.Add("��  ��");
        displays.Add("ȫ  ��");
    }

    public void LeftDisplaySelection()
    {
        //�õ���ȷ������
        int selectionIndex = 0;
        for (int i = 0; i < displays.Count; i++)
        {
            if (displays[i] == currentDisplay.text)
            {
                selectionIndex = i;
                break;
            }
        }

        //������һ���������
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

        //������һ���������
        int targetIndex = selectionIndex + 1;
        if (targetIndex >= 0 && targetIndex < displays.Count)
        {
            currentDisplay.text = displays[targetIndex];
        }
    }

    public void LeftResolutionSelection()
    {
        //�õ���ȷ������
        int selectionIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (resolutions[i] == currentResolution.text)
            {
                selectionIndex = i;
                break;
            }
        }

        //������һ���������
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

        if(currentDisplay.text == "��  ��")
        {
            Screen.fullScreen = false;
        }
        else if(currentDisplay.text == "ȫ  ��")
        {
            Screen.fullScreen = true;
        }
    }
}
