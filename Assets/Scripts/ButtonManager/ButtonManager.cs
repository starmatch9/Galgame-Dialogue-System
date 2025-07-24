using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public DialogueManager dialogueManager;

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
}
