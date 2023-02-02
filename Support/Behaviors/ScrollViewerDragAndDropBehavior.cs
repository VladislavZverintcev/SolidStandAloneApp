using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Support.Behaviors
{
    public static class ScrollViewerDragAndDropBehavior
    {
        public static bool GetIsScrollOnDragOverEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(IsScrollOnDragOverEnabledProperty);
        }

        public static void SetIsScrollOnDragOverEnabled(DependencyObject element, bool value)
        {
            element.SetValue(IsScrollOnDragOverEnabledProperty, value);
        }

        public static readonly DependencyProperty IsScrollOnDragOverEnabledProperty =
            DependencyProperty.RegisterAttached("IsScrollOnDragOverEnabled", 
            typeof(bool),
            typeof(ScrollViewerDragAndDropBehavior),
            new FrameworkPropertyMetadata(false, OnIsScrollOnDragOverPropertyChanged));

        private static void OnIsScrollOnDragOverPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dropTarget = d as FrameworkElement;

            if (dropTarget == null) return;

            if ((bool)e.OldValue)
            {
                dropTarget.PreviewDragOver -= OnPreviewDragOver;
            }

            if ((bool)e.NewValue)
            {
                dropTarget.PreviewDragOver += OnPreviewDragOver;
            }
        }

        private static DateTime _lastPreviewDragOverEvent = DateTime.MinValue;

        private static void OnPreviewDragOver(object sender, DragEventArgs e)
        {
            if (DateTime.Now.Subtract(_lastPreviewDragOverEvent).Milliseconds >= 250)
            {
                _lastPreviewDragOverEvent = DateTime.Now;
                var dropTarget = sender as FrameworkElement;

                if (dropTarget == null)
                {
                    return;
                }

                var scrollViewer = FindChild<ScrollViewer>(dropTarget);

                if (scrollViewer == null)
                {
                    return;
                }

                const double tolerance = 20;

                const double yOffset = 5;
                const double xOffset = 5;

                var yPos = e.GetPosition(dropTarget).Y;
                var xPos = e.GetPosition(dropTarget).X;


                if (yPos < tolerance)
                {
                    SmoothVerticalScroll(scrollViewer, -yOffset);
                }
                else if (yPos > dropTarget.ActualHeight - tolerance)
                {
                    SmoothVerticalScroll(scrollViewer, yOffset);
                }

                if (xPos < tolerance)
                {
                    SmoothHorizontalScroll(scrollViewer, -xOffset);
                }
                else if (xPos > dropTarget.ActualWidth - tolerance)
                {
                    SmoothHorizontalScroll(scrollViewer, xOffset);
                }
            }
        }

        private static void SmoothVerticalScroll(ScrollViewer scrollviewer, double offset)
        {

            if (offset > 0)
            {
                for (var i = 0; i < offset; i++)
                {
                    scrollviewer.ScrollToVerticalOffset(scrollviewer.VerticalOffset + 1);
                }
            }
            else
            {
                for (var i = 0; i < Math.Abs(offset); i++)
                {
                    scrollviewer.ScrollToVerticalOffset(scrollviewer.VerticalOffset - 1);
                }
            }
        }

        private static void SmoothHorizontalScroll(ScrollViewer scrollviewer, double offset)
        {
            if (offset > 0)
            {
                for (var i = 0; i < offset; i++)
                {
                    scrollviewer.ScrollToHorizontalOffset(scrollviewer.HorizontalOffset + 1);
                }
            }
            else
            {
                for (var i = 0; i < Math.Abs(offset); i++)
                {
                    scrollviewer.ScrollToHorizontalOffset(scrollviewer.HorizontalOffset - 1);
                }
            }
        }

        public static T FindChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, 0);
                if (child != null && child is T)
                {
                    return (T)child;
                }

                var childItem = FindChild<T>(child);
                if (childItem != null)
                {
                    return childItem;
                }
            }
            return null;
        }
    }
}
