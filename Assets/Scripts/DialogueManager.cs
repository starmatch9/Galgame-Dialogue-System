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
        
    }

    void Update()
    {
        
    }

    /*方法区*/


}
