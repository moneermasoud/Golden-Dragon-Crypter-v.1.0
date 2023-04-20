Imports System.Text
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography


Public Class FRM_encrypt

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

                For x = 0 To OpenFileDialog1.FileNames.Count - 1

                    'MsgBox(OpenFileDialog1.FileNames(x)) 'OpenFileDialog1.FileNames(x)
                    Me.ListView1.Items.Add(OpenFileDialog1.FileNames(x))
                    Me.ListView1.Items.Item(ListView1.Items.Count - 1).SubItems.Add(OpenFileDialog1.FileNames(x))

                Next


            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox1.ReadOnly = False
            TextBox1.PasswordChar = ""
        Else
            TextBox1.ReadOnly = True
            TextBox1.PasswordChar = "*"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ListView1.Items.Clear()
    End Sub

    Private Sub FRM_encrypt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Function RandomString()
        Dim s As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
        Dim r As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To My.Settings.private_key.ToString
            Dim idx As Integer = r.Next(0, 35)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Text = RandomString()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Clear()
        TextBox1.PasswordChar = ""
        CheckBox1.Checked = True
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Close()

    End Sub

    Private Function GetFileName(ByVal path As String) As String
        Dim _filename As String = System.IO.Path.GetFileName(path)
        Return _filename
    End Function


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = vbNullString Then
            MsgBox("الرجاء انشاء مفتاح تشفير", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If
        If ListView1.Items.Count = 0 Then
            MsgBox("الرجاء اختار الملفات المراد تشفيرها", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If
        Try
            For i = 0 To ListView1.Items.Count - 1

                'MsgBox(ListView1.Items(i).SubItems(1).Text)
                If My.Settings.path_backup = vbNullString Then
                    'do nothing 

                Else
                    My.Computer.FileSystem.CopyFile(ListView1.Items(i).SubItems(1).Text, My.Settings.path_backup & "\" & GetFileName(ListView1.Items(i).SubItems(1).Text), True)

                End If

                Dim FileEncrypted = Encryption(ListView1.Items(i).SubItems(1).Text, TextBox1.Text)

                'do Encryption
                My.Computer.FileSystem.WriteAllBytes(ListView1.Items(i).SubItems(1).Text, FileEncrypted, False)

                File.Move(ListView1.Items(i).SubItems(1).Text, ListView1.Items(i).SubItems(1).Text & ".GDC")
                'Thread.Sleep(100)

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Dim strI5 As String = "INSERT INTO  Encrypted_files ([Name_Path_file],[Private_key],[Date_f])  VALUES ('" & ListView1.Items(i).SubItems(1).Text & "','" & TextBox1.Text & "','" & Now.ToString() & "')"
                Dim cmd3 As New SqlCommand(strI5, con)
                cmd3.ExecuteNonQuery()

            Next
            MsgBox("تم التشفير الملفات المحددة بنجاح", MsgBoxStyle.Information, "تشفير ملفات")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try


    End Sub





    Function Encryption(ByVal Path As String, ByVal KEy_gen As String) As Byte()

        Dim input As Byte() = File.ReadAllBytes(Path)

        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim SHA256 As New System.Security.Cryptography.SHA256Cng
        Dim ciphertext As String = ""
        Try
            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(KEy_gen))
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = input
            Return DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
        Catch ex As Exception
        End Try
    End Function

    Function Decryption(ByVal Path As String, ByVal KEy_gen As String) As Byte()

        Dim input As Byte() = File.ReadAllBytes(Path)

        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim SHA256 As New System.Security.Cryptography.SHA256Cng
        Try
            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(KEy_gen))
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = input
            Return DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Function

End Class