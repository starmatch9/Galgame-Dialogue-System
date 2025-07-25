using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

//���ù۲���ģʽ�����պ�������۲���ʱ�ټ���۲��ߵĽӿ�
public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    
    //Ҫִ�еĹ�����dialogueManager��ĳЩ�����й���
    DialogueManager dialogueManager;

    [Header("�Ի���Ƶ�б�")]
    public List<AudioDialogue> audioDialogues;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        dialogueManager = GetComponent<DialogueManager>();
    }

    public void executeUpdate()
    {
        if(FindAud(dialogueManager.dialogueIndex) != null)
        {
            audioSource.clip = FindAud(dialogueManager.dialogueIndex).clip;
            audioSource.Play();
        }
    }


    //ͨ���Ի�����Ѱ���б��ж�Ӧ��Ƶ����
    public AudioDialogue FindAud(int index)
    {
        foreach (AudioDialogue audioDialogue in audioDialogues)
        {
            if (audioDialogue.dialogueLineIndex == index)
            {
                return audioDialogue;
            }
        }
        return null;
    }

}
