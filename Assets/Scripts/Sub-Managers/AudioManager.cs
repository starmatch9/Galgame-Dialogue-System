using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

//���ù۲���ģʽ�����պ�������۲���ʱ�ټ���۲��ߵĽӿ�
public class AudioManager : MonoBehaviour, ObserverInterface
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
    //ע�⣺֮��ɳ�����չ��DialogueLine�࣬ÿ���������û��ڹ������е�ѡ����Զ������б�����ͨ��OnValidate����ʵ�֣�������������������������
    AudioDialogue FindAud(int index)
    {
        foreach (AudioDialogue audioDialogue in audioDialogues)
        {
            if (audioDialogue.dialogueLineIndex == index && audioDialogue.clip != null)
            {
                return audioDialogue;
            }
        }
        return null;
    }

    //����������ڷ�����ÿ�α༭���и���Ԫ��ʱ����
    private void OnValidate()
    {
        UpdateDialogueIndices();
    }
    //ÿ��List������������Ӧ�Ի�������
    void UpdateDialogueIndices()
    {
        for (int i = 0; i < audioDialogues.Count; i++)
        {
            audioDialogues[i].dialogueLineIndex = i;
        }
    }

}
