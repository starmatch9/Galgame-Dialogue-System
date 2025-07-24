using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public DialogueManager dialogueManager;

    /*自动播放区*/
    [Header("自动播放按钮")]
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
        //分支选项出现时，也停止自动播放
        bool isContinue = true;
        while (isContinue)
        {
            //通过循环叠加时间从而
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

    /*隐藏文本区*/
    [Header("隐藏文本按钮")]
    public GameObject DialogueBox;

    public void ButtonHide()
    {
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
}
