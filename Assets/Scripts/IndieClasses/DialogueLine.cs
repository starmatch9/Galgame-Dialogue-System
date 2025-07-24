//这个类用来储存csv表格中每一列的信息
//编号--0  标志--1  人物名称--2  文本--3  跳转编号--4  结束--5
using JetBrains.Annotations;

public class DialogueLine
{
    //编号
    public int index = 0;

    //标志
    public string symbol = "";

    //人物名称
    public string name = "";

    //文本
    public string content = "";

    //跳转编号
    public int jump = 0;

    //结束标志（可能含\r）
    //没啥用，省略


    /*构造函数*/
    //用于初始化数据
    public DialogueLine(string _index, string _symbol, string _name, string _content, string _jump)
    {
        //END时，jump可能是空,得区别一下
        if (_symbol == "END")
        {
            index = int.Parse(_index);
            symbol = _symbol;
            //其他都没有
            return;
        }

        index = int.Parse(_index);
        symbol = _symbol;
        name = _name;
        content = _content;
        jump = int.Parse(_jump);
    }
}
