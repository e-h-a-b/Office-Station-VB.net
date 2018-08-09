Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
'<info>
' --------------------Fader Theme--------------------
' Creator - SaketSaket (HF)
' UID - 1869668
' Inspiration & Credits to all Theme creators of HF
' Version - 1.0
' Date Created - 1st December 2014
' Date Modified - 12th December 2014
'
'
'Special Thanks to Aeonhack for RoundRect0 Functions...
'AlertBox Control idea taken from iSynthesis' Flat UI theme
'
'
' For bugs & Constructive Criticism contact me on HF
' If you like it & want to DONATE then pm me on HF
' --------------------Fader Theme--------------------
'<info>

'Please Leave Credits in Source, Do not redistribute
 
Module Draw0
    'Special Thanks to Aeonhack for RoundRect0 Functions... ;)
    Public Function RoundRect0(ByVal rectangle As Rectangle, ByVal curve As Integer) As GraphicsPath
        Dim p As GraphicsPath = New GraphicsPath()
        Dim arcRectangleWidth As Integer = curve * 2
        p.AddArc(New Rectangle(rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -180, 90)
        p.AddArc(New Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -90, 90)
        p.AddArc(New Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 0, 90)
        p.AddArc(New Rectangle(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 90, 90)
        p.AddLine(New Point(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y), New Point(rectangle.X, curve + rectangle.Y))
        Return p
    End Function
End Module
Public Class FaderListBox : Inherits ListBox
    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or _
                 ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
        DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        ForeColor = Color.White
        BackColor = Color.FromArgb(61, 61, 61)
        BorderStyle = Windows.Forms.BorderStyle.None
        ItemHeight = 20
    End Sub
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)

        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)

        MyBase.OnPaint(e)
        g.Clear(Color.Transparent)
        g.FillPath(New SolidBrush(Color.FromArgb(61, 61, 61)), RoundRect0(rect, 3))
        g.DrawPath(New Pen(Color.Black, 2), RoundRect0(rect, 3))

        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)

        g.TextRenderingHint = TextRenderingHint.AntiAlias
        g.SmoothingMode = SmoothingMode.HighQuality

        g.SetClip(RoundRect0(New Rectangle(0, 0, Width, Height), 3))
        g.Clear(Color.Transparent)
        g.FillRectangle(New SolidBrush(BackColor), New Rectangle(e.Bounds.X, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height + 3))

        If e.State.ToString().Contains("Selected,") Then
            Dim selectgb As New LinearGradientBrush(New Rectangle(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height), Color.FromArgb(61, 61, 61), Color.FromArgb(41, 41, 31), 90S)
            g.FillRectangle(selectgb, New Rectangle(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
            g.DrawRectangle(New Pen(Color.FromArgb(128, 128, 128)), New Rectangle(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
            Try
                g.DrawString(Items(e.Index).ToString(), New Font("Segoe UI", 10, FontStyle.Bold), New SolidBrush(Color.FromArgb(245, 245, 245)), New Rectangle(e.Bounds.X + 3, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
            Catch : End Try
        Else
            Dim nonselectgb As New LinearGradientBrush(New Rectangle(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height), Color.FromArgb(81, 81, 81), Color.FromArgb(61, 61, 61), 90S)
            g.FillRectangle(nonselectgb, e.Bounds)
            Try
                g.DrawString(Items(e.Index).ToString(), New Font("Segoe UI", 10, FontStyle.Regular), New SolidBrush(Color.FromArgb(245, 245, 245)), New Rectangle(e.Bounds.X + 3, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
            Catch : End Try
        End If

        g.DrawPath(New Pen(Color.FromArgb(61, 61, 61), 2), RoundRect0(New Rectangle(0, 0, Width - 1, Height - 1), 1))

        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class
