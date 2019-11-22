Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net.Http
Imports System.Net
Imports System.IO
Imports System.Text
Imports Newtonsoft

Public Class Form1
    Dim dt As DataTable

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call load()
    End Sub

    Async Function load() As Task
        dt = New DataTable()
        showProgressBar()

        Try
            '--------------- RESTFUL API -----------------------
            Dim address As String = "https://apex.oracle.com/pls/apex/belajar_apex_10_11_2019/hrd_api/getjabatan"
            Dim webClient As New WebClient

            'aktifkan SSL
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            'download data json
            Dim result As String = webClient.DownloadString(address)

            'baca response
            Dim data As JObject = JObject.Parse(result)

            'ambil objek ITEMS
            Dim arrData As String = data("items").ToString

            'konversi json ke datatable
            dt = JsonConvert.DeserializeObject(Of DataTable)(arrData)

            'taruh data grid view
            DataGridView1.DataSource = dt
        Catch ex As Exception
            Console.Write(ex.Message)
        Finally
            hideProgressBar()
        End Try
    End Function

    Sub showProgressBar()
        With ProgressBar1
            .Visible = True
            .MarqueeAnimationSpeed = 30
            .Style = ProgressBarStyle.Marquee
        End With
    End Sub

    Sub hideProgressBar()
        With ProgressBar1
            .Visible = False
            .MarqueeAnimationSpeed = 0
            .Style = ProgressBarStyle.Blocks
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call load()
    End Sub
End Class
