﻿Imports System.Text
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography

Public Class FRM_Decrypt

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

    Private Sub FRM_Decrypt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Clear()
        TextBox1.PasswordChar = ""
        CheckBox1.Checked = True
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Close()
    End Sub


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



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = vbNullString Then
            MsgBox("الرجاء ادخال مفتاح تشفير", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If

        Try
            For i = 0 To ListView1.Items.Count - 1

                Dim str_path As String
                str_path = ListView1.Items(i).SubItems(1).Text
                Dim result = ListView1.Items(i).SubItems(1).Text.Substring(ListView1.Items(i).SubItems(1).Text.Length - 4)

                'MsgBox(ListView1.Items(i).SubItems(1).Text)


                Dim FileEncrypted = Decryption(ListView1.Items(i).SubItems(1).Text, TextBox1.Text)

                'do Decryption
                My.Computer.FileSystem.WriteAllBytes(ListView1.Items(i).SubItems(1).Text, FileEncrypted, False)

                File.Move(ListView1.Items(i).SubItems(1).Text, ListView1.Items(i).SubItems(1).Text.Replace(result, ""))
                'Thread.Sleep(100)



                'MsgBox(ListView1.Items(i).SubItems(1).Text.Replace(result, ""))
            Next
            MsgBox("تم فك التشفير الملفات المحددة بنجاح", MsgBoxStyle.Information, "تشفير ملفات")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
End Class