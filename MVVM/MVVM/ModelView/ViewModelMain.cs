using MVVM.Model.FilesForSerialization;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Input;

namespace MVVM.ModelView
{
    public class ViewModelMain : INotifyPropertyChanged
    {
        #region fields and props
        private string path = @"W:\programs\git_repos\testGitRepos\StudRepos\MVVM\MVVM";
        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(); } }
        #endregion fields and props

        Person _person = new Person()
        {
            age = 22,
            name = "Bob",
            number = 01
        };

        
        public ViewModelMain()
        {

        }



        #region Methods
        public void Serialize(Person person, string path)
        {
            this.path = path;
            using (Stream stream = new FileStream(path + @"text.bin", FileMode.OpenOrCreate, FileAccess.Write))
            {
                IFormatter serialize = new BinaryFormatter();
                serialize.Serialize(stream, person);

            }
        }
        public void DeSerialize()
        {
            using (Stream stream = new FileStream(path + @"text.bin", FileMode.Open, FileAccess.Read))
            {
                Person PersonContainer;
                IFormatter deserialize = new BinaryFormatter();
                PersonContainer = (Person)deserialize.Deserialize(stream);
                StringBuilder line = new StringBuilder();
                line.Append(PersonContainer.number);
                line.Append(PersonContainer.name);
                line.Append(PersonContainer.age);
                Message = line.ToString();
            }
        }
        #endregion Methods
        #region DelegateCommand
        public DelegateCommand _Serialize
        {
            get { return new DelegateCommand((obj) => { Serialize(_person, path); }); }
        }
        public DelegateCommand _DeSerialize
        {
            get { return new DelegateCommand((obj) => { DeSerialize(); }); }
        }
        #endregion
        #region MvvM PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion MvvM PropertyChanged

    }
}






