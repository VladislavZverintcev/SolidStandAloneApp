using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Support.Controls
{
    public class CustomButton : Button
    {
        #region Properties

        #region Icon
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(GeometryGroup), typeof(CustomButton), new PropertyMetadata(null));

        public GeometryGroup Icon
        {
            get { return (GeometryGroup)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconFillProperty =
            DependencyProperty.Register(nameof(IconFill), typeof(SolidColorBrush), typeof(CustomButton), new PropertyMetadata(null));

        public SolidColorBrush IconFill
        {
            get { return (SolidColorBrush)GetValue(IconFillProperty); }
            set { SetValue(IconFillProperty, value); }
        }
        #endregion Icon

        #region Icon stretch
        public static readonly DependencyProperty IconStretchProperty =
           DependencyProperty.Register(nameof(IconStretch), typeof(Stretch), typeof(CustomButton), new PropertyMetadata(null));

        public Stretch IconStretch
        {
            get { return (Stretch)GetValue(IconStretchProperty); }
            set { SetValue(IconStretchProperty, value); }
        }
        #endregion Icon stretch

        #region Icon margin
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register(nameof(IconMargin), typeof(Thickness), typeof(CustomButton), new PropertyMetadata(null));

        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        #endregion Icon margin

        #region Icon visibility
        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register(nameof(IconVisibility), typeof(Visibility), typeof(CustomButton), new PropertyMetadata(default(Visibility)));

        public Visibility IconVisibility
        {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }
        #endregion Icon visibility

        #region Text
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(CustomButton), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion Text

        #region Text visibility
        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register(nameof(TextVisibility), typeof(Visibility), typeof(CustomButton), new PropertyMetadata(default(Visibility)));

        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }
        #endregion Text visibility

        #region Corner radius
        public static readonly DependencyProperty CorRadiusProperty =
            DependencyProperty.Register(nameof(CorRadius), typeof(CornerRadius), typeof(CustomButton), new PropertyMetadata(new CornerRadius(0)));

        public CornerRadius CorRadius
        {
            get { return (CornerRadius)GetValue(CorRadiusProperty); }
            set { SetValue(CorRadiusProperty, value); }

        }
        #endregion Corner radius

        #endregion Properties

        #region Constructors
        static CustomButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomButton), new FrameworkPropertyMetadata(typeof(CustomButton)));
        }
        #endregion Constructors
    }
}
