using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static GlobalContants;

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

    [Header("打字机效果设置")]
    [Range(0,1)]public float intervalTime = 0.025f;
    [HideInInspector]
    public Coroutine typeTextCoroutine = null;

    [Header("出场人物列表")]
    //存放人物列表
    //（字典不支持检查器可视）
    //后续可以考虑在制表时加入“人物索引”的列
    public List<Character> characters;

    //对话行列表（字典版）索引<-->对话行类
    //以及当前对话的索引值，指向表格中的编号
    [HideInInspector]
    public Dictionary<int, DialogueLine> dialogueLines = new Dictionary<int, DialogueLine>();
    [HideInInspector]
    public int dialogueIndex;

    [Header("分支选项父组件及按钮预制件")]
    //分支选项
    //为使用Instantiate方法，需要提供父对象和预制件游戏对象
    public Transform parentGroup;
    public GameObject optionPref;

    //一个管理器对应一个DifferentSymbol
    DifferentSymbols df;

    //维护观察者列表
    ObserverInterface[] observers;

    //连接一个按钮管理器（对话历史的更新需要其方法）
    [HideInInspector]
    public ButtonManager buttonManager;


    /*生命周期区*/
    void Awake()
    {
        //让文件中的文本按分隔符进行分隔，索引从零开始
        string[] rows = dataFile.text.Split('\n');
        //每行都转为DialogueLine类
        foreach (string row in rows)
        {
            //英文逗号分隔
            string[] cells = row.Split(",");
            //初始化DialogueLine的同时加入索引对应的字典    索引<-->对话行类
            //警告：要注意不能把空行、汉字行转化进去
            //方案：int.TryParse可以识别不转化为数字的字符串，返回bool值
            if (int.TryParse(cells[L_Index], out int result)){
                dialogueLines[result] = new DialogueLine(cells[L_Index], cells[L_Symbol], cells[L_Name], cells[L_Content], cells[L_Jump]);
            }
        }
        dialogueIndex = 0;

        //初始化differentSymbols
        df = new DifferentSymbols(this);

        //初始化（注册）观察者
        observers = GetComponents<ObserverInterface>();

        //初始化按钮管理器
        buttonManager = GetComponent<ButtonManager>();
    }

    void Start()
    {
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
        DialogueLine line = dialogueLines[dialogueIndex];

        //通知观察者
        notify();

        df.DialogueLineAnalysis(line);
    }

    /*通知观察者方法*/
    //主题
    void notify()
    {
        //如果是列表，需要遍历
        foreach (ObserverInterface observer in observers)
        {
            observer.executeUpdate();
        }
    }

    //
    //发现（待尝试）：这样一来，似乎角色立绘的变化更适合使用观察者模式
    //

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

    //执行打字机效果
    public void ExecuteTypeText()
    {
        if (typeTextCoroutine != null)
        {
            StopCoroutine(typeTextCoroutine);
            typeTextCoroutine = null;
        }
        typeTextCoroutine = StartCoroutine(TypeText());
    }
    //打字机效果
    IEnumerator TypeText()
    {
        //刷新网格
        content.ForceMeshUpdate();
        int total = content.textInfo.characterCount;
        int current = 0;
        while (current <= total)
        {
            content.maxVisibleCharacters = current;
            current++;
            yield return new WaitForSeconds(intervalTime);
        }
        //置空协程
        typeTextCoroutine = null;
    }
}
