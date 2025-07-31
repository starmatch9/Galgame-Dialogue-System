using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    /*自动播放区*/
    [Header("自动播放按钮")]
    public Image buttonImage;
    public Color runningColor;
    Color currentColor;
    //用于控制协程
    [HideInInspector]
    public bool isAuto = false;
    Coroutine autoPlayCoroutine = null;
    AudioSource audioSource = null;
    //目前只有播放按钮用到了对话管理器所以暂时放在这里
    DialogueManager dialogueManager;
    //需要获取音频管理器中的音频列表
    AudioManager audioManager;

    public void ButtonAuto()
    {
        isAuto = !isAuto;
        if (isAuto)
        {
            autoStart();
            buttonImage.color = runningColor;
        }
        else
        {
            autoEnd();
            buttonImage.color = currentColor;
        }
    }
    IEnumerator AutoPlay()
    {
        while (true)
        {
            if (audioSource.clip != null)
            {
                //事件回调，2018+版本用，这个可以在播放结束后跳出
                yield return new WaitWhile(() => audioSource.isPlaying);
            }

            //如果这段对话的音频是空的，就按打字机协程来
            if (audioManager.currentClip == null)
            {
                //如果协程在运行，就在代码块里循环

                float timeAll = 0f; 
                while (dialogueManager.typeTextCoroutine != null)
                {
                    yield return null;
                    timeAll += Time.deltaTime;
                }
                //打字机结束后，时停时长
                //Debug.Log(timeAll * 6);
                //让阅读时间和字的长度大致成正比
                yield return new WaitForSeconds(timeAll * 6);
            }

            //音频播放结束后等待时间的长度
            yield return new WaitForSeconds(1f);

            dialogueManager.BoxClick();
        }
    }
    
    //协程强制开始时的动作
    public void autoStart()
    {
        if (autoPlayCoroutine != null)
        {
            StopCoroutine(autoPlayCoroutine);
            autoPlayCoroutine = null;
        }
        //在开始协程前重新清空
        autoPlayCoroutine = StartCoroutine(AutoPlay());
    }
    //这个函数可以用来暂时关闭，再调用autoStart即可重新开始（利用isAuto还是true）
    //要想彻底关闭，可以直接调用ButtonAuto
    public void autoEnd()
    {
        if (autoPlayCoroutine != null)
        {
            StopCoroutine(autoPlayCoroutine);
            autoPlayCoroutine = null;
        }
    }


    /*隐藏文本区*/
    [Header("隐藏文本按钮")]
    public GameObject DialogueBox;

    public void ButtonHide()
    {
        //自动结束播放
        if (isAuto)
        {
            ButtonAuto();
        }
        StartCoroutine(HideTextBox());
    }
    IEnumerator HideTextBox()
    {
        DialogueBox.SetActive(false);
        //直到鼠标点击跳出循环
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }
        DialogueBox.SetActive(true);
    }

    /*对话历史区*/
    [Header("显示历史对话按钮")]
    public GameObject historyPanel;
    public Transform historyItems;
    public GameObject itemPref;

    List<HistoryItem> itemsList = new List<HistoryItem>();

    public void ButtonHistory()
    {
        //自动结束播放
        if (isAuto)
        {
            ButtonAuto();
        }

        historyPanel.SetActive(true);

        foreach (HistoryItem item in itemsList)
        {
            if (item != null)
            {
                GameObject newItem = Instantiate(itemPref, historyItems);
                TMP_Text historyLineName = newItem.transform.Find("Name").GetComponent<TMP_Text>();
                TMP_Text historyLineContent = newItem.transform.Find("Content").GetComponent<TMP_Text>();

                historyLineName.text = item.name;
                historyLineContent.text = item.content;
            }
        }

    }
    public void historyUpdate(DialogueLine line)
    {


        HistoryItem historyItem = new HistoryItem();

        if(line.symbol == "O")
        {
            historyItem.name = "（选　项）";
        }
        else if(line.symbol == "W")
        {
            historyItem.name = "【" + line.name + "】";
        }
        else if(line.symbol == "T")
        {
            historyItem.name = "";
        }
        historyItem.content = line.content;

        itemsList.Add(historyItem);
    }

    private void Start()
    {
        currentColor = buttonImage.color;
        dialogueManager = GetComponent<DialogueManager>();
        audioManager = GetComponent<AudioManager>();
        audioSource = GetComponent<AudioSource>();
    }

    [Header("设置按钮")]
    public GameObject settingPanel;

    public void ButtonSetting()
    {
        //自动结束播放
        if (isAuto)
        {
            ButtonAuto();
        }
        settingPanel.SetActive(true);
    }
}
