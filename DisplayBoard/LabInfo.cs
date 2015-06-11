using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DisplayBoard
{

    [DataContract(Name = "lab", Namespace = "")]
    class LabInfo
    {
        [DataMember(Name = "id")]
        public String Id { get; set; }

        [DataMember(Name = "text")]
        public String Text { get; set; }

        [DataMember(Name = "created_at")]
        public String Date { get; set; }

        [DataMember(Name = "name")]
        public String User { get; set; }


    }
}
