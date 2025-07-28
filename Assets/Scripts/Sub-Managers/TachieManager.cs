using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TachieManager : MonoBehaviour, ObserverInterface
{
    DialogueManager dialogueManager;

    [Header("对话立绘列表")]
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

                //如果是阴影调节
                if (td.isShadow)
                {
                    dialogueManager.portrait.color = new Color(110f/255f, 110f/255f, 110f/255f, 1);
                }
                else
                {
                    dialogueManager.portrait.color = Color.white;
                }

                //不为空就放立绘
                dialogueManager.portrait.sprite = td.tachie;
                dialogueManager.avatar.sprite = td.tachie;
            }

        }
    }

    //通过对话索引寻找列表中对应立绘
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

    //这个生命周期方法在每次编辑器中更新元素时调用
    private void OnValidate()
    {
        UpdateDialogueIndices();
    }
    //每个List索引都递增对应对话行索引
    void UpdateDialogueIndices()
    {
        for (int i = 0; i < tachieDialogues.Count; i++)
        {
            tachieDialogues[i].dialogueLineIndex = i;
        }
    }
}
