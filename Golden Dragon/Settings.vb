Imports System.Data
Imports System.Data.SqlClient

Public Class Settings
    'This is a fucking datatable 
    Private dt As New DataTable
    'This is a fucking SqlDataAdapter 
    Private da As SqlDataAdapter
    Private crr As Integer
    Private Index As Integer
    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        show_Users()
        TextBox1.Text = My.Settings.private_key
        TextBox2.Text = My.Settings.path_backup
    End Sub

    'Show Users for DataGridView1
    Private Sub show_Users()
        Try
            Dim SQLQuery As String
            SQLQuery = "SELECT * FROM Users"

            dt.Clear()

            da = New SqlDataAdapter(SQLQuery, con)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            With DataGridView1

                .Columns(0).HeaderText = "رقم المستخدم"
                .Columns(1).HeaderText = "أسم المستخدم"
                .Columns(2).HeaderText = "كلمة المرور"
                '.Columns(3).HeaderText = "الصلاحيات"

            End With
        Catch ex As Exception
            MsgBox("خطأإتصال بقواعد البيانات", MsgBoxStyle.Critical)

        End Try
    End Sub

    Private Sub cls()
        UserTxT.Clear()
        PassTXT.Clear()
        Label4.Visible = False
        Label5.Visible = False
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cls()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        cls()
        show_Users()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click
        Label5.Visible = True
        Label4.Visible = True

        '.........................................................................................
        ' ..........................................................................................
        If DataGridView1.CurrentRow.Index = dt.Rows.Count Or DataGridView1.Item(0, 0).Value = Nothing Then
            Exit Sub
        End If

        'TabControl1.SelectTab(0)
        crr = DataGridView1.CurrentRow.Index

        Label4.Text = DataGridView1.Item(0, crr).Value.ToString
        UserTxT.Text = DataGridView1.Item(1, crr).Value.ToString
        PassTXT.Text = DataGridView1.Item(2, crr).Value.ToString
        


        '   ==============================================================================================================================================

        'منع أي خطأ في الداتا قريد فيو عند عمل كلك على سطر فارغ أو حقل فارغ أو كلك يمين أو الفراغ 

        Try
            crr = DataGridView1.CurrentRow.Index
            Index = crr
            If DataGridView1.CurrentRow.Index = dt.Rows.Count Or DataGridView1.Item(0, 0).Value = Nothing Or DataGridView1.Item(0, crr).Value.ToString = vbNullString Then
                Exit Sub
            End If
        Catch When Err.Number = 91
            Exit Sub
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'شرط التحقق من المدخلات غير فارغة 
        If UserTxT.Text = vbNullString Then

            MsgBox("الرجاء أدخال أسم المستخدم", MsgBoxStyle.Information, "رسالة خطأ")
            Exit Sub
        End If
        If PassTXT.Text = vbNullString Then

            MsgBox("الرجاء أدخال كلمة المرور", MsgBoxStyle.Information, "رسالة خطأ")
            Exit Sub
        End If

        Try

            Dim strI3 As String = "INSERT INTO Users (UserName,User_Pass) VALUES ('" & UserTxT.Text & "','" & PassTXT.Text & "')"
            con.Open()
            Dim cmd3 As New SqlCommand(strI3, con)
            cmd3.ExecuteNonQuery()
            MsgBox("تمت الأضافة بنجاح ", MsgBoxStyle.Information, "الأضافة")
            show_Users()
            cls()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Label4.Text = vbNullString Then

            MsgBox("الرجاء تحديد المستخدم", MsgBoxStyle.Information, "رسالة خطأ")
            Exit Sub
        End If
        Try
            If MsgBox("هل تريد تأكيد عملية التحديث", MsgBoxStyle.YesNo, "التأكيد") = DialogResult.Yes Then
                Dim strdelete As String = "update Users set  UserName='" & UserTxT.Text & "', User_Pass='" & PassTXT.Text & "' where Id_user=" & Label4.Text
                con.Open()
                Dim cmddelete As New SqlCommand(strdelete, con)
                cmddelete.ExecuteNonQuery()
                MsgBox("تم التحديث بنجاح", MsgBoxStyle.Information, "التحديث")
                show_Users()
                cls()
            Else
                MsgBox("تم ألغاء عملية التحديث", MsgBoxStyle.Information, "عملية التحديث")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Label4.Text = vbNullString Then

            MsgBox("الرجاء تحديد المستخدم", MsgBoxStyle.Information, "رسالة خطأ")
            Exit Sub
        End If
        Try
            If MsgBox("هل تريد تأكيد عملية الحذف", MsgBoxStyle.YesNo, "التأكيد") = DialogResult.Yes Then
                Dim strdelete As String = "Delete From Users where Id_user=" & Label4.Text
                con.Open()
                Dim cmddelete As New SqlCommand(strdelete, con)
                cmddelete.ExecuteNonQuery()
                MsgBox("تم الحذف بنجاح", MsgBoxStyle.Information, "الحذف")
                show_Users()
                cls()

            Else
                MsgBox("تم ألغاء عملية الحذف", MsgBoxStyle.Information, "عملية الحذف")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        FolderBrowserDialog1.ShowDialog()
        TextBox2.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            My.Settings.path_backup = TextBox2.Text
            My.Settings.Save()
            MsgBox("تم حفظ التعديلات بنجاح ", MsgBoxStyle.Information, "Save Settings")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            My.Settings.private_key = TextBox1.Text
            My.Settings.Save()
            MsgBox("تم حفظ التعديلات بنجاح ", MsgBoxStyle.Information, "Save Settings")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class