
Imports MongoDB.Bson
Imports MongoDB.Driver
Imports Newtonsoft.Json

Public Class _Default
    Inherits Page

    Private _listCustomer As List(Of BsonDocument)

    Public ReadOnly Property GetListCustomers() As List(Of BsonDocument)
        Get
            Return _listCustomer
        End Get
    End Property


    Private Sub ConnectDb()
        Dim client = New MongoClient("mongodb://localhost:27017")
        Dim database = client.GetDatabase("mydb")
        Dim collection = database.GetCollection(Of BsonDocument)("customers")


        Me._listCustomer = collection.Find(New BsonDocument()).ToList()

        Dim listCustomer As New List(Of Object)

        For Each customer In collection.Find(New BsonDocument()).ToList()
            listCustomer.Add(New With {
                .Id = customer.Item("_id").ToString(),
                .Name = customer.Item("name").ToString(),
                .Gender = customer.Item("gender").ToString()
            })
        Next



        Me.grdListCustomer.DataSource = listCustomer
        Me.grdListCustomer.DataBind()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        ConnectDb()

        With Me.pnlConnection
            .InnerHtml = "Hello"
            .Style.Add("color", "red")
        End With

    End Sub

    Private Sub grdListCustomer_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdListCustomer.RowCommand
        Dim client = New MongoClient("mongodb://localhost:27017")
        Dim database = client.GetDatabase("mydb")
        Dim collection = database.GetCollection(Of BsonDocument)("customers")

        Dim id = CType(CType(e.CommandSource, LinkButton).NamingContainer, GridViewRow).Cells(1).Text

        If e.CommandName = "DeleteMe" Then

            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of Object)("_id", New ObjectId(id))

            collection.DeleteOne(filter)

            Me.pnlConnection.InnerHtml = ("hello from delete command").ToUpper
        ElseIf e.CommandName = "EditMe" Then
            Me._id.Value = id

            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of Object)("_id", New ObjectId(id))
            Dim doc = collection.Find(filter).FirstOrDefault()

            Me.name.Text = doc.Item("name").ToString
            Me.gender.Text = doc.Item("gender").ToString
            Me.age.Text = doc.Item("age").ToString()


            mvMongo.SetActiveView(vwEdit)
        End If

    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim client = New MongoClient("mongodb://localhost:27017")
        Dim database = client.GetDatabase("mydb")
        Dim collection = database.GetCollection(Of BsonDocument)("customers")

        Dim filter = Builders(Of BsonDocument).Filter.Eq(Of Object)("_id", New ObjectId(Me._id.Value))

        Dim update = Builders(Of BsonDocument).Update _
            .Set(Of Object)("name", Me.name.Text) _
            .Set(Of Object)("gender", Me.gender.Text) _
            .Set(Of Object)("age", Me.age.Text)

        collection.UpdateOne(filter, update)

        'View Main
        Me.mvMongo.SetActiveView(vwMain)

        'Refresh
        Call Me.Page_Load(sender, e)


    End Sub


End Class