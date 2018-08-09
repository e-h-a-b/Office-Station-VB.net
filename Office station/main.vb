Imports System.Net
Imports System.Text
Imports System.Threading
Imports System.IO
Imports System.Windows.Forms.DataVisualization.Charting

Public Class main

#Region "style of form"

    'A form with custom border and title bar.
    'Some functions, such as resize the window via mouse, are not implemented yet. 


    'The color and the width of the border.
    Private borderColor As Color = Color.Cyan  'FromArgb(45, 47, 49) '45, 47, 49
    Private borderWidth As Integer = 3
    'The color and region of the header.
    Private headerColor As Color = Color.Cyan 'FromArgb(45, 47, 49)
    Private headerRect As Drawing.Rectangle
    'The region of the client.
    Private clientRect As Drawing.Rectangle
    'The region of the title text.
    Private titleRect As Drawing.Rectangle
    'The region of the minimum button.
    Private miniBoxRect As Drawing.Rectangle
    'The region of the maximum button.
    Private maxBoxRect As Drawing.Rectangle
    'The region of the close button.
    Private closeBoxRect As Drawing.Rectangle
    'The states of the three header buttons.
    Private miniState As ButtonState
    Private maxState As ButtonState
    Private closeState As ButtonState
    'Store the mouse down point to handle moving the form.
    Private x000 As Integer = 0
    Private y000 As Integer = 0
    'The height of the header.
    Const HEADER_HEIGHT As Integer = 20
    'The size of the header buttons.
    ReadOnly BUTTON_BOX_SIZE As Size = New Size(15, 15)


    Dim Test00 As Boolean = False

    Private Sub Form1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        'Draw the header.
        Using b As Brush = New SolidBrush(Color.FromArgb(100, borderColor))
            e.Graphics.FillRectangle(b, headerRect)
        End Using
        'Draw the title text
        If Test00 = False Then
            Using b As Brush = New SolidBrush(Color.Cyan)
                e.Graphics.DrawString("Office Station", Me.Font, b, titleRect)
            End Using
        ElseIf Test00 = True Then
            Using b As Brush = New SolidBrush(Color.Cyan)
                e.Graphics.DrawString("Office Station", Me.Font, b, titleRect)
            End Using
        End If

        'Draw the header buttons.
        'If Me.MinimizeBox Then
        '    ControlPaint.DrawCaptionButton(e.Graphics, miniBoxRect, CaptionButton.Minimize, miniState)
        'End If
        'If Me.MinimizeBox Then
        '    ControlPaint.DrawCaptionButton(e.Graphics, maxBoxRect, CaptionButton.Maximize, maxState)
        'End If
        If Me.MinimizeBox Then
            ControlPaint.DrawCaptionButton(e.Graphics, closeBoxRect, CaptionButton.Close, closeState)
        End If
        'Draw the border.
        'Dim gra As Graphics = Me.CreateGraphics
        'ControlPaint.DrawBorder(gra, clientRect, borderColor, _
        'borderWidth, ButtonBorderStyle.Solid, borderColor, borderWidth, ButtonBorderStyle.Solid, borderColor, borderWidth, ButtonBorderStyle.Solid, borderColor, borderWidth, ButtonBorderStyle.Solid)

    End Sub

    'Handle resize to adjust the region ot border, header and so on.
    Private Sub Form1_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Resize
        headerRect = New Drawing.Rectangle(Me.ClientRectangle.Location, New Size(Me.ClientRectangle.Width, HEADER_HEIGHT))
        clientRect = New Drawing.Rectangle(New Point(Me.ClientRectangle.Location.X, Me.ClientRectangle.Y + HEADER_HEIGHT), _
           New Point(Me.ClientRectangle.Width, Me.ClientRectangle.Height - HEADER_HEIGHT))
        Dim yOffset = (headerRect.Height + borderWidth - BUTTON_BOX_SIZE.Height) / 2
        titleRect = New Drawing.Rectangle(yOffset, yOffset, _
                      Me.ClientRectangle.Width - 3 * (BUTTON_BOX_SIZE.Width + 1) - yOffset, _
                      BUTTON_BOX_SIZE.Height)
        miniBoxRect = New Drawing.Rectangle(Me.ClientRectangle.Width - 3 * (BUTTON_BOX_SIZE.Width + 1), _
                            yOffset, BUTTON_BOX_SIZE.Width, BUTTON_BOX_SIZE.Height)
        maxBoxRect = New Drawing.Rectangle(Me.ClientRectangle.Width - 2 * (BUTTON_BOX_SIZE.Width + 1), _
                            yOffset, BUTTON_BOX_SIZE.Width, BUTTON_BOX_SIZE.Height)
        closeBoxRect = New Drawing.Rectangle(Me.ClientRectangle.Width - 1 * (BUTTON_BOX_SIZE.Width + 1), _
                                   yOffset, BUTTON_BOX_SIZE.Width, BUTTON_BOX_SIZE.Height)

    End Sub
    Dim hh
    Dim mm, mb
    Private Sub Form1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        'Start to move the form.
        If (titleRect.Contains(e.Location)) Then
            x000 = e.X
            y000 = e.Y

            hh = 1
        End If

        'Check and press the header buttons.
        Dim mousePos As Point = Me.PointToClient(Control.MousePosition)
        If (miniBoxRect.Contains(mousePos)) Then
            miniState = ButtonState.Pushed
        ElseIf (maxBoxRect.Contains(mousePos)) Then
            maxState = ButtonState.Pushed
        ElseIf (closeBoxRect.Contains(mousePos)) Then
            closeState = ButtonState.Pushed
        End If

    End Sub

    Private Sub Form1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        'Move and refresh.

        If (hh <> 0) Then
            Me.Location = New Point(Control.MousePosition.X - x000, Control.MousePosition.Y - y000)

        End If


    End Sub

    Private Sub Form1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        'Reset the mouse point.
        x000 = e.X
        y000 = e.Y
        hh = 0
        'Check the button states and modify the window state.
        If miniState = ButtonState.Pushed Then
            Me.WindowState = FormWindowState.Minimized
            miniState = ButtonState.Normal
        ElseIf maxState = ButtonState.Pushed Then
            If Me.WindowState = FormWindowState.Normal Then
                Me.WindowState = FormWindowState.Maximized
                maxState = ButtonState.Checked
            Else
                Me.WindowState = FormWindowState.Normal
                maxState = ButtonState.Normal
            End If
        ElseIf closeState = ButtonState.Pushed Then
            Me.Close()
        End If

    End Sub

    'Handle this event to maxmize/normalize the form via double clicking the title bar.
    Private Sub Form1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDoubleClick
        If (titleRect.Contains(e.Location)) Then
            If Me.WindowState = FormWindowState.Normal Then
                Me.WindowState = FormWindowState.Maximized
                maxState = ButtonState.Checked
            Else
                Me.WindowState = FormWindowState.Normal
                maxState = ButtonState.Normal
            End If
        End If
    End Sub


