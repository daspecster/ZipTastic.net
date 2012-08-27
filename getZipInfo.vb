﻿Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json

Public Class getZipInfo
    ' in case of future upgrades
    Public API_Key As String = String.Empty

    Public Function getZipData(zipCode As String, country As String) As iZip
        Try
            Dim str As String = sendReq(zipCode, country)
            Return JsonConvert.DeserializeObject(Of iZip)(str)
        Catch ex As Exception
            Throw New ApplicationException("An error occurred retrieving your city and state information")
        End Try
        
    End Function

    Private Function sendReq(zipCode As String, country As String) As String
         Dim uri As String = "http://zip.elevenbasetwo.com/v2/" & country & "/" & zipCode
        Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
        request.Method = WebRequestMethods.Http.Get
        request.ContentType = "text/html"
        Dim oResponse As HttpWebResponse = request.GetResponse()
        Dim reader As New StreamReader(oResponse.GetResponseStream())
        Dim tmp As String = reader.ReadToEnd()
        oResponse.Close()
        Return tmp
    End Function
End Class
