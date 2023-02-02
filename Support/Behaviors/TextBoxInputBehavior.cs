using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows;

namespace Support.Behaviors
{
    public class TextBoxInputBehavior : Behavior<TextBox>
    {
        const NumberStyles validNumberStyles = NumberStyles.AllowDecimalPoint |
                                                   NumberStyles.AllowThousands |
                                                   NumberStyles.AllowLeadingSign;
        public TextBoxInputBehavior()
        {
            this.InputMode = TextBoxInputMode.None;
            this.JustPositivDecimalInput = false;
        }

        public TextBoxInputMode InputMode { get; set; }


        public static readonly DependencyProperty JustPositivDecimalInputProperty =
            DependencyProperty.Register("JustPositivDecimalInput", typeof(bool),
            typeof(TextBoxInputBehavior), new FrameworkPropertyMetadata(false));

        public bool JustPositivDecimalInput
        {
            get { return (bool)GetValue(JustPositivDecimalInputProperty); }
            set { SetValue(JustPositivDecimalInputProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown += AssociatedObjectPreviewKeyDown;

            DataObject.AddPastingHandler(AssociatedObject, Pasting);

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown -= AssociatedObjectPreviewKeyDown;

            DataObject.RemovePastingHandler(AssociatedObject, Pasting);
        }

        private void Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var pastedText = (string)e.DataObject.GetData(typeof(string));

                if (!this.IsValidInput(this.GetText(pastedText)))
                {
                    System.Media.SystemSounds.Beep.Play();
                    e.CancelCommand();
                }
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
                e.CancelCommand();
            }
        }

        private void AssociatedObjectPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (!this.IsValidInput(this.GetText(" ")))
                {
                    System.Media.SystemSounds.Beep.Play();
                    e.Handled = true;
                }
            }
        }

        private void AssociatedObjectPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!this.IsValidInput(this.GetText(e.Text)))
            {
                System.Media.SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private string GetText(string input)
        {
            var txt = this.AssociatedObject;

            int selectionStart = txt.SelectionStart;
            if (txt.Text.Length < selectionStart)
                selectionStart = txt.Text.Length;

            int selectionLength = txt.SelectionLength;
            if (txt.Text.Length < selectionStart + selectionLength)
                selectionLength = txt.Text.Length - selectionStart;

            var realtext = txt.Text.Remove(selectionStart, selectionLength);

            int caretIndex = txt.CaretIndex;
            if (realtext.Length < caretIndex)
                caretIndex = realtext.Length;

            var newtext = realtext.Insert(caretIndex, input);

            return newtext;
        }

        private bool CheckIsDigit(string wert)
        {
            return wert.ToCharArray().All(Char.IsDigit);
        }

        private bool IsCountZerosBeforeNumberCorrect(string input)
        {
            var symbols = new List<char>();

            foreach (var symbol in input.ToCharArray())
            {
                if (symbol == '0')
                {
                    symbols.Add(symbol);
                }
                else
                {
                    break;
                }
            }

            var zeros = new List<char>();

            foreach (var symbol in symbols)
            {
                if (symbol == '0')
                {
                    zeros.Add(symbol);
                }
                else
                {
                    break;
                }
            }

            if (zeros.Count > 1)
            {
                return false;
            }

            return true;
        }

        private bool IsNumberHaveZeroFirst(string input)
        {
            if (input.ToCharArray().First() == '0')
            {
                return true;
            }

            return false;
        }

        private bool IsNumberGreaterThanMaxInt32(string input)
        {
            try
            {
                Convert.ToInt32(input);
            }
            catch (Exception exception)
            {
                if (exception is OverflowException)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsNumberGreaterThanMaxDouble(string input)
        {
            try
            {
                Convert.ToDouble(input);
            }
            catch (Exception exception)
            {
                if (exception is OverflowException)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsValidInput(string input)
        {
            switch (InputMode)
            {
                case TextBoxInputMode.None:
                    return true;

                case TextBoxInputMode.DigitInput:
                    if (IsCountZerosBeforeNumberCorrect(input) == false)
                        return false;

                    return CheckIsDigit(input);

                case TextBoxInputMode.DigitInputWithoutFirstZero:
                    if (IsNumberHaveZeroFirst(input) == true)
                        return false;

                    return CheckIsDigit(input);

                case TextBoxInputMode.DigitInputForMaxValueInt32AndWithoutFirstZero:
                    if (IsNumberGreaterThanMaxInt32(input) == true ||
                        IsNumberHaveZeroFirst(input) == true)
                        return false;

                    return CheckIsDigit(input);

                case TextBoxInputMode.Decimal:
                    decimal d1;

                    ///
                    /// Some logic will be later
                    ///

                    var result1 = decimal.TryParse(input, validNumberStyles, CultureInfo.CurrentCulture, out d1);

                    return result1;

                case TextBoxInputMode.DecimalInputWithoutMinus:
                    decimal d2;

                    if (input.ToCharArray().Where(x => x == ',').Count() > 1)
                        return false;

                    if (input.ToCharArray().Where(x => x == '-').Count() > 0)
                        return false;

                    if (IsCountZerosBeforeNumberCorrect(input) == false)
                        return false;

                    var result2 = decimal.TryParse(input, validNumberStyles, CultureInfo.CurrentCulture, out d2);

                    return result2;

                case TextBoxInputMode.DecimalInputForMaxValueDoubleAndWithoutMinus:
                    decimal d3;

                    if (input.ToCharArray().Where(x => x == ',').Count() > 1)
                        return false;

                    if (input.ToCharArray().Where(x => x == '-').Count() > 0)
                        return false;

                    if (IsCountZerosBeforeNumberCorrect(input) == false)
                        return false;

                    if (IsNumberGreaterThanMaxDouble(input) == true)
                        return false;

                    var result3 = decimal.TryParse(input, validNumberStyles, CultureInfo.CurrentCulture, out d3);

                    return result3;

                default: throw new ArgumentException("Unknown TextBoxInputMode");

            }
            return true;
        }
    }

    public enum TextBoxInputMode
    {
        None,
        Decimal,
        DecimalInputWithoutMinus,
        DecimalInputForMaxValueDoubleAndWithoutMinus,
        DigitInput,
        DigitInputWithoutFirstZero,
        DigitInputForMaxValueInt32AndWithoutFirstZero
    }
}
