Imports System.Net
Imports System.Text

Public Class sendreq

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

    Private Sub Form2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
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
    Private Sub Form2_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Resize
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
    Private Sub Form2_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
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

    Private Sub Form2_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        'Move and refresh.

        If (hh <> 0) Then
            Me.Location = New Point(Control.MousePosition.X - x000, Control.MousePosition.Y - y000)

        End If


    End Sub

    Private Sub Form2_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
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
    Private Sub Form2_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDoubleClick
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




    Dim wclient As New WebClient
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' '$StId','$StName','$Stdatet','$Stprogram','$Stamount','$Sttimesent','$Stprice','$Stpaym','$Ststatupay','$Ststatu')
        Dim result As String = wclient.DownloadString("http://engco.tk/handler.php?action=Set&StId=" + TextBox1.Text + _
             "&Sttimesent=" + TextBox2.Text + "&Stprogram=" + TextBox3.Text + _
           "&Stamount=" + TextBox4.Text + "&Stprice=" + TextBox7.Text + "&Stpaym=" + TextBox5.Text + _
           "&Ststatupay=" + TextBox6.Text + "&Ststatu=" + ComboBox1.Text + "&Stfacebook=" + main.fac)
        fast()
        If result = "1" Then
            MessageBox.Show("Succssefuly Regristed")
        ElseIf result = "0" Then
            MessageBox.Show("There are Some Error")
        End If
    End Sub
    Sub fast()
        Dim ritxt As New TextBox
        main.DataGridView1.DataSource = Nothing
        'Remove Blank Lines
        main.DataGridView1.Rows.Clear()
        main.DataGridView1.Columns.Clear()
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
        main.DataGridView1.Columns.Add(coll)
        main.DataGridView1.Columns(0).Width = 130
        col.DataPropertyName = "PropertyName"
        col.DataPropertyName = "PropertyName"
        col.HeaderText = "التاريخ"
        col.Name = "nam"
        main.DataGridView1.Columns.Add(col)
        main.DataGridView1.Columns(0).Width = 130
        col2.DataPropertyName = "PropertyName"
        col2.HeaderText = "البرنامج المستخدم"
        col2.Name = "age"
        main.DataGridView1.Columns.Add(col2)
        'form1.DataGridView1.Columns(1).Width = 450
        col3.DataPropertyName = "PropertyName3"
        col3.HeaderText = "كمية الشغل"
        col3.Name = "adrs"
        main.DataGridView1.Columns.Add(col3)
        'form1.DataGridView1.Columns(2).Width = 250
        col4.DataPropertyName = "PropertyName4"
        col4.HeaderText = "وقت التسليم"
        col4.Name = "address"
        main.DataGridView1.Columns.Add(col4)
        '  form1.DataGridView1.Columns(3).Width = 350
        col5.DataPropertyName = "PropertyName5"
        col5.HeaderText = "سعر الشغل"
        col5.Name = "phone"
        main.DataGridView1.Columns.Add(col5)
        ' form1.DataGridView1.Columns(4).Width = 150
        col6.DataPropertyName = "PropertyName6"
        col6.HeaderText = "طريقة الدفع"
        col6.Name = "fax"
        main.DataGridView1.Columns.Add(col6)
        col7.DataPropertyName = "PropertyName7"
        col7.HeaderText = "الدفع"
        col7.Name = "email"
        main.DataGridView1.Columns.Add(col7)
        main.DataGridView1.Columns(6).Width = 60
        col8.DataPropertyName = "PropertyName8"
        col8.HeaderText = "الحالة"
        col8.Name = "urlface"
        main.DataGridView1.Columns.Add(col8)
        Dim gggg
        Dim ar As New ArrayList
        Dim ar1 As New ArrayList
        For i As Integer = 0 To ritxt.Lines.Length - 1
            If ritxt.Lines(i).Contains("جارى") Then
                gggg = i
                ar.Add(gggg)
                main.Label5.Text = ar.Count
            End If
            If ritxt.Lines(i).Contains("تام") Then
                gggg = i
                ar1.Add(gggg)
                main.Label6.Text = ar1.Count
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

                main.DataGridView1.Rows.Add(cellvalue)
                '                DataGridView1.Rows.Add(TextLine.Split(" ")(0), TextLine.Replace(TextLine.Split(" ")(0), "").Substring(1))
                main.Label1.Text = ritxt.Lines.Length - 1

            End If

        Next

        main.DataGridView1.AutoResizeRows( _
          DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders)
        'For i = 0 To form1.DataGridView1.Columns.Count - 1
        '    If i <> 1 Then '//restricted columns, 'i' is Your column index
        '        form1.DataGridView1.Columns(i).ReadOnly = True
        '    End If
        'Next
        Dim ro As DataGridViewRow
        Dim n1 As Integer = 0
        For Each ro In main.DataGridView1.Rows

            main.DataGridView1.Rows(n1).HeaderCell.Value = (1 + n1).ToString
            main.DataGridView1.RowHeadersWidth = 40
            n1 += 1
        Next
        colo(ar)
        colo1(ar1)
    End Sub
    Private Sub colo(ByVal list As ArrayList)
        Dim num As Integer
        For Each num In list
            main.DataGridView1.Rows(num).DefaultCellStyle.BackColor = Color.LightSkyBlue
        Next
    End Sub
    Private Sub colo1(ByVal list As ArrayList)
        Dim num As Integer
        For Each num In list
            main.DataGridView1.Rows(num).DefaultCellStyle.BackColor = Color.LightGreen
        Next
    End Sub
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = signin.id
        TextBox8.Text = main.fac
    End Sub
End Class