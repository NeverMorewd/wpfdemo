using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Thunisoft.Framework.Bindings;

namespace Thunisoft.Demo.Pages
{
    /// <summary>
    /// Interaction logic for Page_TreeView.xaml
    /// </summary>
    public partial class Page_TreeView : Page
    {
        public Page_TreeView()
        {
            InitializeComponent();
            DataContext = new DataViewModel();
        }
    }
    public class DataViewModel : TFBindingProperty
    {
        public ObservableCollection<ItemTreeData> ItemTreeDataList
        {
            get;
            set;
        }
        public DataViewModel()
        {
            ItemTreeDataList = new ObservableCollection<ItemTreeData> 
            {
                new ItemTreeData
                {
                    ItemId=1,
                    ItemName="lala",
                    Children = new ObservableCollection<ItemTreeData>
                    {
                        new ItemTreeData
                        {
                            ItemId=11,
                            ItemName="lala",
                            Children = new ObservableCollection<ItemTreeData>
                            {
                                new ItemTreeData
                                {
                                    ItemId=11,
                                    ItemName="lala",
                                }
                            }
                        } 
                    }
                },
                 new ItemTreeData
                {
                    ItemId=1,
                    ItemName="lala",
                    Children = new ObservableCollection<ItemTreeData>
                    {
                        new ItemTreeData
                        {
                            ItemId=11,
                            ItemName="lala",
                            Children = new ObservableCollection<ItemTreeData>
                            {
                                new ItemTreeData
                                {
                                    ItemId=11,
                                    ItemName="lala",
                                }
                            }
                        }
                    }
                },
                  new ItemTreeData
                {
                    ItemId=1,
                    ItemName="lala",
                    Children = new ObservableCollection<ItemTreeData>
                    {
                        new ItemTreeData
                        {
                            ItemId=11,
                            ItemName="lala",
                            Children = new ObservableCollection<ItemTreeData>
                            {
                                new ItemTreeData
                                {
                                    ItemId=11,
                                    ItemName="lala",
                                }
                            }
                        }
                    }
                }
            };
        }
    }
    public class ItemTreeData: TFBindingProperty
    {
        public int ItemId { get; set; }      // ID
        public string ItemName { get; set; } // 名称
        public int ItemStep { get; set; }    // 所属的层级
        public int ItemParent { get; set; }  // 父级的ID

        private ObservableCollection<ItemTreeData> _children = new ObservableCollection<ItemTreeData>();
        public ObservableCollection<ItemTreeData> Children
        {  
            get
            {
                return _children;
            }
            set
            {
                SetProperty(ref _children, value);
            }
        }

        public bool IsExpanded { get; set; } // 节点是否展开
        public bool IsSelected { get; set; } // 节点是否选中
    }
}
