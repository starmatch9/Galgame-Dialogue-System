using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalContants
{
    //数据行line，以L开头
    //编号--0  标志--1  人物名称--2  文本--3  跳转编号--4  结束--5
    //这这些对应为常量，以便于后续扩展
    public const int L_Index = 0;
    public const int L_Symbol = 1;
    public const int L_Name = 2;
    public const int L_Content = 3;
    public const int L_Jump = 4;
    public const int L_Final = 5;
}
