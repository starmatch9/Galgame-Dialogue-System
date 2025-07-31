using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    /*�Զ�������*/
    [Header("�Զ����Ű�ť")]
    public Image buttonImage;
    public Color runningColor;
    Color currentColor;
    //���ڿ���Э��
    [HideInInspector]
    public bool isAuto = false;
    Coroutine autoPlayCoroutine = null;
    AudioSource audioSource = null;
    //Ŀǰֻ�в��Ű�ť�õ��˶Ի�������������ʱ��������
    DialogueManager dialogueManager;
    //��Ҫ��ȡ��Ƶ�������е���Ƶ�б�
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
                //�¼��ص���2018+�汾�ã���������ڲ��Ž���������
                yield return new WaitWhile(() => audioSource.isPlaying);
            }

            //�����ζԻ�����Ƶ�ǿյģ��Ͱ����ֻ�Э����
            if (audioManager.currentClip == null)
            {
                //���Э�������У����ڴ������ѭ��

                float timeAll = 0f; 
                while (dialogueManager.typeTextCoroutine != null)
                {
                    yield return null;
                    timeAll += Time.deltaTime;
                }
                //���ֻ�������ʱͣʱ��
                //Debug.Log(timeAll * 6);
                //���Ķ�ʱ����ֵĳ��ȴ��³�����
                yield return new WaitForSeconds(timeAll * 6);
            }

            //��Ƶ���Ž�����ȴ�ʱ��ĳ���
            yield return new WaitForSeconds(1f);

            dialogueManager.BoxClick();
        }
    }
    
    //Э��ǿ�ƿ�ʼʱ�Ķ���
    public void autoStart()
    {
        if (autoPlayCoroutine != null)
        {
            StopCoroutine(autoPlayCoroutine);
            autoPlayCoroutine = null;
        }
        //�ڿ�ʼЭ��ǰ�������
        autoPlayCoroutine = StartCoroutine(AutoPlay());
    }
    //�����������������ʱ�رգ��ٵ���autoStart�������¿�ʼ������isAuto����true��
    //Ҫ�볹�׹رգ�����ֱ�ӵ���ButtonAuto
    public void autoEnd()
    {
        if (autoPlayCoroutine != null)
        {
            StopCoroutine(autoPlayCoroutine);
            autoPlayCoroutine = null;
        }
    }


    /*�����ı���*/
    [Header("�����ı���ť")]
    public GameObject DialogueBox;

    public void ButtonHide()
    {
        //�Զ���������
        if (isAuto)
        {
            ButtonAuto();
        }
        StartCoroutine(HideTextBox());
    }
    IEnumerator HideTextBox()
    {
        DialogueBox.SetActive(false);
        //ֱ�����������ѭ��
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

    /*�Ի���ʷ��*/
    [Header("��ʾ��ʷ�Ի���ť")]
    public GameObject historyPanel;
    public Transform historyItems;
    public GameObject itemPref;

    List<HistoryItem> itemsList = new List<HistoryItem>();

    public void ButtonHistory()
    {
        //�Զ���������
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
            historyItem.name = "��ѡ���";
        }
        else if(line.symbol == "W")
        {
            historyItem.name = "��" + line.name + "��";
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

    [Header("���ð�ť")]
    public GameObject settingPanel;

    public void ButtonSetting()
    {
        //�Զ���������
        if (isAuto)
        {
            ButtonAuto();
        }
        settingPanel.SetActive(true);
    }
}
