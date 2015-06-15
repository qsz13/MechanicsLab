using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuerySystem.Model
{
    class MyMessage
    {
        public MyMessage(){ }

        public String m_lab = "1号实验室";
        public String m_statu = "时段2- 正在进行";
        public String m_content = "实验内容";
        public String m_class = "1100201 材料力学 蒋建华";
        public String m_people = "老师名字";
        public String[] mm = new String[8];
        public void getmm()
        {
            mm[0] = m_lab;
            mm[1] = m_statu;
            mm[2] = m_content;
            mm[3] = m_class;
            mm[4] = m_people;
        }
        public override bool Equals(object obj)
        {

            MyMessage a = obj as MyMessage;
            a.getmm();
            this.getmm();
            for (int i = 0; i < 5; i++)
            {
                if (!mm[0].Equals(a.mm[0]))
                    return false;
            }
            return true;
        }
    }
}
