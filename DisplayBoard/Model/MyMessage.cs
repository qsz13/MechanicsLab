using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayBoard.Model
{
    class MyMessage
    {
        public MyMessage(){ }

        public String m_lab = "1号实验室";
        public String m_statu = "进行:";
        public String m_content = "未知名实验";
        public String m_class = "神秘地带";
        public String m_people = "黑衣人";
        public String[] mm = new String[8];
        public void getmm()
        {
            mm[0] = m_lab;
            mm[1] = m_statu;
            mm[2] = m_content;
            mm[3] = m_class;
            mm[4] = m_people;
        }
    }
}
