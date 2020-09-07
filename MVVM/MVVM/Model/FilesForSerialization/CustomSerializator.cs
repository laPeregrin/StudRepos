using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace MVVM.Model.FilesForSerialization
{
    public class ContainerForSerializator
    {
        private List<string> _infoForSerialContainer;
        private List<string> _infoForSerialValueContainer;
        public List<string> infoForSerialContainer { get { return _infoForSerialContainer; } set { _infoForSerialContainer = value; } }
        public List<string> infoForSerialValueContainer { get { return _infoForSerialValueContainer; } set { _infoForSerialValueContainer = value; } }
    }
    public class CustomSerializator : IFormatter
    {
        ContainerForSerializator _ContainerForSerializator;
        public CustomSerializator()
        {
            _ContainerForSerializator = new ContainerForSerializator();
        }
        private string _path;
        private List<string> _infoForSerial;
        private List<string> _infoForSerialValue;
        public List<string> infoForSerial { get { return _infoForSerial; } set { _infoForSerial = value; } }
        public List<string> infoForSerialValue { get { return _infoForSerialValue; } set { _infoForSerialValue = value; } }
        public string path { get { return _path; } set { _path = value; } }
        Type type;
        public SerializationBinder Binder { get; set; }
        public StreamingContext Context { get; set; }
        public ISurrogateSelector SurrogateSelector { get; set; }

        public CustomSerializator(Type type)
        {
            this.type = type;
        }
        public void Serialize(Stream serializationStream, object graph)
        {
            List<PropertyInfo> propertyInfo = type.GetProperties().ToList();
            StreamWriter streamWriter = new StreamWriter(serializationStream);
            foreach (PropertyInfo info in propertyInfo)
            {
                infoForSerial.Add(info.Name);
                infoForSerialValue.Add(info.GetValue(graph).ToString());
            }
            _ContainerForSerializator.infoForSerialContainer = infoForSerial;
            _ContainerForSerializator.infoForSerialValueContainer = infoForSerialValue;
            streamWriter.Flush();


        }
        public object Deserialize(Stream serializationStream)
        {
            throw new NotImplementedException();
        }
    }
}
