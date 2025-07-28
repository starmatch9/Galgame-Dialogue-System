using UnityEngine;

//该语句可以让自定义类中显示其成员
[System.Serializable]
public class Character
{   
    //人物名称
    public string name = null;

    //人物立绘是否存在
    public bool havePortrait = false;

    //人物好感度
    //（暂不需要）
    //public int love;
}
