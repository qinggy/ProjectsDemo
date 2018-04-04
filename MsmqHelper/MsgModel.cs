using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsmqHelper
{
    [Serializable]
    public class MsgModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public MsgModel() { }
        public MsgModel(string _id, string _name)
        {
            Id = _id;
            Name = _name;
        }
    }
}
