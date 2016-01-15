#region Header
// ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// ┃  FILE:       ExtensionMethods.cs
// ┃  PROJECT:    BaseConverter
// ┃  SOLUTION:   BaseConverter
// ┃  CREATED:    2016-01-14 @ 6:59 PM
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
using System.Text;

namespace BaseConverter
{
	public static class ExtensionMethods
	{
		public static string ToBinaryString(this long value, bool isSigned, int sizeInBits)
		{
			if (sizeInBits != 8 && sizeInBits != 16 && sizeInBits != 32 && sizeInBits != 64)
				return String.Empty;

			var result = new StringBuilder();
			var quotient = value;
			while (quotient != 0)
			{
				result.Insert(0, quotient%2);
				quotient /= 2;
			}
			return result.ToString();
		}

		public static bool ParseBinaryString(this string value, bool isSigned, int sizeInBits, out long numericValue)
		{
			numericValue = 0L;
			if (sizeInBits != 8 && sizeInBits != 16 && sizeInBits != 32 && sizeInBits != 64)
				return false;

			for (var i = 0; i < value.Length; i++)
			{
				if (value[i] != '0' && value[i] != '1')
					return false;

				if (value[i] == '1')
					numericValue += (long) Math.Pow(2, (value.Length - 1) - i);
			}
			return true;
		}

		public static bool ParseHexadecimalString(this string value, bool isSigned, int sizeInBits, out long numericValue)
		{
			numericValue = 0L;
			if (sizeInBits != 8 && sizeInBits != 16 && sizeInBits != 32 && sizeInBits != 64)
				return false;

			for (var i = 0; i < value.Length; i++)
			{
				var currentDigit = 0;
				switch (value[i])
				{
					case '0':
						currentDigit = 0;
						break;
					case '1':
						currentDigit = 1;
						break;
					case '2':
						currentDigit = 2;
						break;
					case '3':
						currentDigit = 3;
						break;
					case '4':
						currentDigit = 4;
						break;
					case '5':
						currentDigit = 5;
						break;
					case '6':
						currentDigit = 6;
						break;
					case '7':
						currentDigit = 7;
						break;
					case '8':
						currentDigit = 8;
						break;
					case '9':
						currentDigit = 9;
						break;
					case 'A':
						currentDigit = 10;
						break;
					case 'B':
						currentDigit = 11;
						break;
					case 'C':
						currentDigit = 12;
						break;
					case 'D':
						currentDigit = 13;
						break;
					case 'E':
						currentDigit = 14;
						break;
					case 'F':
						currentDigit = 15;
						break;
					default:
						return false;
				}

				numericValue += (long) Math.Pow(16, (value.Length - 1) - i)*currentDigit;
			}
			return true;
		}
	}
}