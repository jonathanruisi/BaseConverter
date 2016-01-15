#region Header
// ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// ┃  FILE:       MainWindow.xaml.cs
// ┃  PROJECT:    BaseConverter
// ┃  SOLUTION:   BaseConverter
// ┃  CREATED:    2016-01-14 @ 7:12 PM
// ┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// ┃  AUTHOR:     Jonathan Ruisi
// ┃  EMAIL:      JonathanRuisi@gmail.com
// ┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// ┃  This file is part of BaseConverter.
// ┃  BaseConverter is free software: you can redistribute it and/or modify it under the terms
// ┃  of the GNU General Public License as published by the Free Software Foundation,
// ┃  either version 3 of the license, or (at your option) to any later version.
// ┃
// ┃  BaseConverter is distributed in the hope that it will be useful,
// ┃  but WITHOUT ANY WARRANTY; without even the implied warranty of
// ┃  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// ┃  See the GNU General Public License for more details.
// ┃
// ┃  A copy of the GNU General Public License is included with BaseConverter,
// ┃  and is also available at <http://www.gnu.org/licenses/>
// ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
#endregion

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

using BaseConverter;

// TODO: Implement capability to handle signed values
// TODO: Fix truncation issues (occurs when switching bases with a value other than zero)

namespace JonathanRuisi.BaseConverter
{
	public partial class MainWindow
	{
		#region Properties
		private long CurrentValue
		{
			get { return (long) GetValue(CurrentValueProperty); }
			set { SetValue(CurrentValueProperty, value); }
		}

		public static readonly DependencyProperty CurrentValueProperty =
			DependencyProperty.Register("CurrentValue", typeof(long), typeof(MainWindow),
				new FrameworkPropertyMetadata(0L, OnDependencyPropertyChanged));

		private bool IsSigned { get { return (bool) GetValue(IsSignedProperty); } set { SetValue(IsSignedProperty, value); } }

		public static readonly DependencyProperty IsSignedProperty =
			DependencyProperty.Register("IsSigned", typeof(bool), typeof(MainWindow),
				new FrameworkPropertyMetadata(false, OnDependencyPropertyChanged));

		private int SizeInBits
		{
			get { return (int) GetValue(SizeInBitsProperty); }
			set { SetValue(SizeInBitsProperty, value); }
		}

		public static readonly DependencyProperty SizeInBitsProperty =
			DependencyProperty.Register("SizeInBits", typeof(int), typeof(MainWindow),
				new FrameworkPropertyMetadata(16, OnDependencyPropertyChanged));

		private NumberBase NumberBase
		{
			get { return (NumberBase) GetValue(NumberBaseProperty); }
			set { SetValue(NumberBaseProperty, value); }
		}

		public static readonly DependencyProperty NumberBaseProperty =
			DependencyProperty.Register("NumberBase", typeof(NumberBase), typeof(MainWindow),
				new FrameworkPropertyMetadata(NumberBase.Hexadecimal, OnDependencyPropertyChanged));

		private string BinaryString
		{
			get { return (string) GetValue(BinaryStringProperty); }
			set { SetValue(BinaryStringProperty, value); }
		}

		public static readonly DependencyProperty BinaryStringProperty =
			DependencyProperty.Register("BinaryString", typeof(string), typeof(MainWindow),
				new FrameworkPropertyMetadata(String.Empty, OnDependencyPropertyChanged));

		private string DecimalString
		{
			get { return (string) GetValue(DecimalStringProperty); }
			set { SetValue(DecimalStringProperty, value); }
		}

		public static readonly DependencyProperty DecimalStringProperty =
			DependencyProperty.Register("DecimalString", typeof(string), typeof(MainWindow),
				new FrameworkPropertyMetadata(String.Empty, OnDependencyPropertyChanged));

		private string HexadecimalString
		{
			get { return (string) GetValue(HexadecimalStringProperty); }
			set { SetValue(HexadecimalStringProperty, value); }
		}

		public static readonly DependencyProperty HexadecimalStringProperty =
			DependencyProperty.Register("HexadecimalString", typeof(string), typeof(MainWindow),
				new FrameworkPropertyMetadata(String.Empty, OnDependencyPropertyChanged));
		#endregion

