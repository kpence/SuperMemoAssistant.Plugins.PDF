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
// Created On:   2018/06/11 14:29
// Modified On:  2018/06/11 14:37
// Modified By:  Alexis

#endregion




using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Patagames.Pdf.Enums;
using Patagames.Pdf.Net;
using Patagames.Pdf.Net.Controls.Wpf;
//using System.Drawing;

// ReSharper disable PossibleMultipleEnumeration

// ReSharper disable BitwiseOperatorOnEnumWithoutFlags

namespace SuperMemoAssistant.Plugins.PDF.Viewer
{
  /// <inheritdoc />
  public partial class IPDFViewer : PdfViewer
  {
    public static readonly Color SMExtractColor = Color.FromArgb(30,
                                                                 80,
                                                                 100,
                                                                 120);
    public static readonly Color IPDFExtractColor = Color.FromArgb(30,
                                                                   120,
                                                                   100,
                                                                   80);

    protected IPDFDocument IPDFDocument { get; set; }
    protected Dictionary<int, List<HighlightInfo>> ExtractHighlights { get; } = new Dictionary<int, List<HighlightInfo>>();
    



    #region Constructors

    public IPDFViewer()
    {
      _smoothSelection = false;
    }

    public void LoadDocument(IPDFDocument document)
    {
      bool isNewPdf = IPDFDocument?.FilePath.Equals(document.FilePath) ?? false;
      IPDFDocument = document;

      if (isNewPdf == false)
        LoadDocument(document.FilePath);

      else
        OnDocumentLoaded(null);
    }

    protected override void OnDocumentLoaded(EventArgs ev)
    {
      base.OnDocumentLoaded(ev);

      ExtractHighlights.Clear();
      IPDFDocument.SMExtracts.ForEach(e => AddSMExtractHighlight(e.pageIdx, e.startIdx, e.count));
      IPDFDocument.IPDFExtracts.ForEach(e => AddIPDFExtractHighlight(e.pageIdx, e.startIdx, e.count));
    }

    public void ToImg()
    {
      
      //The current page of loaded document.
      var page = CurrentPage;

      var rect = CalcActualRect(0);

      double ratio = rect.Width / page.Width;
      int Width = (int)(page.Width * ratio);
      int Height = (int)(page.Height * ratio);

      using (var bmp = new PdfBitmap((int)Width, (int)Height, true))
      {
        //Render part of page into bitmap;
        CurrentPage.Render(bmp, 0, 0, (int)(Width), (int)(Height), PageRotate.Normal, RenderFlags.FPDF_LCD_TEXT);

        var rbmp = new System.Drawing.Bitmap(bmp.Image);
        rbmp.Save("D:\\Temp\\pdfium_out.png");
      }
    }

    #endregion
  }
}
