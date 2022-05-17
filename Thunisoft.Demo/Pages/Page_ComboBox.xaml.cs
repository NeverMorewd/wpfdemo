using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Thunisoft.Demo.Data;
using Thunisoft.Framework.Commands;

namespace Thunisoft.Demo
{
    /// <summary>
    /// Page_Combox.xaml 的交互逻辑
    /// </summary>
    public partial class Page_ComboBox : Page
    {
        sealed class ViewModel
            : INotifyPropertyChanged
        {
            #region INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            void SetField<X>(ref X field, X value, [CallerMemberName] string propertyName = null)
            {
                if (EqualityComparer<X>.Default.Equals(field, value)) return;

                field = value;

                var h = PropertyChanged;
                if (h != null) h(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion

            public TFCommand<ExCommandParameter> SearchComboBoxSelectionChangedCommand { get; set; }
            public List<Person> Items
            {
                get 
                { 
                    var sorted =  PersonModule.All;
                    var res = sorted.ToList();
                    res.Sort();
                    return res;
                }
            }
            public IReadOnlyList<Person> AllResultItems
            {
                get { return PersonModuleResult.All; }
            }
            private ObservableCollection<Person> resultItem;

            public ObservableCollection<Person> ResultItem
            {
                get { return resultItem; }
                set { SetField(ref resultItem, value); }
            }

            Person selectedItem;
            public Person SelectedItem
            {
                get { return selectedItem; }
                set { SetField(ref selectedItem, value); }
            }

            long? selectedValue;
            public long? SelectedValue
            {
                get { return selectedValue; }
                set { SetField(ref selectedValue, value); }
            }
            public ViewModel()
            {
                ResultItem = new ObservableCollection<Person>(AllResultItems);
                SearchComboBoxSelectionChangedCommand = new TFCommand<ExCommandParameter>(SearchComboBoxSelectionChanged);
            }

            private void SearchComboBoxSelectionChanged(ExCommandParameter obj)
            {
                Person person = obj.Parameter as Person;
                if(person!=null)
                {
                    ResultItem = new ObservableCollection<Person>(AllResultItems.Where(x => x.Name.Equals(person.Name)).ToList());
                }
                else
                {
                    ResultItem = new ObservableCollection<Person>(AllResultItems);
                }
            }
        }
        public Page_ComboBox()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }
    }
}