		#region Constructor
		public MainWindow()
		{
			InitializeComponent();
		}
		#endregion

		#region Property Changed Callbacks
		private static void OnDependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var mainWnd = d as MainWindow;
			if (mainWnd == null) return;

			if (e.Property == NumberBaseProperty)
			{
				switch ((NumberBase) e.NewValue)
				{
					case NumberBase.Binary:
						mainWnd.Button0.IsEnabled = true;
						mainWnd.Button1.IsEnabled = true;
						mainWnd.Button2.IsEnabled = false;
						mainWnd.Button3.IsEnabled = false;
						mainWnd.Button4.IsEnabled = false;
						mainWnd.Button5.IsEnabled = false;
						mainWnd.Button6.IsEnabled = false;
						mainWnd.Button7.IsEnabled = false;
						mainWnd.Button8.IsEnabled = false;
						mainWnd.Button9.IsEnabled = false;
						mainWnd.ButtonA.IsEnabled = false;
						mainWnd.ButtonB.IsEnabled = false;
						mainWnd.ButtonC.IsEnabled = false;
						mainWnd.ButtonD.IsEnabled = false;
						mainWnd.ButtonE.IsEnabled = false;
						mainWnd.ButtonF.IsEnabled = false;

						Grid.SetColumnSpan(mainWnd.Button1, 2);
						mainWnd.Button1.BorderThickness = new Thickness(5, 5, 5, 2.5);
						mainWnd.Button2.Opacity = 0;
						mainWnd.Button2.Visibility = Visibility.Collapsed;
						mainWnd.ButtonSign.BorderThickness = new Thickness(2.5, 5, 5, 5);
						break;
					case NumberBase.Decimal:
						mainWnd.Button0.IsEnabled = true;
						mainWnd.Button1.IsEnabled = true;
						mainWnd.Button2.IsEnabled = true;
						mainWnd.Button3.IsEnabled = true;
						mainWnd.Button4.IsEnabled = true;
						mainWnd.Button5.IsEnabled = true;
						mainWnd.Button6.IsEnabled = true;
						mainWnd.Button7.IsEnabled = true;
						mainWnd.Button8.IsEnabled = true;
						mainWnd.Button9.IsEnabled = true;
						mainWnd.ButtonA.IsEnabled = false;
						mainWnd.ButtonB.IsEnabled = false;
						mainWnd.ButtonC.IsEnabled = false;
						mainWnd.ButtonD.IsEnabled = false;
						mainWnd.ButtonE.IsEnabled = false;
						mainWnd.ButtonF.IsEnabled = false;

						Grid.SetColumnSpan(mainWnd.Button1, 1);
						mainWnd.Button2.Opacity = 1;
						mainWnd.Button2.Visibility = Visibility.Visible;
						mainWnd.Button1.BorderThickness = new Thickness(5, 2.5, 2.5, 2.5);
						mainWnd.ButtonSign.BorderThickness = new Thickness(2.5, 2.5, 5, 5);
						mainWnd.Button7.BorderThickness = new Thickness(5, 5, 2.5, 2.5);
						mainWnd.Button8.BorderThickness = new Thickness(2.5, 5, 2.5, 2.5);
						mainWnd.Button9.BorderThickness = new Thickness(2.5, 5, 5, 2.5);
						break;
					case NumberBase.Hexadecimal:
						mainWnd.Button0.IsEnabled = true;
						mainWnd.Button1.IsEnabled = true;
						mainWnd.Button2.IsEnabled = true;
						mainWnd.Button3.IsEnabled = true;
						mainWnd.Button4.IsEnabled = true;
						mainWnd.Button5.IsEnabled = true;
						mainWnd.Button6.IsEnabled = true;
						mainWnd.Button7.IsEnabled = true;
						mainWnd.Button8.IsEnabled = true;
						mainWnd.Button9.IsEnabled = true;
						mainWnd.ButtonA.IsEnabled = true;
						mainWnd.ButtonB.IsEnabled = true;
						mainWnd.ButtonC.IsEnabled = true;
						mainWnd.ButtonD.IsEnabled = true;
						mainWnd.ButtonE.IsEnabled = true;
						mainWnd.ButtonF.IsEnabled = true;

						Grid.SetColumnSpan(mainWnd.Button1, 1);
						mainWnd.Button2.Opacity = 1;
						mainWnd.Button2.Visibility = Visibility.Visible;
						mainWnd.Button1.BorderThickness = new Thickness(5, 2.5, 2.5, 2.5);
						mainWnd.ButtonSign.BorderThickness = new Thickness(2.5, 2.5, 5, 5);
						mainWnd.Button7.BorderThickness = new Thickness(5, 2.5, 2.5, 2.5);
						mainWnd.Button8.BorderThickness = new Thickness(2.5);
						mainWnd.Button9.BorderThickness = new Thickness(2.5, 2.5, 5, 2.5);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			else if (e.Property == SizeInBitsProperty)
			{
				if (mainWnd.CurrentValue != 0)
				{
					MessageBox.Show(mainWnd,
						"Please note that values are not always correctly truncated when changing size for an existing value", "Warning",
						MessageBoxButton.OK, MessageBoxImage.Information);
				}
			}
			else if (e.Property == IsSignedProperty)
			{
				if ((bool) e.NewValue)
				{
					MessageBox.Show(mainWnd, "Please note that negative numbers are not represented correctly (yet)...", "Warning",
						MessageBoxButton.OK, MessageBoxImage.Information);
				}
			}
			else if (e.Property == CurrentValueProperty)
			{
				mainWnd.TextBoxBinary.FontSize = mainWnd.CurrentValue > 65536 ? (mainWnd.CurrentValue > 4294967295 ? 10 : 20) : 30;
				switch (mainWnd.NumberBase)
				{
					case NumberBase.Binary:
						mainWnd.DecimalString = mainWnd.CurrentValue.ToString();
						mainWnd.HexadecimalString = mainWnd.CurrentValue.ToString("X");
						break;
					case NumberBase.Decimal:
						mainWnd.BinaryString = mainWnd.CurrentValue.ToBinaryString(mainWnd.IsSigned, mainWnd.SizeInBits);
						mainWnd.HexadecimalString = mainWnd.CurrentValue.ToString("X");
						break;
					case NumberBase.Hexadecimal:
						mainWnd.BinaryString = mainWnd.CurrentValue.ToBinaryString(mainWnd.IsSigned, mainWnd.SizeInBits);
						mainWnd.DecimalString = mainWnd.CurrentValue.ToString();
						break;
				}
			}
		}
		#endregion

		#region Event Handlers (Window)
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var binding = new Binding("IsSigned") {Source = this, Mode = BindingMode.TwoWay};
			ToggleButtonSigned.SetBinding(ToggleButton.IsCheckedProperty, binding);
			binding = new Binding("IsSigned") {Source = this};
			ButtonSign.SetBinding(IsEnabledProperty, binding);

			binding = new Binding("SizeInBits")
			{
				Source = this,
				Mode = BindingMode.TwoWay,
				Converter = new SizeInBitsToBooleanConverter(),
				ConverterParameter = 8
			};
			ToggleButtonSize8.SetBinding(ToggleButton.IsCheckedProperty, binding);

			binding = new Binding("SizeInBits")
			{
				Source = this,
				Mode = BindingMode.TwoWay,
				Converter = new SizeInBitsToBooleanConverter(),
				ConverterParameter = 16
			};
			ToggleButtonSize16.SetBinding(ToggleButton.IsCheckedProperty, binding);

			binding = new Binding("SizeInBits")
			{
				Source = this,
				Mode = BindingMode.TwoWay,
				Converter = new SizeInBitsToBooleanConverter(),
				ConverterParameter = 32
			};
			ToggleButtonSize32.SetBinding(ToggleButton.IsCheckedProperty, binding);

			binding = new Binding("SizeInBits")
			{
				Source = this,
				Mode = BindingMode.TwoWay,
				Converter = new SizeInBitsToBooleanConverter(),
				ConverterParameter = 64
			};
			ToggleButtonSize64.SetBinding(ToggleButton.IsCheckedProperty, binding);

			binding = new Binding("NumberBase")
			{
				Source = this,
				Mode = BindingMode.TwoWay,
				Converter = new NumberBaseToBooleanConverter(),
				ConverterParameter = NumberBase.Binary
			};
			ToggleButtonBinary.SetBinding(ToggleButton.IsCheckedProperty, binding);

			binding = new Binding("NumberBase")
			{
				Source = this,
				Mode = BindingMode.TwoWay,
				Converter = new NumberBaseToBooleanConverter(),
				ConverterParameter = NumberBase.Decimal
			};
			ToggleButtonDecimal.SetBinding(ToggleButton.IsCheckedProperty, binding);

			binding = new Binding("NumberBase")
			{
				Source = this,
				Mode = BindingMode.TwoWay,
				Converter = new NumberBaseToBooleanConverter(),
				ConverterParameter = NumberBase.Hexadecimal
			};
			ToggleButtonHexadecimal.SetBinding(ToggleButton.IsCheckedProperty, binding);

			binding = new Binding("NumberBase")
			{
				Source = this,
				Converter = new NumberBaseToVisibilityConverter(),
				ConverterParameter = NumberBase.Binary
			};
			PolygonBinary.SetBinding(VisibilityProperty, binding);

			binding = new Binding("NumberBase")
			{
				Source = this,
				Converter = new NumberBaseToVisibilityConverter(),
				ConverterParameter = NumberBase.Decimal
			};
			PolygonDecimal.SetBinding(VisibilityProperty, binding);

			binding = new Binding("NumberBase")
			{
				Source = this,
				Converter = new NumberBaseToVisibilityConverter(),
				ConverterParameter = NumberBase.Hexadecimal
			};
			PolygonHexadecimal.SetBinding(VisibilityProperty, binding);

			binding = new Binding("BinaryString") {Source = this, Mode = BindingMode.TwoWay};
			TextBoxBinary.SetBinding(TextBox.TextProperty, binding);
			binding = new Binding("DecimalString") {Source = this, Mode = BindingMode.TwoWay};
			TextBoxDecimal.SetBinding(TextBox.TextProperty, binding);
			binding = new Binding("HexadecimalString") {Source = this, Mode = BindingMode.TwoWay};
			TextBoxHexadecimal.SetBinding(TextBox.TextProperty, binding);
		}
		#endregion