#End Region
    Private Sub Form1_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LocationChanged
        perohead.Show()
        perohead.Location = New Point(Me.Location.X - 80, Me.Location.Y - 125)

    End Sub
    Public Shared fac

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sendreq.Show()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        previewsentreq.Show()
    End Sub
    Sub fast()
        Dim ritxt As New TextBox
        DataGridView1.DataSource = Nothing
        'Remove Blank Lines
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        Dim wClient As New WebClient
        ritxt.MaxLength = 99999999
        wClient.Encoding = Encoding.UTF8
        wClient.UseDefaultCredentials = True
        wClient.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim IsFlagFound As Boolean = True
        Dim NewColName As String
        Dim cellvalue(20) As String
        Dim result = wClient.DownloadString("http://engco.tk/handler.php?action=perv")
        ritxt.Clear()

        : ritxt.Text = result & vbCrLf
        : ritxt.Text = ritxt.Text.Replace("####", vbCrLf)
        Do Until InStr(ritxt.Text, vbCrLf & vbCrLf) = 0
            ritxt.Text = Replace(ritxt.Text, vbCrLf & vbCrLf, vbCrLf)
        Loop
        Dim coll, col, col1, col2, col3, col4, col5, col6, col7, col8, col9, col10, col11 As New DataGridViewTextBoxColumn
        coll.DataPropertyName = "PropertyName"
        coll.HeaderText = "مسلسل"
        coll.Name = "nam2"
        DataGridView1.Columns.Add(coll)
        DataGridView1.Columns(0).Width = 130
        col.DataPropertyName = "PropertyName"
        col.HeaderText = "التاريخ"
        col.Name = "nam"
        DataGridView1.Columns.Add(col)
        DataGridView1.Columns(1).Width = 130
        col2.DataPropertyName = "PropertyName"
        col2.HeaderText = "البرنامج"
        col2.Name = "age"
        DataGridView1.Columns.Add(col2)
        'DataGridView1.Columns(1).Width = 450
        col3.DataPropertyName = "PropertyName3"
        col3.HeaderText = "كمية الشغل"
        col3.Name = "adrs"
        DataGridView1.Columns.Add(col3)
        'DataGridView1.Columns(2).Width = 250
        col4.DataPropertyName = "PropertyName4"
        col4.HeaderText = "وقت التسليم"
        col4.Name = "address"
        DataGridView1.Columns.Add(col4)
        '  DataGridView1.Columns(3).Width = 350
        col5.DataPropertyName = "PropertyName5"
        col5.HeaderText = "سعر الشغل"
        col5.Name = "phone"
        DataGridView1.Columns.Add(col5)
        ' DataGridView1.Columns(4).Width = 150
        col6.DataPropertyName = "PropertyName6"
        col6.HeaderText = "طريقة الدفع"
        col6.Name = "fax"
        DataGridView1.Columns.Add(col6)
        col7.DataPropertyName = "PropertyName7"
        col7.HeaderText = "الدفع"
        col7.Name = "email"
        DataGridView1.Columns.Add(col7)
        DataGridView1.Columns(7).Width = 60
        col8.DataPropertyName = "PropertyName8"
        col8.HeaderText = "الحالة"
        col8.Name = "urlface"
        DataGridView1.Columns.Add(col8)
        Dim gggg
        Dim ar As New ArrayList
        Dim ar1 As New ArrayList
        For i As Integer = 0 To ritxt.Lines.Length - 1
            If ritxt.Lines(i).Contains("جارى") Then
                gggg = i
                ar.Add(gggg)
                Label5.Text = ar.Count
            End If
            If ritxt.Lines(i).Contains("تام") Then
                gggg = i
                ar1.Add(gggg)
                Label6.Text = ar1.Count
            End If
            Dim row = ritxt.Lines(i) & If(i < ritxt.Lines.Length - 1, Environment.NewLine, "")
            cellvalue = row.Split(","c) 'check what is ur separator
            If IsFlagFound Then
                'For l = 0 To cellvalue.Length - 1
                '    NewColName = Trim(cellvalue(l))
                '    NewColName = NewColName.Replace(vbNewLine, Nothing)
                '    DataGridView1.Columns.Add(NewColName, NewColName)

                'Next
                '    IsFlagFound = False
                'Else

                DataGridView1.Rows.Add(cellvalue)
                '                DataGridView1.Rows.Add(TextLine.Split(" ")(0), TextLine.Replace(TextLine.Split(" ")(0), "").Substring(1))
                Label1.Text = ritxt.Lines.Length - 1

            End If

        Next

        DataGridView1.AutoResizeRows( _
          DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders)
        'For i = 0 To DataGridView1.Columns.Count - 1
        '    If i <> 1 Then '//restricted columns, 'i' is Your column index
        '        DataGridView1.Columns(i).ReadOnly = True
        '    End If
        'Next
        Dim ro As DataGridViewRow
        Dim n1 As Integer = 0
        For Each ro In DataGridView1.Rows

            DataGridView1.Rows(n1).HeaderCell.Value = (1 + n1).ToString
            DataGridView1.RowHeadersWidth = 40
            n1 += 1
        Next
        colo(ar)
        colo1(ar1)
    End Sub
    Private Sub colo(ByVal list As ArrayList)
        Dim num As Integer
        For Each num In list
            Me.DataGridView1.Rows(num).DefaultCellStyle.BackColor = Color.Teal
        Next
    End Sub
    Private Sub colo1(ByVal list As ArrayList)
        Dim num As Integer
        For Each num In list
            Me.DataGridView1.Rows(num).DefaultCellStyle.BackColor = Color.DimGray
        Next
    End Sub
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        signin.Close()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        HScrollBar1.Maximum = DataGridView1.Columns.Count - 1
        HScrollBar1.Minimum = 0
        HScrollBar1.LargeChange = 1
        TAChart1.Annotations.Add(CurrentAnnotation)
        fast()
        Label8.Text = "ID : " + signin.id
        Dim wClient As New WebClient

        wClient.Encoding = Encoding.UTF8
        wClient.UseDefaultCredentials = True
        wClient.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim result = wClient.DownloadString("http://engco.tk/handler.php?action=name&StId=" + signin.id + "&Stpasss=" + signin.password)
        Label7.Text = "Name : " + result
        perohead.Label1.Text = "NAME : " + result


        Dim result0 = wClient.DownloadString("http://engco.tk/handler.php?action=mobile&StId=" + signin.id + "&Stpasss=" + signin.password)
        Label9.Text = "mobile : " + result0

        Dim result1 = wClient.DownloadString("http://engco.tk/handler.php?action=Facebook&StId=" + signin.id + "&Stpasss=" + signin.password)
        fac = result1

        Dim result2 = wClient.DownloadString("http://engco.tk/handler.php?action=Balance&StId=" + signin.id + "&Stpasss=" + signin.password)
        perohead.Label3.Text = "Balance:  " & result2 & " Ec"
        Label10.Text = result2 & " Ec"

        Dim result3 = wClient.DownloadString("http://engco.tk/handler.php?action=wallet&StId=" + signin.id + "&Stpasss=" + signin.password)
        TextBox1.Text = result3
        TextBox7.Text = result3
        TextBox21.Text = result3
        perohead.Label2.Text = "ID    : " + signin.id
        Dim result4 = wClient.DownloadString("http://engco.tk/handler.php?action=Signture&StId=" + signin.id + "&Stpasss=" + signin.password)
        TextBox2.Text = result4
        TextBox6.Text = result4
        TextBox20.Text = result4

        MessageBox.Show(result2 Mod 10)
        sm(result2)

        For i As Integer = 1 To 25
            Dim rando(2), r1(2), r2(2), r3(2), r4(2), r5(2) As String

            Dim chars = "0123456789"
            Dim random = New Random(Guid.NewGuid().GetHashCode())
            Dim randomString = New String(Enumerable.Repeat(chars, 2).[Select](Function(s) s(random.[Next](s.Length))).ToArray())
            rando(2) = randomString
            r1(2) = (randomString + 5) * 0.2
            r2(2) = (randomString - 5) * 0.2
            r3(2) = (randomString + 30) * 0.2
            r4(2) = (randomString + 20) * 0.2
            DGVH1.Rows().Add({rando(2), r1(2), r2(2), r3(2), r4(2)})

        Next
        Chart1_Click(sender, e)

        NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
        NotifyIcon1.BalloonTipText = "Name : " + result & vbCrLf & "mobile : " + result0
        NotifyIcon1.BalloonTipTitle = "Welcome Mr : " + result
        NotifyIcon1.ShowBalloonTip(1000)
        NotifyIcon1.Icon = New System.Drawing.Icon("D:\Data\blogger.ico")
        NotifyIcon1.Text = "My applicaiton"
        NotifyIcon1.Visible = True

    End Sub

    Sub sm(ByVal result2 As String)
        Dim nm = 0
        Dim mmb As Integer = 0
        ListBox1.Items.Clear()
        If result2 > 1 Then
            nm = 1
            mmb = Math.Round((result2 / nm), 0) - 1
        End If
        If result2 > 10 Then
            nm = 10
            mmb = Math.Round((result2 / nm), 0) - 1
        End If
        If result2 > 100 Then
            nm = 100
            mmb = Math.Round((result2 / nm), 0) - 1
        End If
        If result2 > 1000 Then
            nm = 1000
            mmb = Math.Round((result2 / nm), 0) - 1
        End If
        If result2 > 10000 Then
            nm = 10000
            mmb = Math.Round((result2 / nm), 0) - 1
        End If
        If result2 > 100000 Then
            nm = 100000
            mmb = Math.Round((result2 / nm), 0) - 1
        End If
        If result2 > 1000000 Then
            nm = 1000000
            mmb = Math.Round((result2 / nm), 0) - 1
        End If
        If result2 > 10000000 Then
            nm = 10000000
            mmb = Math.Round((result2 / nm), 0) - 1
        End If
        Dim randoms(mmb) As String

        For i As Integer = 0 To randoms.Length - 1
            Dim chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz"
            Dim random = New Random(Guid.NewGuid().GetHashCode())
            Dim randomString = New String(Enumerable.Repeat(chars, 23).[Select](Function(s) s(random.[Next](s.Length))).ToArray())
            randoms(mmb) = randomString
            ListBox1.Items.Add(nm & " Coin     " & randoms(mmb))
        Next





    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        fast()
    End Sub
    Public Shared gt As String
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        nofdetails.Close()
        If e.RowIndex = 0 Then
            gt = 1
        Else
            Dim v As String = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            gt = e.RowIndex + 1
            nofdetails.Show()
        End If




    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        reqcrypt.Show()
    End Sub
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        gtrealmoney.Show()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        findsomeonewallet.Show()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        ContextMenuStrip1.Visible = True
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub إرسالعملاتToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles إرسالعملاتToolStripMenuItem.Click
        sendcryptoother.Show()
    End Sub

    Private Sub شحنعملاتToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles شحنعملاتToolStripMenuItem.Click
        reqcrypt.Show()
    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        notification.Show()
        sendcryptoother.Show()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim coll, col, col1, col2, col3, col4, col5, col6, col7, col8, col9, col10, col11 As New DataGridViewTextBoxColumn
        coll.DataPropertyName = "PropertyName"
        coll.HeaderText = "مسلسل"
        coll.Name = "nam2"
        DataGridView1.Columns.Add(coll)
        DataGridView1.Columns(0).Width = 130
        col.DataPropertyName = "PropertyName"
        col.HeaderText = "التاريخ"
        col.Name = "nam"
        DataGridView1.Columns.Add(col)
        DataGridView1.Columns(1).Width = 130
        col2.DataPropertyName = "PropertyName"
        col2.HeaderText = "البرنامج"
        col2.Name = "age"
        DataGridView1.Columns.Add(col2)
        'DataGridView1.Columns(1).Width = 450
        col3.DataPropertyName = "PropertyName3"
        col3.HeaderText = "كمية الشغل"
        col3.Name = "adrs"
        DataGridView1.Columns.Add(col3)
        'DataGridView1.Columns(2).Width = 250
        col4.DataPropertyName = "PropertyName4"
        col4.HeaderText = "وقت التسليم"
        col4.Name = "address"
        DataGridView1.Columns.Add(col4)
        '  DataGridView1.Columns(3).Width = 350
        col5.DataPropertyName = "PropertyName5"
        col5.HeaderText = "سعر الشغل"
        col5.Name = "phone"
        DataGridView1.Columns.Add(col5)
        ' DataGridView1.Columns(4).Width = 150
        col6.DataPropertyName = "PropertyName6"
        col6.HeaderText = "طريقة الدفع"
        col6.Name = "fax"
        DataGridView1.Columns.Add(col6)
        col7.DataPropertyName = "PropertyName7"
        col7.HeaderText = "الدفع"
        col7.Name = "email"
        DataGridView1.Columns.Add(col7)
        DataGridView1.Columns(7).Width = 60
        col8.DataPropertyName = "PropertyName8"
        col8.HeaderText = "الحالة"
        col8.Name = "urlface"
        DataGridView1.Columns.Add(col8)

        DataGridView1.Rows.Add({"100", "100"})

    End Sub
    Dim wclient As New WebClient
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        TextBox9.Visible = False
        TextBox8.Visible = False
        Label21.Visible = False
        Label22.Visible = False
        Label19.Visible = False
        Label18.Visible = False
        ComboBox1.Visible = False
        PictureBox6.Visible = False
        PictureBox2.Visible = True
        Label35.Visible = True
        Button10.Visible = False
        ex = 150
        ey = 150
        Timer1.Start()
        Dim result As String = wclient.DownloadString("http://engco.tk/handler.php?action=req&Stamount=" + ComboBox1.Text + "&Stmsg=" + _
                                                      TextBox8.Text + "&Stwallet=" + TextBox9.Text)

        If result = "0" Then
            Timer1.Stop()

            MessageBox.Show("تم ارسال الطلب بنجاح")
            Timer1.Stop()
            TextBox9.Visible = True
            TextBox8.Visible = True
            Label21.Visible = True
            Label22.Visible = True
            Label19.Visible = True
            Label18.Visible = True
            ComboBox1.Visible = True
            PictureBox6.Visible = True
            PictureBox2.Visible = False
            Label35.Visible = False
            Button10.Visible = True
        End If
    End Sub
    Dim x1 As Integer = 100
    Dim x2 As Integer = 95
    Dim y1 As Integer = 100
    Dim y2 As Integer = 95

    Dim degr As Integer = 5

    Dim bm As New Bitmap(1000, 1000)
    Dim graph As Graphics = Graphics.FromImage(bm)

    Dim ex As Integer
    Dim ey As Integer
    Public Function D2R(ByVal Angle As Single) As Single
        D2R = Angle / 180 * Math.PI
    End Function
    Public Function RotatePoint(ByRef pPoint As Point, ByVal Degrees As Single) As Point
        RotatePoint.X = Math.Cos(D2R(Degrees)) * pPoint.X - Math.Sin(D2R(Degrees)) * pPoint.Y
        RotatePoint.Y = Math.Sin(D2R(Degrees)) * pPoint.X + Math.Cos(D2R(Degrees)) * pPoint.Y

    End Function
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        graph.Clear(Color.Transparent)
        For i As Integer = 0 To 270
            Dim pt1 As New Point(RotatePoint(New Point(x1, y1), degr))
            Dim pt2 As New Point(RotatePoint(New Point(x2, y2), degr))
            Dim pn As New Pen(Brushes.Transparent, 5)

            Dim pn1 As New Pen(Color.FromArgb(45, 47, 49), 5)
            Dim pn2 As New Pen(Brushes.Red, 5)
            graph.DrawLine(pn, x1 + ex, y1 + ey, x2 + ex, y2 + ey)
            If i >= 90 Then
                graph.DrawLine(pn1, pt1.X + ex, pt1.Y + ey, pt2.X + ex, pt2.Y + ey)
            Else
                '  graph.DrawLine(pn2, pt1.X + ex, pt1.Y + ey, pt2.X + ex, pt2.Y + ey)
            End If


            PictureBox2.Image = bm
            pic.Image = bm
            pic1.Image = bm
            degr = degr + 1
        Next
    End Sub
    Dim lab, lab1 As New Label
    Dim pic, pic1 As New PictureBox
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        TextBox16.Visible = False
        TextBox17.Visible = False
        TextBox18.Visible = False
        TextBox19.Visible = False
        TextBox20.Visible = False
        TextBox21.Visible = False
        Button11.Visible = False

        Label30.Visible = False
        Label31.Visible = False
        Label24.Visible = False
        Label25.Visible = False
        Label26.Visible = False
        Label27.Visible = False

        PictureBox7.Visible = False

        'lab.Name = "mmmm"
        'lab.Size = New Point(245, 45)
        'lab.Location = New Point(270, 153)
        'lab.Text = "برجاء الانتظار"
        'lab.Font = New System.Drawing.Font("Tahoma", 27.0!)
        'lab.ForeColor = System.Drawing.Color.Cyan

        'pic.Name = "nn"
        'pic.Size = New Point(400, 300)
        'pic.Location = New Point(234, 37)

        'Me.TabPage5.Controls.Add(lab)
        'Me.TabPage5.Controls.Add(pic)
        ex = 150
        ey = 150
        ' Timer1.Start()

        Dim result As String = wclient.DownloadString("http://engco.tk/handler.php?action=rmoney&Stwalletn=" + TextBox21.Text + _
                                                      "&Stsginture=" + TextBox20.Text + "&Stpassword=" + TextBox19.Text + _
                                                      "&Stcoin=" + TextBox18.Text + "&Stnumber=" + TextBox17.Text + "&Stmsgn=" + TextBox16.Text)

        '(`$"&Stwallet`,`$"&Stsginture="`,`$"&Stpassword="`,`$"&Stcoin="`,`$"&Stnumber="`,`$"&Stmsg="`)

        If result = "0" Then
            Timer1.Stop()

            MessageBox.Show("تم ارسال الطلب بنجاح")
            TextBox16.Visible = True
            TextBox17.Visible = True
            TextBox18.Visible = True
            TextBox19.Visible = True
            TextBox20.Visible = True
            TextBox21.Visible = True
            Button11.Visible = True

            Label30.Visible = True
            Label31.Visible = True
            Label24.Visible = True
            Label25.Visible = True
            Label26.Visible = True
            Label27.Visible = True

            PictureBox7.Visible = True
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        TextBox3.Visible = False
        TextBox4.Visible = False
        TextBox5.Visible = False
        TextBox6.Visible = False
        TextBox7.Visible = False
        TextBox15.Visible = False
        Button9.Visible = False
        PictureBox1.Visible = False
        Label13.Visible = False
        Label14.Visible = False
        Label15.Visible = False
        Label16.Visible = False
        Label17.Visible = False
        Label28.Visible = False


        Dim result2 = wclient.DownloadString("http://engco.tk/handler.php?action=Balance&StId=" + signin.id + "&Stpasss=" + signin.password)


        'lab1.Name = "mmm"
        'lab1.Size = New Point(245, 45)
        'lab1.Location = New Point(270, 153)
        'lab1.Text = "برجاء الانتظار"
        'lab1.Font = New System.Drawing.Font("Tahoma", 27.0!)
        'lab1.ForeColor = System.Drawing.Color.Cyan

        'pic1.Name = "nnm"
        'pic1.Size = New Point(400, 300)
        'pic1.Location = New Point(234, 37)

        'Me.TabPage3.Controls.Add(lab1)
        'Me.TabPage3.Controls.Add(pic1)
        'ex = 150
        'ey = 150
        Timer1.Start()
        sm(result2)
        If TextBox7.Text = TextBox5.Text Then
            MessageBox.Show("برجاء كتابة محفظة المرسل اليه")

        ElseIf TextBox3.Text = "" Or
            TextBox4.Text = "" Or
            TextBox5.Text = "" Or
            TextBox6.Text = "" Or
            TextBox7.Text = "" Or
            TextBox15.Text = "" Then
            MessageBox.Show("برجاء كتابة البيانات كاملة")

        Else
            Dim result As String = wclient.DownloadString("http://engco.tk/handler.php?action=updmoney&StId=" + signin.id + _
                   "&Stpasss=" + TextBox3.Text + _
                   "&Stwallet=" + TextBox7.Text + _
                   "&StSignture=" + TextBox6.Text + _
                   "&Stwalletn=" + TextBox5.Text + _
                   "&Stsent=" + TextBox4.Text)
            If result = "00" Then
                Timer1.Stop()

                MessageBox.Show(TextBox4.Text & "= تم ارسال مبلغ")
                Dim resu2 = wclient.DownloadString("http://engco.tk/handler.php?action=Balance&StId=" + signin.id + "&Stpasss=" + signin.password)
                perohead.Label3.Text = "Balance:  " & resu2 & " Ec"
                Label10.Text = resu2 & " Ec"
                sm(resu2)
            Else
                MessageBox.Show("برجاء مراجعة البيانات المدخلة")

            End If

        End If
        TextBox3.Visible = True
        TextBox4.Visible = True
        TextBox5.Visible = True
        TextBox6.Visible = True
        TextBox7.Visible = True
        TextBox15.Visible = True
        Button9.Visible = True
        PictureBox1.Visible = True
        Label13.Visible = True
        Label14.Visible = True
        Label15.Visible = True
        Label16.Visible = True
        Label17.Visible = True
        Label28.Visible = True


    End Sub
    Private Sub HScrollBar1_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs) Handles HScrollBar1.Scroll
        'Use the DataGridView.FirstDisplayedCell property to scroll the display
        DataGridView1.FirstDisplayedScrollingColumnIndex = e.NewValue
    End Sub


    Private Sub Chart1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TAChart1.Click
        TAChart1.ChartAreas(0).AxisY.Minimum = 0
        TAChart1.ChartAreas(0).AxisY.Maximum = 200
        Dim rowC As Integer = DGVH1.RowCount - 1

        With TAChart1
            .Legends.Clear()
            .Series.Clear()
        End With
        TAChart1.Series.Add("Open")
        TAChart1.Series(0).ChartType = SeriesChartType.Candlestick

        Dim Max1 As Long = 0
        For i As Integer = 0 To rowC - 1
            If Max1 < DGVH1.Rows(i).Cells(2).Value Then Max1 = DGVH1.Rows(i).Cells(2).Value
        Next

        Dim Min1 As Long = Max1
        For i As Integer = 0 To rowC - 1
            If Min1 > DGVH1.Rows(i).Cells(3).Value Then Min1 = DGVH1.Rows(i).Cells(3).Value
        Next

        '0 Name
        '1 open
        '2 high
        '3 low
        '4 close

        Max1 = Max1 * 0.01 + Max1
        Min1 = Min1 - Min1 * 0.01


        For Count As Integer = 0 To rowC - 1
            TAChart1.Series(0).Points.AddXY(DGVH1.Item(0, Count).Value, DGVH1.Item(2, Count).Value, DGVH1.Item(3, Count).Value, DGVH1.Item(1, Count).Value, DGVH1.Item(4, Count).Value)
        Next



        With TAChart1.ChartAreas(0)
            .AxisX.Title = "Date"
            .AxisY.Title = "Units"
            '.AxisY.Minimum = Min1
            '.AxisY.Maximum = Max1
            '.AxisY2.Minimum = Min1
            '.AxisY2.Maximum = Max1
            .AxisX.MajorGrid.Enabled = False
            .AxisY.MajorGrid.LineColor = Color.FromArgb(45, 45, 45)
            .AxisY2.MajorGrid.LineWidth = 0
            .AxisX.IsReversed = True
            .BackColor = Color.FromArgb(64, 64, 64)
            .AxisX.LineWidth = 0
            .AxisY.LineWidth = 0
            .AxisY2.LineWidth = 0
            .AxisX.LabelStyle.ForeColor = Color.Orange
            .AxisY.LabelStyle.ForeColor = Color.Orange
            '.AxisX.LabelStyle.Font = New Font("Cambria", 8, FontStyle.Regular)
            '.AxisY.LabelStyle.Enabled = False
            '.AxisY2.LabelStyle.Enabled = False
            '.AxisX.MajorTickMark.Enabled = False
            '.AxisY.MajorTickMark.Enabled = False
            '.AxisY2.MajorTickMark.Enabled = False
            '.AxisX.MinorTickMark.Enabled = False
            '.AxisY.MinorTickMark.Enabled = False
            '.AxisY2.MinorTickMark.Enabled = False
            .AxisX.LabelStyle.Angle = -90
            .AxisX.Interval = 1
        End With

        With TAChart1.Series(0)
            .BorderColor = Color.FromArgb(64, 64, 64)
            .Color = Color.Tomato
        End With

        For i As Integer = 0 To rowC - 1
            TAChart1.Series(0).Points(i).Color = Color.Green
            If DGVH1.Rows(i + 1).Cells(1).Value > DGVH1.Rows(i + 1).Cells(4).Value Then
                TAChart1.Series(0).Points(i).BorderColor = Color.FromArgb(64, 64, 64)
                TAChart1.Series(0).Points(i).Color = Color.Green
                TAChart1.Series(0).Points(i).LabelForeColor = Color.Green
            End If

            If DGVH1.Rows(i + 1).Cells(1).Value < DGVH1.Rows(i + 1).Cells(4).Value Then
                TAChart1.Series(0).Points(i).BorderColor = Color.FromArgb(64, 64, 64)
                TAChart1.Series(0).Points(i).Color = Color.Red
                TAChart1.Series(0).Points(i).LabelForeColor = Color.Red
            End If
        Next
        TAChart1.BackColor = Color.FromArgb(64, 64, 64)
    End Sub

    Private Sub DGVH1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        'DGVH1.Rows.Clear()
        'DGVH1.Columns.Clear()
        'Dim Today0 As Date = Date.Today
        'Dim Day0 As Integer = Today0.Day
        'Dim Month0 As Integer = Today0.Month - 1
        'Dim Year0 As Integer = Today0.Year
        'Dim Year10 As Integer = Today0.Year - 10

        'Dim MonthFormat As String = "MMM"
        'Dim MonthDate As String = (Today0.ToString(MonthFormat))

        'Dim myUri As New Uri("http://engco.tk/handler.php?action=prev")
        'Dim request As HttpWebRequest = DirectCast(WebRequest.Create(myUri), HttpWebRequest)
        'Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
        'Dim receiveStream As Stream = response.GetResponseStream()

        'Dim TextFieldParser1 As New Microsoft.VisualBasic.FileIO.TextFieldParser(receiveStream)
        ''Dim TextFieldParser1 As New Microsoft.VisualBasic.FileIO.TextFieldParser("c:\AAPL Balance SheetA.csv")
        'TextFieldParser1.Delimiters = New String() {","}
        'While Not TextFieldParser1.EndOfData
        '    Dim Row1 As String() = TextFieldParser1.ReadFields()

        '    If DGVH1.Columns.Count = 0 AndAlso Row1.Count > 0 Then
        '        Dim i As Integer

        '        For i = 0 To 5
        '            DGVH1.Columns.Add("Column" & i + 1, "Column" & i + 1)
        '        Next
        '    End If

        '    DGVH1.Rows.Add(Row1)
        'End While

        'receiveStream.Close()
        'response.Close()

        'code below converts everything to numbers...some random values are not numerical (strings) 


    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim randoms(2), r1(2), r2(2), r3(2), r4(2), r5(2) As String

        For i As Integer = 1 To 50

            Dim chars = "0123456789"
            Dim random = New Random(Guid.NewGuid().GetHashCode())
            Dim randomString = New String(Enumerable.Repeat(chars, 2).[Select](Function(s) s(random.[Next](s.Length))).ToArray())
            randoms(2) = randomString
            r1(2) = (randomString + 5) * 0.2
            r2(2) = (randomString - 5) * 0.2
            r3(2) = (randomString + 20) * 0.2
            r4(2) = (randomString + 20) * 0.2
            DGVH1.Rows().Add({randoms(2), r1(2), r2(2), r3(2), r4(2)})

        Next

    End Sub
    Private Sub chart1_GetToolTipText(ByVal sender As Object, ByVal e As ToolTipEventArgs) Handles TAChart1.GetToolTipText
        ' Check selected chart element and set tooltip text for it
        Select Case e.HitTestResult.ChartElementType
            Case ChartElementType.DataPoint
                Dim dataPoint = e.HitTestResult.Series.Points(e.HitTestResult.PointIndex)
                Dim result As HitTestResult = TAChart1.HitTest(e.X, e.Y)
                'e.Text = "Closed On " & dataPoint.YValues(0).ToString & vbCrLf & "Close: " & TAChart1.Series(result.Series.Name).Points(result.PointIndex).YValues(3) & _
                ' vbLf & "Change: " &
                '         (TAChart1.Series(result.Series.Name).Points(result.PointIndex).YValues(3) -
                '         TAChart1.Series(result.Series.Name).Points(result.PointIndex).YValues(2)).ToString
                vs = dataPoint.YValues(0).ToString
                Exit Select
        End Select

        'If result.ChartElementType = ChartElementType.DataPoint Then

        '    TAChart1.Invalidate()
        'End If
    End Sub
    Private CurrentAnnotation As New CalloutAnnotation
    Dim vs
    Dim stripline As StripLine = New StripLine()

    Private Sub Chart1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TAChart1.MouseMove
        CurrentAnnotation.Visible = True

        stripline.Interval = 0
        stripline.IntervalOffset = 180 - e.Y
        stripline.StripWidth = 0.2
        stripline.BackColor = Color.Blue
        TAChart1.ChartAreas(0).AxisY.StripLines.Add(stripline)




        Dim result As HitTestResult = TAChart1.HitTest(e.X, e.Y)

        If result.ChartElementType = ChartElementType.DataPoint Then
            With CurrentAnnotation
                .CalloutStyle = CalloutStyle.Borderline
                .ForeColor = Color.DarkCyan
                .Font = New Font("Tahoma", 10)
                .BackColor = Color.Black
                .AnchorDataPoint = TAChart1.Series(result.Series.Name).Points(result.PointIndex)
                '  .X = TAChart1.Series(result.Series.Name).Points(result.PointIndex).XValue
                ' .Y = TAChart1.Series(result.Series.Name).Points(result.PointIndex).YValues(3)
                .Text = "Start: " & vs & vbLf & "Close: " & TAChart1.Series(result.Series.Name).Points(result.PointIndex).YValues(3) &
                    vbLf & "Change: " &
                    (TAChart1.Series(result.Series.Name).Points(result.PointIndex).YValues(3) -
                    TAChart1.Series(result.Series.Name).Points(result.PointIndex).YValues(2)).ToString
            End With

        End If




        TAChart1.Invalidate()
    End Sub


    Private Sub growthChart_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles TAChart1.MouseEnter
        TAChart1.Focus()
    End Sub


    Private Sub growthChart_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TAChart1.MouseWheel
        Try
            With TAChart1
                If (e.Delta < 0) Then
                    .ChartAreas(0).AxisX.ScaleView.ZoomReset()
                    .ChartAreas(0).AxisY.ScaleView.ZoomReset()
                End If

                If (e.Delta > 0) Then
                    Dim xMin As Double = .ChartAreas(0).AxisX.ScaleView.ViewMinimum
                    Dim xMax As Double = .ChartAreas(0).AxisX.ScaleView.ViewMaximum
                    Dim yMin As Double = .ChartAreas(0).AxisY.ScaleView.ViewMinimum
                    Dim yMax As Double = .ChartAreas(0).AxisY.ScaleView.ViewMaximum
                    Dim posXStart As Double = (.ChartAreas(0).AxisX.PixelPositionToValue(e.Location.X) _
                                - ((xMax - xMin) _
                                / 4))
                    Dim posXFinish As Double = (.ChartAreas(0).AxisX.PixelPositionToValue(e.Location.X) _
                                + ((xMax - xMin) _
                                / 4))
                    Dim posYStart As Double = (.ChartAreas(0).AxisY.PixelPositionToValue(e.Location.Y) _
                                - ((yMax - yMin) _
                                / 4))
                    Dim posYFinish As Double = (.ChartAreas(0).AxisY.PixelPositionToValue(e.Location.Y) _
                                + ((yMax - yMin) _
                                / 4))
                    .ChartAreas(0).AxisX.ScaleView.Zoom(posXStart, posXFinish)
                    .ChartAreas(0).AxisY.ScaleView.Zoom(posYStart, posYFinish)
                End If
            End With


        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Dim randoms(2), r2(2), r3(2), r4(2), r5(2) As String
    Dim r1 As String = 0
    Dim working As Boolean
    Dim Count As Integer = 0
    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick

        Dim chars = "0123456789"
        Dim random = New Random(Guid.NewGuid().GetHashCode())
        Dim randomString = New String(Enumerable.Repeat(chars, 2).[Select](Function(s) s(random.[Next](s.Length))).ToArray())
        randoms(2) = randomString
        'r1(2) = (randomString + 5) * 0.2
        r2(2) = (randomString)
        r3(2) = (randomString)
        r4(2) = (randomString)

        Label40.Text = r3(2)
        Count += 1
        'DGVH1.Rows.Insert(0, {Name , Open, Endline, Startline, Close})
        DGVH1.Rows.Insert(0, {"C" & Count, r1, r3(2), r2(2), r4(2)})
        DGVH1.Rows.RemoveAt(DGVH1.Rows.Count - 2)
        Chart1_Click(sender, e)
        r1 = r4(2)
        working = True
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If working = True Then
            Timer3.Stop()
            Button15.BackColor = Color.Red
            Button15.Text = "معطل"
            working = False
        ElseIf working = False Then
            Timer3.Start()
            Button15.Text = "يعمل"
            Button15.BackColor = Color.Lime
            working = True
        End If

    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim stripline As StripLine = New StripLine()
        stripline.Interval = 0
        stripline.IntervalOffset = Label40.Text
        stripline.StripWidth = 0.3
        stripline.BackColor = Color.Black
        TAChart1.ChartAreas(0).AxisY.StripLines.Add(stripline)
        updowncureency.Show()
        updowncureency.DGVH1.Rows.Insert(0, {"C" & Count, r1, r3(2), r2(2), r4(2)})
        Dim anno As New DataVisualization.Charting.TextAnnotation
        anno.AllowTextEditing = True
        anno.AllowSelecting = True
        anno.AllowMoving = True
        anno.AllowResizing = True
        anno.X = 90
        anno.Y = Label40.Text
        anno.Text = "Point : " & Label40.Text
        TAChart1.Annotations.Add(anno)
    End Sub

    Private Sub TAChart1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TAChart1.MouseLeave
        CurrentAnnotation.Visible = False
    End Sub
End Class
