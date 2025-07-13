using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

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

    //��������б�
    //���ֵ䲻֧�ּ�������ӣ�
    //�������Կ������Ʊ�ʱ���롰��������������
    public List<Character> characters;

    //�Ի����б�
    //�Լ���ǰ�Ի�������ֵ��ָ�����еı��
    string[] rows;
    int dialogueIndex;

    /*����������*/

    void Awake()
    {
        //���ļ��е��ı����ָ������зָ����������㿪ʼ
        rows = dataFile.text.Split('\n');
        dialogueIndex = 0;
    }

    void Start()
    {
        //UpdateDialogue(FindCha("���� ����"), "һ����Ҫ����¥��һ����Ҫ����¥��");
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
        //���ҵ�rows��ƥ������ֵ����
        foreach(var row in rows)
        {
            //Ӣ�Ķ��ŷָ�
            string[] cells = row.Split(",");
            //���ݱ�־�Ĳ�ͬ�����ò�ͬ�ķ���
            //�����Կ���Ϊ���˹��ĶԻ�������־�������չ��
            //������ʶ���־����ֹת���������ַ�
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

    //��ǰΪ���﷢��ʱ
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
        characterName.text = "��" + character.name + "��";
        content.text = "  " + newContent;
    }

    //��ǰΪ����ʱ
    public void UpdateT(string newContent)
    {
        portrait.enabled = false;
        avatar.enabled = false;
        characterName.text = "";
        content.text = "  " + newContent;
    }

    //��ǰΪ��֧ѡ��ʱ
    public void UpdateO()
    {

    }

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


    /*    //���¶Ի��������
    public void UpdateDialogue(Character character, string newContent)
    {
        //�ֱ��޸�ͷ�����棨����Դͼ��һ���������֡�����
        //����ɫû������ʱ������ʾ
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
        //��������
        portrait.sprite = character.portrait;

        //����ͷ��
        avatar.sprite = character.portrait;

        //��������
        characterName.text = "��" + character.name + "��";

        //��������
        content.text = "  " + newContent;
    }*/
}
