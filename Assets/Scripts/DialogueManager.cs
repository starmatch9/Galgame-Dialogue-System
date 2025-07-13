using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    /*变量区*/

    //存放csv文件
    public UnityEngine.TextAsset dataFile;

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

    //对话行列表
    //以及当前对话的索引值，指向表格中的编号
    string[] rows;
    int dialogueIndex;

    /*生命周期区*/

    void Awake()
    {
        //让文件中的文本按分隔符进行分隔，索引从零开始
        rows = dataFile.text.Split('\n');
        dialogueIndex = 0;
    }

    void Start()
    {
        //UpdateDialogue(FindCha("长崎 素世"), "一袋米要扛几楼，一袋米要扛二楼。");
        Advance();
    }

    void Update()
    {
        
    }

    /*方法区*/

    //注意：每行索引对应意义
    //编号--0  标志--1  人物名称--2  文本--3  跳转编号--4  结束--5

    //根据索引推进对话的内容
    public void Advance()
    {
        //先找到rows中匹配索引值的行
        foreach(var row in rows)
        {
            //英文逗号分隔
            string[] cells = row.Split(",");
            //根据标志的不同，采用不同的方法
            //（可以考虑为主人公的对话新增标志，灵活扩展）
            //在这里识别标志，防止转化非数字字符
            if (cells[1] == "W" && int.Parse(cells[0]) == dialogueIndex)
            {
                Debug.Log(row);
                Debug.Log(cells[2]);
                UpdateW(FindCha(cells[2]), cells[3]);
                dialogueIndex = int.Parse(cells[4]);

                break;
            }
            else if (cells[1] == "T" && int.Parse(cells[0]) == dialogueIndex)
            {
                UpdateT(cells[3]);
                dialogueIndex = int.Parse(cells[4]);

                break;
            }
            else if (cells[1] == "O" && int.Parse(cells[0]) == dialogueIndex)
            {

                break;
            }
        }
    }

    //当前为人物发言时
    public void UpdateW(Character character, string newContent)
    {
        Debug.Log(character.name);
        if (character.portrait == null)
        {
            portrait.enabled = false;
            avatar.enabled = false;
        }
        else
        {
            portrait.enabled = true;
            avatar.enabled = true;
        }


        portrait.sprite = character.portrait;
        avatar.sprite = character.portrait;
        characterName.text = "【" + character.name + "】";
        content.text = "  " + newContent;
    }

    //当前为心理活动时
    public void UpdateT(string newContent)
    {
        portrait.enabled = false;
        avatar.enabled = false;
        characterName.text = "";
        content.text = "  " + newContent;
    }

    //当前为分支选项时
    public void UpdateO()
    {

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

    public void BoxClick()
    {
        Advance();
    }


    /*    //更新对话框的内容
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
    }*/
}
