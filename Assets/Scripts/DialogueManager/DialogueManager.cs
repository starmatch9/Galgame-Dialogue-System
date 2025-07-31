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
    /*������*/

    //���csv�ļ�
    public UnityEngine.TextAsset dataFile;

    //�������������UIͼ�����
    public Image portrait;

    //��������ͷ���UIͼ�����
    public Image avatar;

    //�����������Ƶ�TMP�ı����
    public TMP_Text characterName;

    //����Ի����ݵ�TMP�ı����
    public TMP_Text content;

    [Header("���ֻ�Ч������")]
    [Range(0,1)]public float intervalTime = 0.025f;
    [HideInInspector]
    public Coroutine typeTextCoroutine = null;

    [Header("���������б�")]
    //��������б�
    //���ֵ䲻֧�ּ�������ӣ�
    //�������Կ������Ʊ�ʱ���롰��������������
    public List<Character> characters;

    //�Ի����б��ֵ�棩����<-->�Ի�����
    //�Լ���ǰ�Ի�������ֵ��ָ�����еı��
    [HideInInspector]
    public Dictionary<int, DialogueLine> dialogueLines = new Dictionary<int, DialogueLine>();
    [HideInInspector]
    public int dialogueIndex;

    [Header("��֧ѡ��������ťԤ�Ƽ�")]
    //��֧ѡ��
    //Ϊʹ��Instantiate��������Ҫ�ṩ�������Ԥ�Ƽ���Ϸ����
    public Transform parentGroup;
    public GameObject optionPref;

    //һ����������Ӧһ��DifferentSymbol
    DifferentSymbols df;

    //ά���۲����б�
    ObserverInterface[] observers;

    //����һ����ť���������Ի���ʷ�ĸ�����Ҫ�䷽����
    [HideInInspector]
    public ButtonManager buttonManager;


    /*����������*/
    void Awake()
    {
        //���ļ��е��ı����ָ������зָ����������㿪ʼ
        string[] rows = dataFile.text.Split('\n');
        //ÿ�ж�תΪDialogueLine��
        foreach (string row in rows)
        {
            //Ӣ�Ķ��ŷָ�
            string[] cells = row.Split(",");
            //��ʼ��DialogueLine��ͬʱ����������Ӧ���ֵ�    ����<-->�Ի�����
            //���棺Ҫע�ⲻ�ܰѿ��С�������ת����ȥ
            //������int.TryParse����ʶ��ת��Ϊ���ֵ��ַ���������boolֵ
            if (int.TryParse(cells[L_Index], out int result)){
                dialogueLines[result] = new DialogueLine(cells[L_Index], cells[L_Symbol], cells[L_Name], cells[L_Content], cells[L_Jump]);
            }
        }
        dialogueIndex = 0;

        //��ʼ��differentSymbols
        df = new DifferentSymbols(this);

        //��ʼ����ע�ᣩ�۲���
        observers = GetComponents<ObserverInterface>();

        //��ʼ����ť������
        buttonManager = GetComponent<ButtonManager>();
    }

    void Start()
    {
        Advance();
    }

    void Update()
    {
        
    }

    /*������*/
    //ע�⣺ÿ��������Ӧ����
    //���--0  ��־--1  ��������--2  �ı�--3  ��ת���--4  ����--5
    //���������ƽ��Ի�������
    public void Advance()
    {
        DialogueLine line = dialogueLines[dialogueIndex];

        //֪ͨ�۲���
        notify();

        df.DialogueLineAnalysis(line);
    }

    /*֪ͨ�۲��߷���*/
    //����
    void notify()
    {
        //������б���Ҫ����
        foreach (ObserverInterface observer in observers)
        {
            observer.executeUpdate();
        }
    }

    //
    //���֣������ԣ�������һ�����ƺ���ɫ����ı仯���ʺ�ʹ�ù۲���ģʽ
    //

    //ͨ�������ַ���Ѱ���б��ж�Ӧ�������
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

    //ִ�д��ֻ�Ч��
    public void ExecuteTypeText()
    {
        if (typeTextCoroutine != null)
        {
            StopCoroutine(typeTextCoroutine);
            typeTextCoroutine = null;
        }
        typeTextCoroutine = StartCoroutine(TypeText());
    }
    //���ֻ�Ч��
    IEnumerator TypeText()
    {
        //ˢ������
        content.ForceMeshUpdate();
        int total = content.textInfo.characterCount;
        int current = 0;
        while (current <= total)
        {
            content.maxVisibleCharacters = current;
            current++;
            yield return new WaitForSeconds(intervalTime);
        }
        //�ÿ�Э��
        typeTextCoroutine = null;
    }
}
