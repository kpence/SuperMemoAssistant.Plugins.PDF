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
// Created On:   2019/09/03 18:15
// Modified On:  2020/01/17 10:20
// Modified By:  Alexis

#endregion




using System;
using Patagames.Pdf;
using SuperMemoAssistant.Plugins.PDF.Utils.Web;

namespace SuperMemoAssistant.Plugins.PDF.Models
{
  public class PDFAreaSelection : ITextContent
  {
    #region Constructors

    public PDFAreaSelection(int    pageIndex,
                            double x1,
                            double y1,
                            double x2,
                            double y2)
    {
      PageIndex = pageIndex;
      X1        = x1;
      Y1        = y1;
      X2        = x2;
      Y2        = y2;
    }

    #endregion




    #region Properties & Fields - Public

    public AreaType Type      { get; set; } = AreaType.Normal;
    public int      PageIndex { get; set; }
    public double   X1        { get; set; }
    public double   Y1        { get; set; }
    public double   X2        { get; set; }
    public double   Y2        { get; set; }
    public string   OcrText   { get; set; }

    #endregion




    #region Methods Impl

    public override string ToString()
    {
      return $"area extract page {PageIndex}";
    }

    /// <inheritdoc />
    public void Append(HtmlBuilder builder)
    {
      if (Type != AreaType.Ocr)
        throw new NotSupportedException("Append called on a non OCR area");

      if (IsValid())
        builder.Append(OcrText);
    }

    #endregion




    #region Methods

    public FS_RECTF Normalized()
    {
      double x1 = Math.Min(X1, X2);
      double x2 = Math.Max(X1, X2);
      double y1 = Math.Min(Y1, Y2);
      double y2 = Math.Max(Y1, Y2);

      return new FS_RECTF(x1,
                          y1,
                          x2,
                          y2);
    }

    public (System.Windows.Point, System.Windows.Point) NormalizedPoints()
    {
      double x1 = Math.Min(X1,
                           X2);
      double x2 = Math.Max(X1,
                           X2);
      double y1 = Math.Min(Y1,
                           Y2);
      double y2 = Math.Max(Y1,
                           Y2);

      return (new System.Windows.Point(x1,
                                       y1),
              new System.Windows.Point(x2,
                                       y2));
    }

    public bool IsValid()
    {
      return Type == AreaType.Normal
        || Type == AreaType.Ocr && string.IsNullOrWhiteSpace(OcrText) == false;
    }

    #endregion




    #region Enums

    public enum AreaType
    {
      Normal = 1,
      Ocr    = 2,
    }

    #endregion
  }
}
