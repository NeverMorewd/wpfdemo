using System;
using System.Windows;
using System.Windows.Media;

namespace Thunisoft.Framework.ExtensionMethod
{
    public static class WindowExtension
    {
        public static void ResolutionAdapt<T>(this Window aBaseWindow,double aWidthOffset = 0, double aHeightOffset = 0) where T : DependencyObject
        {
            if (typeof(T) == typeof(Window))
            {
                aBaseWindow.Height = (aBaseWindow.Height * Application.Current.MainWindow.ActualHeight / 1080) + aHeightOffset;
                aBaseWindow.Width = (aBaseWindow.Width * Application.Current.MainWindow.ActualWidth / 1920) + aWidthOffset;
            }
            else
            {
                T dependencyObject = aBaseWindow.FindChildByType<T>();
                if (dependencyObject is FrameworkElement frameworkElement)
                {
                    frameworkElement.Height = (frameworkElement.Height * Application.Current.MainWindow.ActualHeight / 1080) + aHeightOffset;
                    frameworkElement.Width = (frameworkElement.Width * Application.Current.MainWindow.ActualWidth / 1920) + aWidthOffset;
                }
            }
        }
        public static void ResolutionAdapt(this Window aBaseWindow, string aControlName = null, double aWidthOffset = 0, double aHeightOffset = 0)
        {
            if (string.IsNullOrEmpty(aControlName))
            {
                aBaseWindow.Height = (aBaseWindow.Height * Application.Current.MainWindow.ActualHeight / 1080) + aHeightOffset;
                aBaseWindow.Width = (aBaseWindow.Width * Application.Current.MainWindow.ActualWidth / 1920) + aWidthOffset;
            }
            else
            {
                DependencyObject dependencyObject = aBaseWindow.FindControl(aControlName);
                if (dependencyObject is FrameworkElement frameworkElement)
                {
                    frameworkElement.Height = (frameworkElement.Height * Application.Current.MainWindow.ActualHeight / 1080) + aHeightOffset;
                    frameworkElement.Width = (frameworkElement.Width * Application.Current.MainWindow.ActualWidth / 1920) + aWidthOffset;
                }
            }
        }
        private static FrameworkElement FindControl(this FrameworkElement obj, string aName)
        {
            if (obj.Name == aName)
            {
                return obj;
            }
            var res = VisualTreeHelper.GetChild(obj, 1);
            if (res is FrameworkElement element)
            {
                return FindControl(element, aName);
            }
            return null;
        }
        private static DependencyObject FindControl(this DependencyObject obj, Type aControlType)
        {
            if (obj.DependencyObjectType.SystemType.Name == aControlType.Name)
            {
                return obj;
            }
            int childcount = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childcount; i++)
            {
                return FindControl(VisualTreeHelper.GetChild(obj, i), aControlType);
            }
            return null;
           
        }

        public static T FindChildByName<T>(this DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChildByName<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }
        public static T FindChildByType<T>(this DependencyObject parent) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChildByType<T>(child);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
        public static T FindVisualParent<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindVisualParent<T>(parentObject);
            }
        }
    }
}
