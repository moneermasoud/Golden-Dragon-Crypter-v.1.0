Imports System.IO
Imports System.Data.SqlClient
Imports System.Windows.Forms.Control
Module config
    ' Dim Read As New StreamReader("")
    Dim con_str As String = My.Settings.con_str

    Public con As New SqlConnection(con_str) ' الاتصال بالقاعدة

    ' Public setting As New My.MySettings

    Public SQLQuery As String
    Public CMD As New SqlCommand

    ' reader 
    Public reader As SqlDataReader
    Public reader_users As SqlDataReader
    Public reader_guar As SqlDataReader
    Public reader_widow As SqlDataReader
    Public rde As SqlDataReader

    Public rde2 As SqlDataReader
    Public log As SqlDataReader

    Public UserName As String

    Public UserID As Integer

    ' Public Permes As String
    Public Permes As String


    'Show Report Cars 
    Public report_show_member As New DataTable
    Public report_show_member_da As SqlDataAdapter


    '
    Public report_show_c As New DataTable
    Public report_show_c2 As New DataTable
    Public report_show_c_da As SqlDataAdapter


    'Show Report License
    Public report_show_license As New DataTable
    Public report_show_license_da As SqlDataAdapter

End Module
