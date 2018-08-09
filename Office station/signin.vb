Imports System.Net
Imports System.IO
Imports System.Text

Public Class signin

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

    Private Sub Form6_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
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
    Private Sub Form6_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Resize
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
    Private Sub Form6_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
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

    Private Sub Form6_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        'Move and refresh.

        If (hh <> 0) Then
            Me.Location = New Point(Control.MousePosition.X - x000, Control.MousePosition.Y - y000)

        End If


    End Sub

    Private Sub Form6_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
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
    Private Sub Form6_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDoubleClick
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
    Public Shared id
    Public Shared password
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        signup.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' $StId','$StName','$Stmobile','$Stpasss','$Stfacebook
        On Error Resume Next
        Dim result As String = wclient.DownloadString("http://engco.tk/handler.php?action=login&StId=" + TextBox1.Text + "&Stpasss=" + TextBox2.Text)
        id = TextBox1.Text
        password = TextBox2.Text
        If result = "1" Then
            id = TextBox1.Text
            password = TextBox2.Text
            MessageBox.Show("اهلا بك")
            main.Show()
            Me.Hide()
        ElseIf result = "0" Then
            MessageBox.Show("كلمة المرور غير صحيحة اعد المحاولة")
        End If
    End Sub

    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        main.Close()

        Dim objReader As New System.IO.StreamReader("C:\Users\Ihab majdy\Desktop\0000.txt", Encoding.Default)
        Dim lineCount As Integer 'lines read so far in file
        Dim filename As String = String.Empty
        Dim TextLine As String = ""

        Do While objReader.Peek() <> -1
            TextLine = objReader.ReadLine()
            If lineCount = 0 Then TextBox1.Text = TextLine
            If lineCount = 1 Then TextBox2.Text = TextLine

            lineCount = lineCount + 1 'increment lineCount
        Loop

    End Sub
End Class