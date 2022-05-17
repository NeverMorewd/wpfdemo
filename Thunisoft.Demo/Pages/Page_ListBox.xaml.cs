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
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page_ListBox : Page
    {
        sealed class ViewModel
            : INotifyPropertyChanged
        {
            #region INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            void SetField<TX>(ref TX field, TX value, [CallerMemberName] string propertyName = null)
            {
                if (EqualityComparer<TX>.Default.Equals(field, value)) return;
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion

            private readonly List<Case> _caseList;
            private readonly List<string> _selectedCaseId = new List<string>();
            private ObservableCollection<Case> _items;

            public ObservableCollection<Case> Items
            {
                get { return _items; }
                set { SetField(ref _items, value); }
            }
            private ObservableCollection<Case> _resultItems;

            public ObservableCollection<Case> ResultItems
            {
                get { return _resultItems; }
                set { SetField(ref _resultItems, value); }
            }

            Case selectedItem;
            public Case SelectedItem
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
            public TFCommand<ExCommandParameter> SearchComboBoxSelectionChangedCommand { get; set; }
            public TFCommand<ExCommandParameter> CaseCheckedCommand { get; set; }
            public TFCommand<ExCommandParameter> CaseUnCheckedCommand { get; set; }
            public ViewModel()
            {
                Items = new ObservableCollection<Case>(CaseModule.All);
                ResultItems = new ObservableCollection<Case>(CaseModule.All);
                _caseList = Items.ToList();
                SearchComboBoxSelectionChangedCommand = new TFCommand<ExCommandParameter>(SearchComboBoxSelectionChanged);
                CaseCheckedCommand = new TFCommand<ExCommandParameter>(CaseChecked);
                CaseUnCheckedCommand = new TFCommand<ExCommandParameter>(CaseUnChecked);
            }

            private void CaseChecked(ExCommandParameter obj)
            {
                //throw new NotImplementedException();
                _selectedCaseId.Add(obj?.Parameter?.ToString());
            }

            private void CaseUnChecked(ExCommandParameter obj)
            {
                //throw new NotImplementedException();
                _selectedCaseId.Remove(obj?.Parameter?.ToString());
            }

            private void SearchComboBoxSelectionChanged(ExCommandParameter obj)
            {
                if (obj.Parameter is Case aCase)
                {
                    var caselist = _caseList.Where(x => x.CaseName.Equals(aCase.CaseName)).ToList();
                    foreach (var caseitem in caselist)
                    {
                        if (_selectedCaseId.Contains(caseitem.Id))
                        {
                            caseitem.IsSelected = true;
                        }
                    }
                    Items = new ObservableCollection<Case>(caselist);
                }
                else
                {
                    foreach (var caseitem in _caseList)
                    {
                        if (_selectedCaseId.Contains(caseitem.Id))
                        {
                            caseitem.IsSelected = true;
                        }
                    }
                    Items = new ObservableCollection<Case>(_caseList);
                }
            }
        }
        public Page_ListBox()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }
    }
}
