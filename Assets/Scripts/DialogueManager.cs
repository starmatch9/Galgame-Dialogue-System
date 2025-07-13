using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    /*变量区*/
    //存放csv文件
    public TextAsset dataFile;

    //代表人物立绘的UI图像组件
    public Image portrait;

    //代表人物头像的UI图像组件
    public Image avatar;

    //代表人物名称的TMP文本组件
    public TMP_Text characterName;

    //代表对话内容的TMP文本组件
    public TMP_Text content;

    //存放人物列表
    //（字典不支持检查器可视）
    //后续可以考虑在制表时加入“人物索引”的列
    public List<Character> characters;

    /*生命周期区*/
    void Start()
    {
        UpdateDialogue(FindCha("长崎 素世"), "一袋米要扛几楼，一袋米要扛二楼。");
    }

    void Update()
    {
        
    }

    /*方法区*/
    //更新对话框的内容
    public void UpdateDialogue(Character character, string newContent)
    {
        //分别修改头像、立绘（两个源图像一样）、名字、内容
        //当角色没有立绘时，不显示
        if(character.portrait == null)
        {
            portrait.enabled = false;
            avatar.enabled = false;
        }
        else
        {
            portrait.enabled = true;
            avatar.enabled = true;
        }
        //更新立绘
        portrait.sprite = character.portrait;

        //更新头像
        avatar.sprite = character.portrait;

        //更新名字
        characterName.text = "【" + character.name + "】";

        //更新内容
        content.text = "  " + newContent;
    }

    //通过名字字符串寻找列表中对应人物对象
    public Character FindCha(string name)
    {
        foreach (Character character in characters)
        {
            if(character.name == name)
            {
                return character;
            }
        }
        return null;
    }
}
