using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

//采用观察者模式，在日后加入多个观察者时再加入观察者的接口
public class AudioManager : MonoBehaviour
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
