using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Thunisoft.Framework.Bindings;
using Thunisoft.Framework.Commands;

namespace Thunisoft.Demo
{
    /// <summary>
    /// Page_ExpanderListbox.xaml 的交互逻辑
    /// </summary>
    public partial class Page_ExpanderListbox : Page
    {
        public Page_ExpanderListbox()
        {
            InitializeComponent();
            this.DataContext = new ExpanderListboxViewModel();
        }
    }
    public class ExpanderListboxViewModel : TFBindingProperty
    {
        #region case&evidence
        private ObservableCollection<CaseItem> _caseCollection = new ObservableCollection<CaseItem>();
        public ObservableCollection<CaseItem> CaseCollection
        {
            set { SetProperty(ref _caseCollection, value); }
            get { return _caseCollection; }
        }
        private FileItem _EvidenceOnSelected = new FileItem();
        public FileItem EvidenceOnSelected
        {
            set
            {
                if (_EvidenceOnSelected != null)
                {
                    _EvidenceOnSelected.IsSelected = false;
                }
                SetProperty(ref _EvidenceOnSelected, value);
                if (_EvidenceOnSelected != null)
                {
                    _EvidenceOnSelected.IsSelected = true;
                }
            }
            get { return _EvidenceOnSelected; }
        }
        private FileItem _OtherEvidenceOnSelected = new FileItem();
        public FileItem OtherEvidenceOnSelected
        {
            set
            {
                if (_OtherEvidenceOnSelected != null)
                {
                    _OtherEvidenceOnSelected.IsSelected = false;
                }
                SetProperty(ref _OtherEvidenceOnSelected, value);
                if (_OtherEvidenceOnSelected != null)
                {
                    _OtherEvidenceOnSelected.IsSelected = true;
                }
            }
            get { return _OtherEvidenceOnSelected; }
        }
        public TFCommand<ExCommandParameter> ExpandedCommand { get; set; }
        public TFCommand<ExCommandParameter> CollapsedCommand { get; set; }
        public TFCommand<ExCommandParameter> R1Checked { get; set; }
        public TFCommand<ExCommandParameter> R2Checked { get; set; }
        public TFCommand<ExCommandParameter> SelectedCommand { get; set; }

        private ObservableCollection<FileItem> _evidenceOnlyCollection = new ObservableCollection<FileItem>();
        public ObservableCollection<FileItem> EvidenceOnlyCollection
        {
            set { SetProperty(ref _evidenceOnlyCollection, value); }
            get { return _evidenceOnlyCollection; }
        }
        private ObservableCollection<FileItem> _OtherEvidenceOnlyCollection = new ObservableCollection<FileItem>();
        public ObservableCollection<FileItem> OtherEvidenceOnlyCollection
        {
            set { SetProperty(ref _OtherEvidenceOnlyCollection, value); }
            get { return _OtherEvidenceOnlyCollection; }
        }
        #endregion

