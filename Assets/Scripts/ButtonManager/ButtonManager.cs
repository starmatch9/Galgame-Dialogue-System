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
    bool isAuto = false;

    public void ButtonAuto()
    {
        isAuto = !isAuto;
        if (isAuto)
        {
            StartCoroutine(AutoPlay());
        }
    }
    IEnumerator AutoPlay()
    {
        DialogueManager dialogueManager = GetComponent<DialogueManager>();

        Color originalColor = buttonImage.color;
        buttonImage.color = runningColor;
        //��֧ѡ�����ʱ��Ҳֹͣ�Զ�����
        bool isContinue = true;
        while (isContinue)
        {
            //ͨ��ѭ������ʱ��Ӷ�
            float timeCal = 0;
            while (timeCal < 3f)
            {
                if (Input.GetMouseButtonDown(0) || !isAuto || dialogueManager.parentGroup.gameObject.activeSelf)
                {
                    isContinue = false;
                    break;
                }
                timeCal += Time.deltaTime;
                yield return null;
            }
            if (isContinue)
            {
                dialogueManager.BoxClick();
            }
        }
        buttonImage.color = originalColor;
    }

    /*�����ı���*/
    [Header("�����ı���ť")]
    public GameObject DialogueBox;

    public void ButtonHide()
    {
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


}
