﻿#region License & Metadata

// The MIT License (MIT)
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// 
// 
// Created On:   2018/12/12 01:29
// Modified On:  2018/12/13 12:48
// Modified By:  Alexis

#endregion




using System;
using System.Globalization;
using System.Windows.Data;

namespace SuperMemoAssistant.Plugins.PDF.Converters
{
  public class LessOrEqualConverter : IValueConverter
  {
    #region Methods Impl

    /// <inheritdoc />
    public object Convert(object      value,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
      int val1 = System.Convert.ToInt32(value);
      int val2 = System.Convert.ToInt32(parameter);

      return val1 <= val2;
    }

    /// <inheritdoc />
    public object ConvertBack(object      value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
