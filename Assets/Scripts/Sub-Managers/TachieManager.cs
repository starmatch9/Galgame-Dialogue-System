using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TachieManager : MonoBehaviour, ObserverInterface
{
    DialogueManager dialogueManager;

    [Header("�Ի������б�")]
    public List<TachieDialogue> tachieDialogues;

    void Awake()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }

    public void executeUpdate()
    {
        if(FindTac(dialogueManager.dialogueIndex) != null)
        {
            TachieDialogue td = FindTac(dialogueManager.dialogueIndex);
            if (td.tachie == null)
            {
                dialogueManager.portrait.enabled = false;
                dialogueManager.avatar.enabled = false;
            }
            else
            {
                dialogueManager.portrait.enabled = true;
                dialogueManager.avatar.enabled = true;

                //�������Ӱ����
                if (td.isShadow)
                {
                    dialogueManager.portrait.color = new Color(110f/255f, 110f/255f, 110f/255f, 1);
                }
                else
                {
                    dialogueManager.portrait.color = Color.white;
                }

                //��Ϊ�վͷ�����
                dialogueManager.portrait.sprite = td.tachie;
                dialogueManager.avatar.sprite = td.tachie;
            }

        }
    }

    //ͨ���Ի�����Ѱ���б��ж�Ӧ����
    TachieDialogue FindTac(int index)
    {
        foreach (TachieDialogue tachieDialogue in tachieDialogues)
        {
            if (tachieDialogue.dialogueLineIndex == index)
            {
                return tachieDialogue;
            }
        }
        return null;
    }

    //����������ڷ�����ÿ�α༭���и���Ԫ��ʱ����
    private void OnValidate()
    {
        UpdateDialogueIndices();
    }
    //ÿ��List������������Ӧ�Ի�������
    void UpdateDialogueIndices()
    {
        for (int i = 0; i < tachieDialogues.Count; i++)
        {
            tachieDialogues[i].dialogueLineIndex = i;
        }
    }
}
