﻿Imports Newtonsoft.Json

Namespace AfricasTalkingGateway

    Public Class MobilePaymentB2CRecipient
        <JsonProperty("phoneNumber")>
        Public Property PhoneNumber() As String

        <JsonProperty("currencyCode")>
        Public Property CurrencyCode() As String

        <JsonProperty("amount")>
        Public Property Amount() As Decimal

        <JsonProperty("metadata")>
        Public Property Metadata As Dictionary(Of String, String)

        Public Sub New(ByVal phoneNumber As String, ByVal currencyCode As String, ByVal amount As Decimal)
            Me.PhoneNumber = phoneNumber
            Me.CurrencyCode = currencyCode
            Me.Amount = amount
            Metadata = New Dictionary(Of String, String)()
        End Sub

        Public Sub AddMetadata(ByVal key As String, ByVal value As String)
            Me.Metadata.Add(key, value)
        End Sub

        Public Function ToJson() As String
            Dim json = JsonConvert.SerializeObject(Me)
            Return json
        End Function

    End Class
End Namespace



