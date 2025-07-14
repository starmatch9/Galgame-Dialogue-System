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
    Coroutine nowCoroutine = null;

    public void ButtonAuto()
    {
        //Debug.Log(nowCoroutine == null);
        isAuto = true;
        if (nowCoroutine == null)
        {
            nowCoroutine = StartCoroutine(AutoPlay());
        }
    }
    IEnumerator AutoPlay()
    {
        Color originalColor = buttonImage.color;
        buttonImage.color = runningColor;
        //��֧ѡ�����ʱ��Ҳֹͣ�Զ�����
        while (isAuto)
        {
            //ͨ��ѭ������ʱ��Ӷ�
            float timeCal = 0;
            while (timeCal < 3f)
            {
                if (Input.GetMouseButtonDown(0) || !isAuto || dialogueManager.parentGroup.gameObject.activeSelf)
                {
                    //isAuto = false;
                    break;
                }
                timeCal += Time.deltaTime;
                yield return null;
            }
            if (isAuto)
            {
                dialogueManager.BoxClick();
            }
        }
        buttonImage.color = originalColor;
        //Э�̽���ʱ�ÿ�
        nowCoroutine = null;
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
