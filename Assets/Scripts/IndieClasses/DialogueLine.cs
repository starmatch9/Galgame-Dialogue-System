//�������������csv�����ÿһ�е���Ϣ
//���--0  ��־--1  ��������--2  �ı�--3  ��ת���--4  ����--5
using JetBrains.Annotations;

public class DialogueLine
{
    //���
    public int index = 0;

    //��־
    public string symbol = "";

    //��������
    public string name = "";

    //�ı�
    public string content = "";

    //��ת���
    public int jump = 0;

    //������־�����ܺ�\r��
    //ûɶ�ã�ʡ��


    /*���캯��*/
    //���ڳ�ʼ������
    public DialogueLine(string _index, string _symbol, string _name, string _content, string _jump)
    {
        //ENDʱ��jump�����ǿ�,������һ��
        if (_symbol == "END")
        {
            index = int.Parse(_index);
            symbol = _symbol;
            //������û��
            return;
        }

        index = int.Parse(_index);
        symbol = _symbol;
        name = _name;
        content = _content;
        jump = int.Parse(_jump);
    }
}
