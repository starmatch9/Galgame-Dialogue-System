using UnityEngine;

//该语句可以让自定义类中显示其成员
[System.Serializable]
public class Character
{   
    //人物名称
    public string name = null;

    //人物立绘
    //（后续可以更换为立绘列表，制作差分效果，可能需要制表时加入“立绘索引”列）
    public Sprite portrait = null;

    //人物好感度
    //（暂不需要）
    //public int love;
}
