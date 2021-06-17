using System;
using System.Runtime.Serialization;

namespace OPUSERP.VMS.Models
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(string label, double y,string textHead, string textFoot,int? id)
        {
            this.Label = label;
            this.Y = y;
            this.TextHead = textHead;
            this.TextFoot = textFoot;
            this.Id = id;
        }
        [DataMember(Name = "label")]
        public string Label = "";
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;

        [DataMember(Name = "textHead")]
        public string TextHead = "";

        [DataMember(Name = "textFoot")]
        public string TextFoot = "";

        [DataMember(Name = "id")]
        public int? Id = null;
    }
}