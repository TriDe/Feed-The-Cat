Imports System.Xml
Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml.XPath

Public Class frm_FeedTheCat
    Structure feed
        Dim linkname As String
        Dim link_1 As String

    End Structure
    Dim f(10) As feed
    Dim g_count As Integer = 0
    Dim FeedUrl As String
    Dim title As String
    Dim link As String
    Dim description As String
    Dim imageurl As String
    Dim pubDate As String
    Dim first As Integer = 0
    Dim feedname As String
    Dim flag As Integer = 0
    ''' <summary>
    ''' Parse the XML file.Finds the inner text.
    ''' </summary>
    ''' <remarks></remarks>


    Private Sub GetChannelInfo()

        Dim j As Integer
        j = 0
        rtbLinkDisplay.Clear()
        Try

        
        Dim rss As XmlNodeList = GetXmlDoc("rss/channel/item")


        'Loops through XML node does 7 iterations
        While j < 7



            pubDate = rss(j).SelectSingleNode("pubDate").InnerText.ToString & vbNewLine
            Me.rtbLinkDisplay.SelectionColor = Color.MediumBlue

            'Finds the inner text 
            title = rss(j).SelectSingleNode("title").InnerText.ToString & vbNewLine & vbNewLine
            'link = rss(0).SelectSingleNode("link").InnerText.ToString & vbNewLine & vbNewLine




            'lstFeedDisplay.Items.Add(title)
            'displays contents to richtext Box
            rtbLinkDisplay.AppendText(pubDate.ToString)


            Me.rtbLinkDisplay.SelectionFont = New Font("Arial", 14,
                                           FontStyle.Bold)
                Me.rtbLinkDisplay.SelectionColor = Color.DarkBlue
            rtbLinkDisplay.AppendText(title.ToString)
                rtbDescription.AppendText(pubDate.ToString)
                Me.rtbDescription.SelectionFont = New Font("Arial", 10,
                                        FontStyle.Bold)
                rtbDescription.AppendText(title.ToString)
            ' rtbLinkDisplay.AppendText(link.ToString)
            j = j + 1

            End While

            Dim get_url As String

            description = rss(0).SelectSingleNode("description").InnerText.ToString
        get_url = GetImage(description)
            PictureBox4.Load(get_url)
        description = rss(1).SelectSingleNode("description").InnerText.ToString
        get_url = GetImage(description)
        PictureBox3.Load(get_url)
        description = rss(2).SelectSingleNode("description").InnerText.ToString
        get_url = GetImage(description)
            PictureBox5.Load(get_url)
        description = rss(3).SelectSingleNode("description").InnerText.ToString
        get_url = GetImage(description)
            PictureBox6.Load(get_url)
        'Description Text
        Catch ex As Exception
            MessageBox.Show("No Images OR Incorrect URL", "Error")
            Exit Sub
        End Try
    End Sub
    Private Function GetImage(ByVal text As String) As String

        'Uses regular expression to get the image url
        Dim regex As Regex = New Regex("src=""?([^\s""]*)""?")
        Dim match As Match = regex.Match(text)

        If match.Success Then
            'Displays the image to picturebox
            Return (match.Groups(1).Value)
            'match.groups(1).value is the url
        End If
    End Function

    'Sendind and receiving request to get the XML document
    Private Function GetXmlDoc(ByVal node As String) As XmlNodeList

        Dim tempNodelist As System.Xml.XmlNodeList = Nothing
        Dim request As WebRequest = WebRequest.Create(Me.FeedUrl)

        Dim response As WebResponse = request.GetResponse()

        Dim rssStream As Stream = response.GetResponseStream()

        Dim rssDoc As XmlDocument = New XmlDocument()

        rssDoc.Load(rssStream)

        tempNodelist = rssDoc.SelectNodes(node)
        Return tempNodelist



        'Return tempNodelist
    End Function


    Private Function GetName(ByVal link As String) As String
        Dim regex As Regex = New Regex("www.([^.]*)")
        Dim match As Match = regex.Match(link)

        If match.Success Then

            'match.groups(1).value is the url
            Return (match.Groups(1).Value)
        Else
            Return link
        End If
    End Function

    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub RichTextBox1_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtbLinkDisplay.TextChanged

    End Sub

    Private Sub txtLinkTaker_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLinkTaker.TextChanged

    End Sub

    Private Sub btnGetFeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFeed.Click
        'Getting the FeedUrl From the user 
        feedname = GetName(txtLinkTaker.Text)
        FeedUrl = txtLinkTaker.Text
        GetChannelInfo()
        'Threading.Thread.Sleep(1000)
        f(g_count).linkname = feedname
        f(g_count).link_1 = FeedUrl
        g_count = g_count + 1


        Me.lstFeedDisplay.Font = New Font("Arial", 9,
                                          FontStyle.Bold)

        lstFeedDisplay.Items.Add(feedname)




    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If My.Computer.Network.IsAvailable = True Then
            MsgBox("Successfully connected To Internet")
        Else
            MsgBox("Internet Connection Not Found")
            txtLinkTaker.Enabled = False
            Me.Close()
        End If

    End Sub

    Private Sub lstFeedDisplay_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFeedDisplay.SelectedIndexChanged


            'FeedUrl = WebBrowser1.Document.Body.InnerText
            rtbLinkDisplay.Clear()

            Dim i As Integer = 0
            Dim search_url As String = lstFeedDisplay.SelectedItem.ToString
            While i < g_count
                If String.Compare(search_url, f(i).linkname.ToString) = 0 Then
                FeedUrl = f(i).link_1
                ' WebBrowser1.Navigate(link)
                    GetChannelInfo()
                    Exit While
                End If
                i = i + 1
            End While


    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)

    End Sub

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs)

    End Sub
End Class