		#region Event Handlers (Entry Buttons)
		private void ButtonNumeric_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			if (button == null) return;

			if (BinaryString.Length >= SizeInBits)
				return;

			switch (NumberBase)
			{
				case NumberBase.Binary:
					if (BinaryString == "0")
						BinaryString = button.Tag as string;
					else
						BinaryString += button.Tag;
					break;
				case NumberBase.Decimal:
					if (DecimalString == "0")
						DecimalString = button.Tag as string;
					else
						DecimalString += button.Tag;
					break;
				case NumberBase.Hexadecimal:
					if (HexadecimalString == "0")
						HexadecimalString = button.Tag as string;
					else
						HexadecimalString += button.Tag;
					break;
			}
		}

		private void ButtonSign_Click(object sender, RoutedEventArgs e)
		{
			CurrentValue = 0 - CurrentValue;
		}

		private void ButtonClear_Click(object sender, RoutedEventArgs e)
		{
			CurrentValue = 0L;
			BinaryString = "0";
			DecimalString = "0";
			HexadecimalString = "0";
		}
		#endregion

		#region Event Handlers (Text Boxes)
		private void TextBoxBinary_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (NumberBase != NumberBase.Binary)
				return;

			long value;
			if (BinaryString.ParseBinaryString(IsSigned, SizeInBits, out value))
				CurrentValue = value;
		}

		private void TextBoxDecimal_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (NumberBase != NumberBase.Decimal)
				return;

			CurrentValue = long.Parse(DecimalString);
		}

		private void TextBoxHexadecimal_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (NumberBase != NumberBase.Hexadecimal)
				return;

			long value;
			if (HexadecimalString.ParseHexadecimalString(IsSigned, SizeInBits, out value))
				CurrentValue = value;
		}
		#endregion
	}
}