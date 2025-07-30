using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemHeightControl : MonoBehaviour
{
    float unitHeight = 40f;

    RectTransform rectTransform;

    TextMeshProUGUI historyLineName;
    TextMeshProUGUI historyLineContent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        historyLineName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        historyLineContent = transform.Find("Content").GetComponent<TextMeshProUGUI>();

        //此方法名为“注册布局变化回调”，能够让制定方法在布局改变时调用
        historyLineName.RegisterDirtyLayoutCallback(ChangeHeight);
        historyLineContent.RegisterDirtyLayoutCallback(ChangeHeight);
    }

    void ChangeHeight()
    {
        //强制更新文本布局，确保lineCount准确
        historyLineName.ForceMeshUpdate();
        historyLineContent.ForceMeshUpdate();

        int nameCount = historyLineName.textInfo.lineCount;
        int contentCount = historyLineContent.textInfo.lineCount;

        //找出较大的那一个
        int maxOne = Mathf.Max(nameCount, contentCount);
        //大于1的话更新布局
        if (maxOne >= 1) {
            //注：sizeDelta就是rectTransform相对于锚点的尺寸变化，是一个Vector2
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, maxOne * unitHeight);

        }
    }

}
