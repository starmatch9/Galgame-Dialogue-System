using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    /*������*/
    //���csv�ļ�
    public TextAsset dataFile;

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

    /*����������*/
    void Start()
    {
        UpdateDialogue(FindCha("���� ����"), "һ����Ҫ����¥��һ����Ҫ����¥��");
    }

    void Update()
    {
        
    }

    /*������*/
    //���¶Ի��������
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
}
