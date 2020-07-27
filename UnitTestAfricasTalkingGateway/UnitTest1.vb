
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace UnitTestAfricasTalkingGateway
    <TestClass>
    Public Class UnitTest1
        Dim username As String = "sandbox"
        Dim apikey As String = "6c36e56b86c24c2ff66adaff340d60793dff71ac304bc551f7056ca76dd8032a"
        ReadOnly _gateway As New AfricasTalkingGateway(username, apikey)

        <TestMethod()>
        Public Sub TestSendMessage()
            'Tests that we can successfully Send Messages
            Dim message As String = "It works"
            Dim recipient As String = "+254720000001"
            Dim results As String = _gateway.SendMessage(recipient, message)
            ' Let's parse that result and ensure the contents does not have any Error messages 
            Dim statusMessage As Boolean = results.Contains("Success")
            Assert.IsTrue(statusMessage)
        End Sub

        <TestMethod()>
        Public Sub TestBulkSms()
            Dim message As String = "This is a bulk SMS message"
            Dim recipients As String = "+254720000001,+254720000002,+254720000003"
            Dim result As String = _gateway.SendMessage(recipients, message)
            ' Let's ensure we can send bulk sms to comma separated numbers
            Dim bulkSmsStatus As Boolean = result.Contains("Success")
            Assert.IsTrue(bulkSmsStatus)
        End Sub

        <TestMethod()>
        Public Sub TestFetchMessages()
            Dim receivedId As Integer = 0
            Dim result As String = _gateway.FetchMessages(receivedId)
            ' Fetch messages should contain IDs or SMSMessageData (if empty)
            Dim messageIds As Boolean = result.Contains("SMSMessageData")
            Assert.IsTrue(messageIds)
        End Sub

        <TestMethod()>
        Public Sub TestTokenCreation()
            Dim phoneNumber As String = "+254720000001"
            Dim tokenResult As String = _gateway.CreateCheckoutToken(phoneNumber)
            ' Expect a success message 
            Dim tokenStatus As Boolean = tokenResult.Contains("Success")
            Assert.IsTrue(tokenStatus)
        End Sub

        <Ignore()>
        <TestMethod()>
        Public Sub TestCreateSubscription()
            Dim phoneNumber As String = "+254720000001"
            Dim shortcode As String = "44005"
            Dim keyword As String = "coolguy"
            Dim token As String = "CkTkn_bb16a685-6f41-4373-b674-dc301980490c"
            Dim result As String = _gateway.CreateSubscription(phoneNumber, shortcode, keyword, token)
            Dim resStatus As Boolean = result.Contains("Success")
            Assert.IsTrue(resStatus)
        End Sub

        <TestMethod()>
        Public Sub TestAirtimeService()
            Dim airtimeRecipientsList As New ArrayList()
            Dim recipient1 As New Hashtable()
            recipient1("phoneNumber") = "+254720000001"
            recipient1("amount") = "KES 250"
            Dim recipient2 As New Hashtable()
            recipient2("phoneNumber") = "+254720000004"
            recipient2("amount") = "KES 3000"
            airtimeRecipientsList.Add(recipient1)
            airtimeRecipientsList.Add(recipient2)
            Dim airtimeTransact As String = _gateway.SendAirtime(airtimeRecipientsList)
            Dim status As Boolean = airtimeTransact.Contains("Sent")
            Assert.IsTrue(status)
        End Sub

        <TestMethod()>
        Public Sub TestCallService()
            Dim caller As String = "+254720000000"
            Dim recipients As String = "+254720000001"
            Dim callResult As String = _gateway.Call(caller, recipients)
            Dim callStatus As Boolean = callResult.Contains("Queued")
            Assert.IsTrue(callStatus)
        End Sub

        <TestMethod()>
        Public Sub TestCallMultipleNumbers()
            Dim caller As String = "+254720000000"
            Dim recipients As String = "+254720000001,+254720000002,+254720000003"
            Dim callResult As String = _gateway.Call(caller, recipients)
            Dim callStatus As Boolean = callResult.Contains("Queued")
            Assert.IsTrue(callStatus)
        End Sub

        <Ignore()>
        <TestMethod()>
        Public Sub TestMobileCheckout()
            Dim productName As String = "coolproduct"
            Dim phoneNumber As String = "+254720000001"
            Dim amount As Decimal = 30853
            Dim channel As String = "mychannel"
            Dim currency As String = "KES"
            Dim metadata As Dictionary(Of String, String) = New Dictionary(Of String, String) From
            {
                {"reason", "test reason"}
            }
            Dim mobileChckoutResult As String = _gateway.InitiateMobilePaymentCheckout(productName, phoneNumber, currency, amount, channel, metadata)
            Dim mobileCheckoutStatus As Boolean = mobileChckoutResult.Contains("PendingConfirmation")
            Assert.IsTrue(mobileCheckoutStatus)
        End Sub

        <Ignore()>
        <TestMethod()>
        Public Sub TestB2BPayments()
            Dim productName As String = "awesomeproduct"
            Dim currencyCode As String = "KES"
            Dim amount As Decimal = 15
            Dim provider As String = "Athena"
            Dim destinationChannel As String = "mychannel"
            Dim destinationAccount As String = "coolproduct"
            Dim metadataDetails As Dictionary(Of String, String) = New Dictionary(Of String, String) From
                {
                    {"Shop Name", "Kaduna"},
                    {"Reason", "Secret Purchase"}
                }
            Dim transferType As String = "BusinessToBusinessTransfer"
            Dim b2BResult As String = _gateway.MobileB2B(productName, provider, transferType, currencyCode, amount, destinationChannel, destinationAccount, metadataDetails)
            Dim mobileB2BStatus As Boolean = b2BResult.Contains("Queued")
            Assert.IsTrue(mobileB2BStatus)
        End Sub

        <TestMethod()>
        Public Sub TestB2CPayments()
            Dim productName As String = "awesomeproduct"
            Dim currencyCode As String = "KES"
            Dim rec1Num As String = "+254720000002"
            Dim rec2Num As String = "+254720000001"
            Dim rec1Name As String = "T'Challa"
            Dim rec2Name As String = "Shuri"
            Dim rec1Amount As Decimal = 15320
            Dim rec2Amount As Decimal = 33500

            Dim rec1 As MobilePaymentB2CRecipient = New MobilePaymentB2CRecipient(rec1Name, rec1Num, currencyCode, rec1Amount)
            Dim rec2 As MobilePaymentB2CRecipient = New MobilePaymentB2CRecipient(rec2Name, rec2Num, currencyCode, rec2Amount)
            rec2.AddMetadata("Reason", "Awesome Tech")
            rec1.AddMetadata("Reason", "Saving the Kingdom")
            Dim recList As IList(Of MobilePaymentB2CRecipient) = New List(Of MobilePaymentB2CRecipient) From
                    {
                    rec1, rec2
                    }
            Dim mobileB2Ctransaction As String = _gateway.MobilePaymentB2CRequest(productName, recList)
            Dim isTransferInitiated As Boolean = mobileB2Ctransaction.Contains("Queued")
            Assert.IsTrue(isTransferInitiated)
        End Sub

        <Ignore()>
        <TestMethod()>
        Public Sub TestBankTransfer()
            Dim productname As String = "coolproduct"
            Dim currencyCode As String = "NGN"
            Dim recipient1AccountName As String = "Alyssa Hacker"
            Dim recipient1AccountNumber As String = "1234567890"
            Dim recipient1BankCode As Integer = 234001
            Dim recipient1Amount As Decimal = 1500.5D
            Dim recipient1Narration As String = "December Bonus"
            Dim recipient2AccountName As String = "Ben BitDiddle"
            Dim recipient2AccountNumber As String = "234567891"
            Dim recipient2BankCode As Integer = 234004
            Dim recipient2Amount As Decimal = 1500.5D
            Dim recipient2Narration As String = "February Bonus"
            Dim recipient1Account As BankAccount = New BankAccount(recipient1AccountNumber, recipient1BankCode, recipient1AccountName)
            Dim recipientBody As BankTransferRecipients = New BankTransferRecipients(recipient1Amount, recipient1Account, currencyCode, recipient1Narration)
            recipientBody.AddMetadata("Funky", "Metadata")
            Dim recipient2BankAccount As BankAccount = New BankAccount(recipient2AccountNumber, recipient2BankCode, recipient2AccountName)
            Dim recipient2Body As BankTransferRecipients = New BankTransferRecipients(recipient2Amount, recipient2BankAccount, currencyCode, recipient2Narration)
            recipient2Body.AddMetadata("Another", "Funky Metadata")
            Dim recipients As IList(Of BankTransferRecipients) = New List(Of BankTransferRecipients) From
                {
                    recipient2Body, recipientBody
                }
            Dim bankTransferResults As String = _gateway.BankTransfer(productname, recipients)
            Dim bankTransferStatus As Boolean = bankTransferResults.Contains("Queued")
            Assert.IsTrue(bankTransferStatus)
        End Sub

        <Ignore()>
        <TestMethod()>
        Public Sub TestBankChekout()
            Dim productName As String = "coolproduct"
            Dim accountName As String = "Fela Kuti"
            Dim accountNumber As String = "1234567890"
            Dim bankCode As Integer = 234001
            Dim currencyCode As String = "NGN"
            Dim amount As Decimal = 1000.5D
            Dim dob As String = "2017-11-22"
            Dim metadata As Dictionary(Of String, String) = New Dictionary(Of String, String) From
                    {
                    {"Reason", "Buy Vega Records"}
                    }
            Dim narration As String = "We're buying something cool"
            Dim receBank As BankAccount = New BankAccount(accountNumber, bankCode, dob, accountName)
            Dim res As String = _gateway.BankCheckout(productName, receBank, currencyCode, amount, narration, metadata)
            Dim checkoutStatus As Boolean = res.Contains("PendingValidation")
            Assert.IsTrue(checkoutStatus)
        End Sub

        <Ignore()>
        <TestMethod()>
        Public Sub TestCardCheckout()
            Const productName As String = "awesomeproduct"
            Const currencyCode As String = "NGN"
            Const amount As Decimal = 7500.5D
            Const narration As String = "Buy Aluku Records"
            Dim metadata As Dictionary(Of String, String) = New Dictionary(Of String, String) From {
                    {"Parent Company", "Offering Records"},
                    {"C.E.O", "Boddhi Satva"}
                    }
            Const cardCvv As Short = 123
            Const cardNum As String = "123456789012345"
            Const countryCode As String = "NG"
            Const cardPin As String = "1234"
            Const validTillMonth As Integer = 9
            Const validTillYear As Integer = 2019
            Dim cardDetails As PaymentCard = New PaymentCard(cardPin, countryCode, cardCvv, validTillMonth, validTillYear, cardNum)
            Dim checkoutRes As String = _gateway.CardCheckout(productName, cardDetails, currencyCode, amount, narration, metadata)
            Dim cardCheckoutStatus As Boolean = checkoutRes.Contains("PendingValidation")
            Assert.IsTrue(cardCheckoutStatus)
        End Sub
 
 '  TBD Test Bank OTP 
        <Ignore()>
        <TestMethod()>
        Public Sub TestCardOtpValidation()
            Const transId As String = "ATPid_ee698c34c7d807ff55da8a9033dcfecd"
            Const pin As String = "1234"
            Dim otpResults As String = _gateway.OtpValidateCard(transId, pin)
            Dim otpStatus As Boolean = otpResults.Contains("Success")
            Assert.IsTrue(otpStatus)
        End Sub
    End Class
End Namespace

