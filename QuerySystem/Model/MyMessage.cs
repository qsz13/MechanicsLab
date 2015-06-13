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
        private String recode_id = "0";

        public String Recode_id
        {
            get { return recode_id; }
            set { recode_id = value; }
        }
        private String date = "1999-08-01";

        public String Date
        {
            get { return date; }
            set { date = value; }
        }
        private String time = "1,2节课程";

        public String Time
        {
            get { return time; }
            set { time = value; }
        }

        private String m_lab = "本部115";

        public String Lab
        {
            get { return m_lab; }
            set { m_lab = value; }
        }
        private String m_content = "拉伸与收缩实验";

        public String Content
        {
            get { return m_content; }
            set { m_content = value; }
        }
        private String pass_statu = "已通过";

        public String Pass_statu
        {
            get { return pass_statu; }
            set { pass_statu = value; }
        }

        public String m_statu = "时段2- 正在进行";
        
        public String m_class = "1100201 材料力学 蒋建华";
        public String m_people = "老师名字";
        public String[] mm = new String[8];
        public void getmm()
        {
            mm[0] = Lab;
            mm[1] = m_statu;
            mm[2] = Content;
            mm[3] = m_class;
            mm[4] = m_people;
        }

    }
}
