//���Խӿ�
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

interface DialogueUpdate
{
    public void LineUpdate(DialogueLine line, DialogueManager manager);
}

//��������
public class DifferentSymbols
{
    DialogueManager manager = null;

    //C#�����ӿ�ʱ��get��set��ʾ�ⲿ�ɶ���д������
    DialogueUpdate DialogueUpdate { get; set; }

    //�ֵ��࣬symbol��Ӧһ��DifferentSymbol�ű��е�DialogueUpdate��
    Dictionary<string, DialogueUpdate> differentSymbolAnalysis =
        new Dictionary<string, DialogueUpdate>
        {
            {"W", new UpdateW()},
            {"T", new UpdateT()},
            {"O", new UpdateO()},
            {"END", new UpdateEND()}
        };

    //����ִ�еķ���
    public void DialogueLineAnalysis(DialogueLine line)
    {
        DialogueUpdate = differentSymbolAnalysis[line.symbol];
        DialogueUpdate.LineUpdate(line, manager);
    }

    /*���캯��*/
    //һ��DifferentSymbols��һ��manager��
    public DifferentSymbols(DialogueManager _manager) {
        //�����ǵ�managerָ��һ��DialogueManager��
        manager = _manager;
    }
}

/*�������*/
public class UpdateW : DialogueUpdate
{
    public void LineUpdate(DialogueLine line, DialogueManager manager)
    {
        Upd(manager.FindCha(line.name), line.content, manager);
        manager.dialogueIndex = line.jump;
        //�����ֻ�Ч��������TMP_Text����
        manager.ExecuteTypeText();
    }

    void Upd(Character character, string newContent, DialogueManager manager)
    {
        if (!character.havePortrait)
        {
            manager.avatar.enabled = false;
        }
        else
        {
            manager.avatar.enabled = true;
        }
        manager.characterName.text = "��" + character.name + "��";
        manager.content.text = "  " + newContent;
    }
}

public class UpdateT : DialogueUpdate
{
    public void LineUpdate(DialogueLine line, DialogueManager manager)
    {
        Upd(line.content, manager);
        manager.dialogueIndex = line.jump;
        //�����ֻ�Ч��������TMP_Text����
        manager.ExecuteTypeText();
    }

    void Upd(string newContent, DialogueManager manager)
    {
        manager.avatar.enabled = false;
        manager.characterName.text = "";
        manager.content.text = "  " + newContent;
    }
}

public class UpdateO : DialogueUpdate
{
    public void LineUpdate(DialogueLine line, DialogueManager manager)
    {
        manager.parentGroup.gameObject.SetActive(true);
        Upd(manager.dialogueIndex, manager);
    }

    void Upd(int index, DialogueManager manager)
    {
        DialogueLine line = manager.dialogueLines[index];
        if (line.symbol != "O")
        {
            return;
        }
        //����಻�̳���MonoBehavior����ʹ��UnityEngine.Object.Instantiate
        GameObject option = UnityEngine.Object.Instantiate(manager.optionPref, manager.parentGroup);
        option.GetComponentInChildren<TMP_Text>().text = line.content;
        option.GetComponent<Button>().onClick.AddListener(
            delegate { OptionJump(line.jump, manager); }
            );
        Upd(index + 1, manager);
    }

    void OptionJump(int target, DialogueManager manager)
    {
        manager.parentGroup.gameObject.SetActive(false);
        manager.dialogueIndex = target;
        manager.Advance();
    }
}

public class UpdateEND : DialogueUpdate
{
    public void LineUpdate(DialogueLine line, DialogueManager manager)
    {
        Application.Quit();
    }
}
