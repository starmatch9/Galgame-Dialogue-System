using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

//采用观察者模式，在日后加入多个观察者时再加入观察者的接口
public class AudioManager : MonoBehaviour, ObserverInterface
{
    AudioSource audioSource;
    
    //要执行的功能与dialogueManager的某些参数有关呢
    DialogueManager dialogueManager;

    [Header("对话音频列表")]
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


    //通过对话索引寻找列表中对应音频剪辑
    //注意：之后可尝试扩展的DialogueLine类，每个类会根据用户在管理器中的选择而自动加入列表，考虑通过OnValidate方法实现！！！！！！！！！！！！！
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

    //这个生命周期方法在每次编辑器中更新元素时调用
    private void OnValidate()
    {
        UpdateDialogueIndices();
    }
    //每个List索引都递增对应对话行索引
    void UpdateDialogueIndices()
    {
        for (int i = 0; i < audioDialogues.Count; i++)
        {
            audioDialogues[i].dialogueLineIndex = i;
        }
    }

}
