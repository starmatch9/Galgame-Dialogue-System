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

        //�˷�����Ϊ��ע�᲼�ֱ仯�ص������ܹ����ƶ������ڲ��ָı�ʱ����
        historyLineName.RegisterDirtyLayoutCallback(ChangeHeight);
        historyLineContent.RegisterDirtyLayoutCallback(ChangeHeight);
    }

    void ChangeHeight()
    {
        //ǿ�Ƹ����ı����֣�ȷ��lineCount׼ȷ
        historyLineName.ForceMeshUpdate();
        historyLineContent.ForceMeshUpdate();

        int nameCount = historyLineName.textInfo.lineCount;
        int contentCount = historyLineContent.textInfo.lineCount;

        //�ҳ��ϴ����һ��
        int maxOne = Mathf.Max(nameCount, contentCount);
        //����1�Ļ����²���
        if (maxOne >= 1) {
            //ע��sizeDelta����rectTransform�����ê��ĳߴ�仯����һ��Vector2
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, maxOne * unitHeight);

        }
    }

}
