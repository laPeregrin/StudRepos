using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model.FilesForSerialization
{
    enum WorkStatus
    {
        not_choose,
        Workless,
        Freelance,
        Fool_day,
        half_day
    }
    [Serializable]
    public class Person : ISerializable
    {
        #region
        private int _number;
        private string _name;
        private int _age;
        public int number { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        #endregion
        public Person()
        {

        }
        protected internal Person(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            number = serializationInfo.GetInt32("Number");
            name = serializationInfo.GetString("Name");
            age = serializationInfo.GetInt32("Age");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Number", number);
            info.AddValue("Name", name);
            info.AddValue("Age", age);
        }
    }
}
