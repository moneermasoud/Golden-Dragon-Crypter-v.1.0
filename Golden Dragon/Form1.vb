Imports System.Data.SqlClient

Public Class Form1
    Private dt As New DataTable
    Private da As New SqlDataAdapter
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Main.Show()

        Try
            con.Open()

            Dim query As String = "SELECT * FROM Users Where UserName=@UserName And User_Pass=@User_Pass "
            Dim cmd As New SqlCommand(query, con)
            Dim param(1) As SqlParameter
            'Sec SQL Injection 
            param(0) = New SqlParameter("@UserName", SqlDbType.VarChar, 150)
            param(0).Value = ComboBox1.Text

            param(1) = New SqlParameter("@User_Pass", SqlDbType.VarChar, 150)
            param(1).Value = TextBox1.Text

            cmd.Parameters.AddRange(param)

            reader = cmd.ExecuteReader ' Run Execute Reader 
            reader.Read()
            If reader.HasRows Then ' check if has rows 
                UserName = ComboBox1.Text
                UserID = reader(0)
                'Permes = reader(3)

                'MsgBox("ok")
                Main.Show()
                TextBox1.Clear()
                Me.Hide()

            Else

                MsgBox("اسم مستخدم أو كلمة المرور خاطئة ", MsgBoxStyle.Information, "تسجيل الدخول")


            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            reader.Close()
            con.Close()
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'values ComboBox

        Try
            Dim userread As String ' متغير لتخزين الاستعلام
            userread = "select * from Users " ' الاستعلام باصيته للمتغير

            da = New SqlDataAdapter(userread, con) ' هذا الدات ادرتبر 
            da.Fill(dt) ' حطيت الدات ادبتر في دات تيبل 
            ComboBox1.DataSource = dt
            ComboBox1.DisplayMember = "UserName"
            ComboBox1.SelectedIndex = 0
            ' بشكل مختصر هذا الكود باش نخزن المستخدمين في الكومبو بوكس 

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'ComboBox1.Text = My.Settings.con_str
    End Sub
End Class