        #region case&person
        private ObservableCollection<PersonItem> _PersonCollection = new ObservableCollection<PersonItem>();
        public ObservableCollection<PersonItem> PersonCollection
        {
            set { SetProperty(ref _PersonCollection, value); }
            get { return _PersonCollection; }
        }
        #endregion
        List<PersonItem> personItems = new List<PersonItem>
        {
            new PersonItem
            {
                Id = "1",
                Name = "克蕾雅"
            },
            new PersonItem
            {
                Id = "2",
                Name = "特蕾莎"
            },
            new PersonItem
            {
                Id = "3",
                Name = "米利亚"
            },new PersonItem
            {
                Id = "4",
                Name = "奥菲利亚"
            },
            new PersonItem
            {
                Id = "5",
                Name = "温蒂妮"
            },
            new PersonItem
            {
                Id = "6",
                Name = "弗洛拉"
            },
            new PersonItem
            {
                Id = "7",
                Name = "普里西拉"
            },new PersonItem
            {
                Id = "8",
                Name = "伊妮莉"
            },
            new PersonItem
            {
                Id = "9",
                Name = "亚莉斯亚"
            },
            new PersonItem
            {
                Id = "10",
                Name = "比茜"
            },
            new PersonItem
            {
                Id = "11",
                Name = "嘉拉迪雅"
            },
            new PersonItem
            {
                Id = "12",
                Name = "露雪娜"
            },
            new PersonItem
            {
                Id = "13",
                Name = "大力神"
            },
            new PersonItem
            {
                Id = "14",
                Name = "大指挥官"
            },
            new PersonItem
            {
                Id = "15",
                Name = "大大"
            },
            new PersonItem
            {
                Id = "16",
                Name = "11"
            },
            new PersonItem
            {
                Id = "17",
                Name = "1111"
            },
            new PersonItem
            {
                Id = "18",
                Name = "abc"
            },
            new PersonItem
            {
                Id = "19",
                Name = "abcde"
            },
            new PersonItem
            {
                Id = "20",
                Name = "ABCDEFG"
            },
        };
        private ObservableCollection<PersonItem> searchComboxSource = new ObservableCollection<PersonItem>();
        public ObservableCollection<PersonItem> SearchComboxSource
        {
            get
            {
                return searchComboxSource;
            }
            set
            {
                SetProperty(ref searchComboxSource, value);
            }
        }
        public TFCommand<ExCommandParameter> KeyUpSearchCommand { get; set; }
        public TFCommand<ExCommandParameter> PreviewKeyUpCommand { get; set; }
        public TFCommand<ExCommandParameter> KeyDownCommand { get; set; }
        public TFCommand<ExCommandParameter> PreviewKeyDownCommand { get; set; }
        public TFCommand<object> ClearTextCommand { get; set; }
        public TFCommand<ExCommandParameter> SelectionChangedCommand { get; set; }
        public TFCommand<ExCommandParameter> TextInputCommand { get; set; }
        public TFCommand<ExCommandParameter> ComboBoxTextChangedCommand { get; set; }
        private bool _IsDropDownOpen = false;
        public bool IsDropDownOpen
        {
            get { return _IsDropDownOpen; }
            set { SetProperty(ref _IsDropDownOpen, value); }
        }
        private string _SelectText = "";
        public string SelectText
        {
            get { return _SelectText; }
            set { SetProperty(ref _SelectText, value); }
        }
        private PersonItem _ItemOnSelected;
        public PersonItem ItemOnSelected
        {
            get { return _ItemOnSelected; }
            set { SetProperty(ref _ItemOnSelected, value); }
        }
        public ExpanderListboxViewModel()
        {
            #region CaseCollection
            ExpandedCommand = new TFCommand<ExCommandParameter>(ExpandedExecute);
            CollapsedCommand = new TFCommand<ExCommandParameter>(CollapsedExecute);
            SelectedCommand = new TFCommand<ExCommandParameter>(SelectedExecute);
            R1Checked = new TFCommand<ExCommandParameter>(R1CheckedExecute);
            R2Checked = new TFCommand<ExCommandParameter>(R2CheckedExecute);
            var evidences1 = new ObservableCollection<FileItem>
            {
                new FileItem
                {
                     Name = "001-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "002-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "003-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "004-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "005-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "006-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "007-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "008-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "009-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
            };
            var evidences2 = new ObservableCollection<FileItem>
            {
                new FileItem
                {
                     Name = "001-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "002-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "003-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "004-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "005-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "006-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "007-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "008-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "009-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
            };
            var evidences3 = new ObservableCollection<FileItem>
            {
                new FileItem
                {
                     Name = "001-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "002-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "003-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "004-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "005-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "006-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "007-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "008-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "009-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
            };
            var evidences4 = new ObservableCollection<FileItem>
            {
                new FileItem
                {
                     Name = "001-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "002-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "003-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "004-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "005-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "006-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "007-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "008-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "009-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
            };
            var evidences5 = new ObservableCollection<FileItem>
            {
                new FileItem
                {
                     Name = "001-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "002-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "003-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "004-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "005-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "006-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "007-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "008-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "009-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
            };
            var evidences6 = new ObservableCollection<FileItem>
            {
                new FileItem
                {
                     Name = "001-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "002-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "003-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "004-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "005-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "006-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "007-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "008-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
                new FileItem
                {
                     Name = "009-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                },
            };
            CaseCollection = new ObservableCollection<CaseItem>
            {
                new CaseItem
                {
                     CaseName = "哈哈和拉拉的物业纠纷一案01",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = evidences1
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案02",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = evidences2
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案03",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = evidences3
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案04",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = evidences4
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案05",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = evidences5
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案06",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = evidences6
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案07",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案08",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案09",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案10",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案11",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案12",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案13",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案14",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案15",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案16",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案17",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案18",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案19",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案20",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案21",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案22",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案23",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案24",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案25",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案26",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案27",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案28",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案29",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
                new CaseItem
                {
                    CaseName = "哈哈和拉拉的物业纠纷一案30",
                     CaseNo = "（2020）湘民初2020号",
                     EvidenceCollection = new ObservableCollection<FileItem>
                     {
                         new FileItem { Name = "dadasdasdsad",  Title = "原告", Uploader = "wangdong"},
                     }
                },
            };
            EvidenceOnlyCollection = new ObservableCollection<FileItem>
            {
                new FileItem
                {
                     Name = "001-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                     Source = 1
                },
                new FileItem
                {
                     Name = "002-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                     Source = 1
                },
                new FileItem
                {
                     Name = "003-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                     Source = 1
                },
                new FileItem
                {
                     Name = "004-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                     Source = 1
                },
                new FileItem
                {
                     Name = "005-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                     Source = 1
                },
                new FileItem
                {
                     Name = "006-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                     Source = 1
                },
                new FileItem
                {
                     Name = "007-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                     Source = 1
                },
                new FileItem
                {
                     Name = "008-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                     Source = 1
                },
                new FileItem
                {
                     Name = "009-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                     Source = 1
                },
            };
            OtherEvidenceOnlyCollection = new ObservableCollection<FileItem>
            {
                new FileItem
                {
                     Name = "庭前001-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                     Source = 2,
                },
                new FileItem
                {
                     Name = "庭前002-证据文件.doc",
                     Title = "被告",
                     Uploader = "特蕾莎",
                      Source = 2,
                },
            };
            #endregion


            #region PersonCollection
            PersonCollection = new ObservableCollection<PersonItem>
            {
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "11xxxxxxxxxxxxxxxxxx",
                             CaseNo = "11xxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "12xxxxxxxxxxxxxxxxxx",
                             CaseNo = "12xxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "13xxxxxxxxxxxxxxxxxx",
                             CaseNo = "13xxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "14xxxxxxxxxxxxxxxxxx",
                             CaseNo = "14xxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "15xxxxxxxxxxxxxxxxxx",
                             CaseNo = "15xxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "16xxxxxxxxxxxxxxxxxx",
                             CaseNo = "16xxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
                new PersonItem
                {
                     Title = "原告",
                     Name = "hahahah",
                     SubCaseCollection = new ObservableCollection<SubCaseItem>
                     {
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },
                         new SubCaseItem
                         {
                             CaseName = "xxxxxxxxxxxxxxxxxxxx",
                             CaseNo = "xxxxxxxxxxxxxxxx",
                         },

                     },
                },
            };
            #endregion
            SearchComboxSource = new ObservableCollection<PersonItem>(personItems);
            KeyUpSearchCommand = new TFCommand<ExCommandParameter>(KeyUpSearchExecute);
            PreviewKeyUpCommand = new TFCommand<ExCommandParameter>(PreviewKeyUpExecute);
            KeyDownCommand = new TFCommand<ExCommandParameter>(KeyDownExecute);
            PreviewKeyDownCommand = new TFCommand<ExCommandParameter>(PreviewKeyDownExecute);
            ClearTextCommand = new TFCommand<object>(ClearTextExecute);
            SelectionChangedCommand = new TFCommand<ExCommandParameter>(SelectionChangedExecute);
            TextInputCommand = new TFCommand<ExCommandParameter>(TextInputExecute);
            ComboBoxTextChangedCommand = new TFCommand<ExCommandParameter>(ComboBoxTextChangedExecute);

            ItemOnSelected = null;
            SelectText = null;
        }

        private void R2CheckedExecute(ExCommandParameter obj)
        {
            //EvidenceOnlyCollection.Clear();
            //OtherEvidenceOnlyCollection = new ObservableCollection<FileItem>
            //{
            //    new FileItem
            //    {
            //         Name = "庭前001-证据文件.doc",
            //         Title = "被告",
            //         Uploader = "特蕾莎",
            //    },
            //    new FileItem
            //    {
            //         Name = "庭前002-证据文件.doc",
            //         Title = "被告",
            //         Uploader = "特蕾莎",
            //    },
            //};
            EvidenceOnSelected = null;
        }

        private void R1CheckedExecute(ExCommandParameter obj)
        {
            OtherEvidenceOnSelected = null;
            //OtherEvidenceOnlyCollection.Clear();
            //EvidenceOnlyCollection = new ObservableCollection<FileItem>
            //{
            //    new FileItem
            //    {
            //         Name = "001-证据文件.doc",
            //         Title = "被告",
            //         Uploader = "特蕾莎",
            //    },
            //    new FileItem
            //    {
            //         Name = "002-证据文件.doc",
            //         Title = "被告",
            //         Uploader = "特蕾莎",
            //    },
            //    new FileItem
            //    {
            //         Name = "003-证据文件.doc",
            //         Title = "被告",
            //         Uploader = "特蕾莎",
            //    },
            //    new FileItem
            //    {
            //         Name = "004-证据文件.doc",
            //         Title = "被告",
            //         Uploader = "特蕾莎",
            //    },
            //    new FileItem
            //    {
            //         Name = "005-证据文件.doc",
            //         Title = "被告",
            //         Uploader = "特蕾莎",
            //    },
            //    new FileItem
            //    {
            //         Name = "006-证据文件.doc",
            //         Title = "被告",
            //         Uploader = "特蕾莎",
            //    },
            //    new FileItem
            //    {
            //         Name = "007-证据文件.doc",
            //         Title = "被告",
            //         Uploader = "特蕾莎",
            //    },
            //    new FileItem
            //    {
            //         Name = "008-证据文件.doc",
            //         Title = "被告",
            //         Uploader = "特蕾莎",
            //    },
            //    new FileItem
            //    {
            //         Name = "009-证据文件.doc",
            //         Title = "被告",
            //         Uploader = "特蕾莎",
            //    },
            //};
        }

        private void ComboBoxTextChangedExecute(ExCommandParameter obj)
        {

        }

        private void TextInputExecute(ExCommandParameter obj)
        {
            //throw new NotImplementedException();
        }

        private void SelectionChangedExecute(ExCommandParameter obj)
        {
            //IsDropDownOpen = false;
        }

        private void ClearTextExecute(object obj)
        {
            ItemOnSelected = null;
            SelectText = null;
            SearchComboxSource = new ObservableCollection<PersonItem>(personItems);
            IsDropDownOpen = false;
            //throw new NotImplementedException();
        }

        private void PreviewKeyDownExecute(ExCommandParameter obj)
        {
            //throw new NotImplementedException();
        }

        private void KeyDownExecute(ExCommandParameter obj)
        {
            //throw new NotImplementedException();
        }

        private void PreviewKeyUpExecute(ExCommandParameter obj)
        {
            string para = obj.Parameter as string;
        }

        private void KeyUpSearchExecute(ExCommandParameter obj)
        {
            //IsDropDownOpen = true;
            KeyEventArgs keyEventArgs = obj.EventArgs as KeyEventArgs;
            if (keyEventArgs.Key == Key.Down
                || keyEventArgs.Key == Key.Up)
            {
                return;
            }
            if (keyEventArgs.Key == Key.Enter)
            {
                return;
            }
            string para = obj.Parameter as string;
            ItemOnSelected = null;
            //SelectText = para;
            if (!string.IsNullOrEmpty(para))
            {
                SearchComboxSource = new ObservableCollection<PersonItem>(personItems.FindAll(delegate (PersonItem s) { return s.Name.Contains(para?.Trim()); }));
                if (SearchComboxSource.Count > 0)
                {
                    IsDropDownOpen = true;
                }
                //else if (SearchComboxSource.Count == 1)
                //{
                //    if (SearchComboxSource[0].Name == para)
                //    {
                //        IsDropDownOpen = false;
                //    }
                //}
                else
                {
                    SearchComboxSource = new ObservableCollection<PersonItem>(personItems);
                    IsDropDownOpen = false;
                }
            }
            else
            {
                IsDropDownOpen = false;
                SearchComboxSource = new ObservableCollection<PersonItem>(personItems);
            }
        }

        private void SelectedExecute(ExCommandParameter obj)
        {
            //SelectionChangedEventArgs selectionChangedEventArgs = obj.EventArgs as SelectionChangedEventArgs;
            //if (selectionChangedEventArgs.AddedItems.Count == 1 && selectionChangedEventArgs.RemovedItems.Count == 0)
            //{
            //    selectionChangedEventArgs.
            //}
            if (EvidenceOnSelected != null)
            {
                System.Windows.MessageBox.Show(EvidenceOnSelected?.Name);
            }
            if (OtherEvidenceOnSelected != null)
            {
                System.Windows.MessageBox.Show(OtherEvidenceOnSelected?.Name);
            }
        }

        private void CollapsedExecute(ExCommandParameter obj)
        {
            //CaseItem parameter = obj.Parameter as CaseItem;
            //parameter.EvidenceCollection.Clear();
            //parameter.EvidenceCollection.Add(new FileItem
            //{
            //    Name = "999-证据文件.doc",
            //    Title = "原告",
            //    Uploader = "米利亚",
            //});
        }

        private void ExpandedExecute(ExCommandParameter obj)
        {
            //CaseItem parameter = obj.Parameter as CaseItem;
            ////parameter.EvidenceCollection.Clear();
            //parameter.EvidenceCollection.Add(new FileItem
            //{
            //    Name = "999-证据文件.doc",
            //    Title = "原告",
            //    Uploader = "克蕾雅",
            //});
        }
    }
    public class CaseItem : TFBindingProperty
    {
        private ObservableCollection<FileItem> _evidenceCollection = new ObservableCollection<FileItem>();
        public ObservableCollection<FileItem> EvidenceCollection
        {
            set { SetProperty(ref _evidenceCollection, value); }
            get { return _evidenceCollection; }
        }
        public string CaseName { get; set; }
        public string CaseNo { get; set; }
    }
    public class FileItem : TFBindingProperty
    {
        public int AuthorizeStatus { get; set; }//区块链
        public string BasicFileUrl { get; set; }
        public string GroupId { get; set; }//会议id
        public string MemberId { get; set; }//上传人id
        public string Name { get; set; }
        public string OriginalFileUrl { get; set; }
        public string Process { get; set; }
        public int Size { get; set; }
        public int Source { get; set; }// 证据来源   1：当庭上传  2：诉服上传
        public int Stlb { get; set; }//实体文件类别
        public string StorageId { get; set; }
        public string Suffix { get; set; }
        public long Time { get; set; }
        public string Uploader { get; set; }//上传人
        public string Wjbs { get; set; }//文件标识
        public int Wjlx { get; set; }//文件类型
        public string UploadFileId { get; set; }
        public string PdfUrl { get; set; }

        public string Title { get; set; }

        private string _fileNo;
        public string FileNo
        {
            get { return _fileNo; }
            set { SetProperty(ref _fileNo, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        private bool _isCanDelete;
        public bool IsCanDelete
        {
            get { return _isCanDelete; }
            set { SetProperty(ref _isCanDelete, value); }
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
    }

    public class SubCaseItem
    {
        public string CaseName { get; set; }
        public string CaseNo { get; set; }
    }
    public class PersonItem : TFBindingProperty
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public bool CanExpand { get; set; } = false;
        private ObservableCollection<SubCaseItem> _subCaseCollection;
        public ObservableCollection<SubCaseItem> SubCaseCollection
        {
            get { return _subCaseCollection; }
            set { SetProperty(ref _subCaseCollection, value); }
        }
    }
}
