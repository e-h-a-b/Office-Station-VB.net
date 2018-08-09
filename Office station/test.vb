Public Class test
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Label1.Text = TextBox1.TextLength 
        Dim ritxt As New TextBox 
        ritxt.Text = TextBox1.Text
        Dim cellvalue(200) As String
        For i As Integer = 0 To ritxt.Lines.Length - 1
            Dim row = ritxt.Lines(i) & If(i < ritxt.Lines.Length - 1, Environment.NewLine, "")
            cellvalue = row.Split("."c) 'check what is ur separator
            Label2.Text = cellvalue(0)
            Label3.Text = cellvalue(1)
        Next 
        Label1.Text = TextBox1.TextLength - Label3.Text.Count - 1
        Label4.Text = TextBox1.Lines.First.Chars(0)
        For i As Integer = 1 To Label1.Text - 1
            Label5.Text = Label5.Text & TextBox1.Lines.First.Chars(i)
        Next

        TextBox2.Text = money(Label1.Text, Label4.Text)
        TextBox2.Text = TextBox2.Text & "." & Label3.Text

        TextBox3.Text = Label5.Text

        Dim mm As Integer
        For h As Integer = 0 To Label5.Text.Length - 1
            mm = mm & TextBox3.Lines.First.Chars(h)

        Next
        TextBox2.Text = TextBox2.Text + mm
    End Sub
    Sub clear()
        Label1.Text = ""
        Label2.Text = ""
        Label3.Text = ""
        Label4.Text = ""
        Label5.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
    Function money(ByVal num As Integer, ByVal first As Integer)
        Dim numm
        If num = 1 Then numm = first * 1
        If num = 2 Then numm = first * 10
        If num = 3 Then numm = first * 100
        If num = 4 Then numm = first * 1000
        If num = 5 Then numm = first * 10000
        If num = 6 Then numm = first * 100000
        If num = 7 Then numm = first * 1000000
        If num = 8 Then numm = first * 10000000
        If num = 9 Then numm = first * 100000000
        If num = 10 Then numm = first * 1000000000
        If num = 11 Then numm = first * 10000000000
        If num = 12 Then numm = first * 100000000000
        If num = 13 Then numm = first * 1000000000000
        If num = 14 Then numm = first * 10000000000000 
        Return numm
    End Function
    Function identf(ByVal nm As TextBox)
        nm.Text = nm.Text.Replace("0", "aЖH*gK")
        nm.Text = nm.Text.Replace("1", "bV*WЖ")
        nm.Text = nm.Text.Replace("2", "дcX-Zq")
        nm.Text = nm.Text.Replace("3", "dO+PдI")
        nm.Text = nm.Text.Replace("4", "eЩR+ss")
        nm.Text = nm.Text.Replace("5", "fm-uЩy")
        nm.Text = nm.Text.Replace("6", "g#|@tЭ")
        nm.Text = nm.Text.Replace("7", "hЭ)/&n")
        nm.Text = nm.Text.Replace("8", "i}\{§l")
        nm.Text = nm.Text.Replace("9", "j?§/?>")
    End Function
    Function returnidte(ByVal nm As TextBox)
        nm.Text = nm.Text.Replace("aЖH*gK", "0")
        nm.Text = nm.Text.Replace("bV*WЖ", "1")
        nm.Text = nm.Text.Replace("дcX-Zq", "2")
        nm.Text = nm.Text.Replace("dO+PдI", "3")
        nm.Text = nm.Text.Replace("eЩR+ss", "4")
        nm.Text = nm.Text.Replace("fm-uЩy", "5")
        nm.Text = nm.Text.Replace("g#|@tЭ", "6")
        nm.Text = nm.Text.Replace("hЭ)/&n", "7")
        nm.Text = nm.Text.Replace("i}\{§l", "8")
        nm.Text = nm.Text.Replace("j?§/?>", "9")
    End Function
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        clear()
    End Sub
    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        identf(TextBox2) 
    End Sub
    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        returnidte(TextBox2)
    End Sub
End Class