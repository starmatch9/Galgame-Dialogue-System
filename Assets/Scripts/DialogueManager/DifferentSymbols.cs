//策略接口
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

interface DialogueUpdate
{
    public void LineUpdate(DialogueLine line, DialogueManager manager);
}

//上下文类
public class DifferentSymbols
{
    DialogueManager manager = null;

    //C#声明接口时，get和set表示外部可读可写该属性
    DialogueUpdate DialogueUpdate { get; set; }

    //字典类，symbol对应一个DifferentSymbol脚本中的DialogueUpdate类
    Dictionary<string, DialogueUpdate> differentSymbolAnalysis =
        new Dictionary<string, DialogueUpdate>
        {
            {"W", new UpdateW()},
            {"T", new UpdateT()},
            {"O", new UpdateO()},
            {"END", new UpdateEND()}
        };

    //用于执行的方法
    public void DialogueLineAnalysis(DialogueLine line)
    {
        DialogueUpdate = differentSymbolAnalysis[line.symbol];
        DialogueUpdate.LineUpdate(line, manager);
    }

    /*构造函数*/
    //一个DifferentSymbols绑定一个manager类
    public DifferentSymbols(DialogueManager _manager) {
        //让我们的manager指向一个DialogueManager类
        manager = _manager;
    }
}

/*具体策略*/
public class UpdateW : DialogueUpdate
{
    public void LineUpdate(DialogueLine line, DialogueManager manager)
    {
        Upd(manager.FindCha(line.name), line.content, manager);
        manager.dialogueIndex = line.jump;
        //将打字机效果附加在TMP_Text上面
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
        manager.characterName.text = "【" + character.name + "】";
        manager.content.text = "  " + newContent;
    }
}

public class UpdateT : DialogueUpdate
{
    public void LineUpdate(DialogueLine line, DialogueManager manager)
    {
        Upd(line.content, manager);
        manager.dialogueIndex = line.jump;
        //将打字机效果附加在TMP_Text上面
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
        //如果类不继承于MonoBehavior，则使用UnityEngine.Object.Instantiate
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
