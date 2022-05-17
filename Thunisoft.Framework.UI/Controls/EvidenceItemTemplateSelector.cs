using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Windows;

namespace Thunisoft.Framework.UI.Controls
{
    public class EvidenceItemTemplateSelector : System.Windows.Controls.DataTemplateSelector
    {
        public DataTemplate CaseEvidenceItemTemplate
        {
            get;
            set;
        }

        public DataTemplate EvidenceItemTemplate
        {
            get;
            set;
        }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            bool ifContainsCollection = false;
            PropertyInfo[] propertyInfos = item.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //if (propertyInfo.GetValue(item) is IEnumerable)
                if (propertyInfo.GetValue(item) is INotifyCollectionChanged)
                {
                    ifContainsCollection = true;
                    break;
                }
            }
            //ifContainsCollection = item.GetType().GetProperty("IsSeries").GetValue(item).Equals(true);
            if (ifContainsCollection)
            {
                return CaseEvidenceItemTemplate;
            }
            else
            {
                return EvidenceItemTemplate;
            }
        }
    }
}
