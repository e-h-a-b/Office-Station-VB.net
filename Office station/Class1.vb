Imports System.Drawing.Drawing2D
Module ConversionFunctions
    Function ToBrush(ByVal A As Integer, ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) As Brush
        Return New SolidBrush(Color.FromArgb(A, R, G, B))
    End Function
    Function ToBrush(ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) As Brush
        Return New SolidBrush(Color.FromArgb(R, G, B))
    End Function
    Function ToBrush(ByVal A As Integer, ByVal C As Color) As Brush
        Return New SolidBrush(Color.FromArgb(A, C))
    End Function
    Function ToBrush(ByVal Pen As Pen) As Brush
        Return New SolidBrush(Pen.Color)
    End Function
    Function ToBrush(ByVal Color As Color) As Brush
        Return New SolidBrush(Color)
    End Function
    Function ToPen(ByVal A As Integer, ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) As Pen
        Return New Pen(New SolidBrush(Color.FromArgb(A, R, G, B)))
    End Function
    Function ToPen(ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) As Pen
        Return New Pen(New SolidBrush(Color.FromArgb(R, G, B)))
    End Function
    Function ToPen(ByVal A As Integer, ByVal C As Color) As Pen
        Return New Pen(New SolidBrush(Color.FromArgb(A, C)))
    End Function
    Function ToPen(ByVal Brush As SolidBrush) As Pen
        Return New Pen(Brush)
    End Function
    Function ToPen(ByVal Color As Color) As Pen
        Return New Pen(New SolidBrush(Color))
    End Function
End Module

Module RRM
    Public Function RoundRect(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
        Dim P As GraphicsPath = New GraphicsPath()
        Dim ArcRectangleWidth As Integer = Curve * 2
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
        P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
        Return P
    End Function
    Public Function RoundRect(ByVal X As Integer, ByVal Y As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal Curve As Integer) As GraphicsPath
        Dim Rectangle As Rectangle = New Rectangle(X, Y, Width, Height)
        Dim P As GraphicsPath = New GraphicsPath()
        Dim ArcRectangleWidth As Integer = Curve * 2
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
        P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
        Return P
    End Function
End Module

Module Shapes0
    Public Function Triangle(ByVal Location As Point, ByVal Size As Size) As Point()
        Dim ReturnPoints(0 To 3) As Point
        ReturnPoints(0) = Location
        ReturnPoints(1) = New Point(Location.X + Size.Width, Location.Y)
        ReturnPoints(2) = New Point(Location.X + Size.Width \ 2, Location.Y + Size.Height)
        ReturnPoints(3) = Location

        Return ReturnPoints
    End Function
End Module
Public Class SkyDarkTabControl
    Inherits TabControl

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or _
        ControlStyles.ResizeRedraw Or _
        ControlStyles.UserPaint Or _
        ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
    End Sub
    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
        Alignment = TabAlignment.Top
    End Sub
    Dim C1 As Color = Color.FromArgb(62, 60, 58)
    Dim C2 As Color = Color.FromArgb(80, 78, 76)
    Dim C3 As Color = Color.FromArgb(51, 49, 47)
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        Try : SelectedTab.BackColor = C1 : Catch : End Try
        G.Clear(Parent.BackColor)


        For i = 0 To TabCount - 1
            If Not i = SelectedIndex Then
                Dim x2 As Rectangle = New Rectangle(GetTabRect(i).X, GetTabRect(i).Y + 3, GetTabRect(i).Width + 2, GetTabRect(i).Height)
                Dim G1 As New LinearGradientBrush(New Point(x2.X, x2.Y), New Point(x2.X, x2.Y + x2.Height), Color.FromArgb(60, 59, 58), Color.FromArgb(69, 69, 70))
                G.FillRectangle(G1, x2) : G1.Dispose()
                G.DrawRectangle(ToPen(C3), x2)
                G.DrawRectangle(ToPen(C2), New Rectangle(x2.X + 1, x2.Y + 1, x2.Width - 2, x2.Height))
                '  G.DrawString(TabPages(i).Text, Font, ToBrush(130, 176, 190), x2, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
                G.DrawString(TabPages(i).Text, Font, Brushes.Gray, x2, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})

            End If
        Next

        'G.FillRectangle(ToBrush(C1), 0, ItemSize.Height, Width, Height)
        'G.DrawRectangle(ToPen(C2), 0, ItemSize.Height, Width - 1, Height - ItemSize.Height - 1)
        'G.DrawRectangle(ToPen(C3), 1, ItemSize.Height + 1, Width - 3, Height - ItemSize.Height - 3)
        If Not SelectedIndex = -1 Then
            Dim x1 As Rectangle = New Rectangle(GetTabRect(SelectedIndex).X - 2, GetTabRect(SelectedIndex).Y, GetTabRect(SelectedIndex).Width + 3, GetTabRect(SelectedIndex).Height)
            G.FillRectangle(ToBrush(C1), New Rectangle(x1.X + 2, x1.Y + 2, x1.Width - 2, x1.Height))
            G.DrawLine(ToPen(C2), New Point(x1.X, x1.Y + x1.Height - 2), New Point(x1.X, x1.Y))
            G.DrawLine(ToPen(C2), New Point(x1.X, x1.Y), New Point(x1.X + x1.Width, x1.Y))
            G.DrawLine(ToPen(C2), New Point(x1.X + x1.Width, x1.Y), New Point(x1.X + x1.Width, x1.Y + x1.Height - 2))

            G.DrawLine(ToPen(C3), New Point(x1.X + 1, x1.Y + x1.Height - 1), New Point(x1.X + 1, x1.Y + 1))
            G.DrawLine(ToPen(C3), New Point(x1.X + 1, x1.Y + 1), New Point(x1.X + x1.Width - 1, x1.Y + 1))
            G.DrawLine(ToPen(C3), New Point(x1.X + x1.Width - 1, x1.Y + 1), New Point(x1.X + x1.Width - 1, x1.Y + x1.Height - 1))

            G.DrawString(TabPages(SelectedIndex).Text, Font, ToBrush(130, 176, 190), x1, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
        End If
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImage(B.Clone, 0, 0)
        G.Dispose() : B.Dispose()
    End Sub
End Class
