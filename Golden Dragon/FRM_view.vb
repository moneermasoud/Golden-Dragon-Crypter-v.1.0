Imports System.Data.SqlClient

Public Class FRM_view
    Private dt As New DataTable
    Private da As SqlDataAdapter
    Private Sub FRM_view_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        show_Files()
        '
        If RadioButton1.Checked = True Then

            TextBox1.Enabled = True
            TextBox2.Enabled = True
            DateTimePicker1.Enabled = True

        End If

        If RadioButton2.Checked = True Then

            TextBox1.Enabled = False
            TextBox2.Enabled = True
            DateTimePicker1.Enabled = False

        End If

        If RadioButton3.Checked = True Then

            TextBox1.Enabled = True
            TextBox2.Enabled = False
            DateTimePicker1.Enabled = False

        End If

        If RadioButton4.Checked = True Then

            TextBox1.Enabled = False
            TextBox2.Enabled = False
            DateTimePicker1.Enabled = True

        End If



    End Sub

    Private Sub show_Files()
        Try
            Dim SQLQuery As String
            SQLQuery = "SELECT * FROM Encrypted_files"

            dt.Clear()

            da = New SqlDataAdapter(SQLQuery, con)
            da.Fill(dt)
            DataGridView1.DataSource = dt

        Catch ex As Exception
            MsgBox("خطأإتصال بقواعد البيانات", MsgBoxStyle.Critical)

        End Try
        With DataGridView1
            .Columns(0).HeaderText = "ID"
            .Columns(1).HeaderText = "الملف المشفر"
            .Columns(2).HeaderText = "مفتاح فك تشفير"
            .Columns(3).HeaderText = "التاريخ والوقت"


        End With
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Close()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then

            TextBox1.Enabled = True
            TextBox2.Enabled = True
            DateTimePicker1.Enabled = True

        End If

        If RadioButton2.Checked = True Then

            TextBox1.Enabled = False
            TextBox2.Enabled = True
            DateTimePicker1.Enabled = False

        End If

        If RadioButton3.Checked = True Then

            TextBox1.Enabled = True
            TextBox2.Enabled = False
            DateTimePicker1.Enabled = False

        End If

        If RadioButton4.Checked = True Then

            TextBox1.Enabled = False
            TextBox2.Enabled = False
            DateTimePicker1.Enabled = True

        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton1.Checked = True Then

            TextBox1.Enabled = True
            TextBox2.Enabled = True
            DateTimePicker1.Enabled = True

        End If

        If RadioButton2.Checked = True Then

            TextBox1.Enabled = False
            TextBox2.Enabled = True
            DateTimePicker1.Enabled = False

        End If

        If RadioButton3.Checked = True Then

            TextBox1.Enabled = True
            TextBox2.Enabled = False
            DateTimePicker1.Enabled = False

        End If

        If RadioButton4.Checked = True Then

            TextBox1.Enabled = False
            TextBox2.Enabled = False
            DateTimePicker1.Enabled = True

        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton1.Checked = True Then

            TextBox1.Enabled = True
            TextBox2.Enabled = True
            DateTimePicker1.Enabled = True

        End If

        If RadioButton2.Checked = True Then

            TextBox1.Enabled = False
            TextBox2.Enabled = True
            DateTimePicker1.Enabled = False

        End If

        If RadioButton3.Checked = True Then

            TextBox1.Enabled = True
            TextBox2.Enabled = False
            DateTimePicker1.Enabled = False

        End If

        If RadioButton4.Checked = True Then

            TextBox1.Enabled = False
            TextBox2.Enabled = False
            DateTimePicker1.Enabled = True

        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton1.Checked = True Then

            TextBox1.Enabled = True
            TextBox2.Enabled = True
            DateTimePicker1.Enabled = True

        End If

        If RadioButton2.Checked = True Then

            TextBox1.Enabled = False
            TextBox2.Enabled = True
            DateTimePicker1.Enabled = False

        End If

        If RadioButton3.Checked = True Then

            TextBox1.Enabled = True
            TextBox2.Enabled = False
            DateTimePicker1.Enabled = False

        End If

        If RadioButton4.Checked = True Then

            TextBox1.Enabled = False
            TextBox2.Enabled = False
            DateTimePicker1.Enabled = True

        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Clear()
        TextBox2.Clear()
        DateTimePicker1.ResetText()
        show_Files()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If RadioButton1.Checked = True Then
            Try
                SQLQuery = "SELECT * FROM Encrypted_files where Name_Path_file like '" & TextBox2.Text.Trim & "' AND Private_key like '" & TextBox1.Text.Trim & "' AND Date_f like '" & DateTimePicker1.Value.ToString("d/M/yyyy") & "'"
                CMD = New SqlCommand(SQLQuery, con)
                con.Open()
                Dim DR3 As SqlDataReader = CMD.ExecuteReader
                dt.Clear()
                dt.Load(DR3)
                DataGridView1.DataSource = dt
                con.Close()

                'dt_member.Clear()

                'da_member = New SqlDataAdapter(SQLQuery, con)
                'da_member.Fill(dt_member)
                'DataGridView1.DataSource = dt_member

            Catch ex As Exception

                MessageBox.Show(ex.Message)
            Finally
                con.Close()
            End Try

        End If

        If RadioButton2.Checked = True Then
            Try
                SQLQuery = "SELECT * FROM Encrypted_files where Name_Path_file like '%" & TextBox2.Text.Trim & "%'"
                CMD = New SqlCommand(SQLQuery, con)
                con.Open()
                Dim DR3 As SqlDataReader = CMD.ExecuteReader
                dt.Clear()
                dt.Load(DR3)
                DataGridView1.DataSource = dt
                con.Close()

                'dt_member.Clear()

                'da_member = New SqlDataAdapter(SQLQuery, con)
                'da_member.Fill(dt_member)
                'DataGridView1.DataSource = dt_member

            Catch ex As Exception

                MessageBox.Show(ex.Message)
            Finally
                con.Close()
            End Try
        End If



        If RadioButton3.Checked = True Then
            Try
                SQLQuery = "SELECT * FROM Encrypted_files where Private_key like '%" & TextBox1.Text.Trim & "%'"
                CMD = New SqlCommand(SQLQuery, con)
                con.Open()
                Dim DR3 As SqlDataReader = CMD.ExecuteReader
                dt.Clear()
                dt.Load(DR3)
                DataGridView1.DataSource = dt
                con.Close()

                'dt_member.Clear()

                'da_member = New SqlDataAdapter(SQLQuery, con)
                'da_member.Fill(dt_member)
                'DataGridView1.DataSource = dt_member

            Catch ex As Exception

                MessageBox.Show(ex.Message)
            Finally
                con.Close()
            End Try
        End If



        If RadioButton4.Checked = True Then
            Try
                SQLQuery = "SELECT * FROM Encrypted_files where Date_f like '%" & DateTimePicker1.Value.ToString("d/M/yyyy") & "%'"
                CMD = New SqlCommand(SQLQuery, con)
                con.Open()
                Dim DR3 As SqlDataReader = CMD.ExecuteReader
                dt.Clear()
                dt.Load(DR3)
                DataGridView1.DataSource = dt
                con.Close()

                'dt_member.Clear()

                'da_member = New SqlDataAdapter(SQLQuery, con)
                'da_member.Fill(dt_member)
                'DataGridView1.DataSource = dt_member

            Catch ex As Exception

                MessageBox.Show(ex.Message)
            Finally
                con.Close()
            End Try
        End If



    End Sub
End Class