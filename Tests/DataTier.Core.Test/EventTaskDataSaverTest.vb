<TestClass()>
Public Class EventTaskDataSaverTest

    <TestMethod()>
    Public Sub CreateTest()
        Dim settings As New Mock(Of ISettings)()
        Dim data As New EventTaskData()
        Dim saver As New EventTaskDataSaver(settings.Object, data)
        Dim providerFactory As New Mock(Of IDbProviderFactory)()
        Dim connection As New Mock(Of IDbConnection)()
        Dim command As New Mock(Of IDbCommand)
        Dim parameters As New Mock(Of IDataParameterCollection)

        providerFactory.Setup(Sub(f As IDbProviderFactory) f.EstablishTransaction(settings.Object)) _
        .Callback(Sub()
                      command.SetupGet(Of IDataParameterCollection)(Function(c As IDbCommand) c.Parameters).Returns(parameters.Object)
                      connection.Setup(Of IDbCommand)(Function(c As IDbConnection) c.CreateCommand).Returns(command.Object)
                      settings.SetupGet(Of IDbConnection)(Function(s As ISettings) s.Connection).Returns(connection.Object)
                  End Sub)

        providerFactory.Setup(Of IDbDataParameter)(Function(f As IDbProviderFactory) f.CreateParameter()) _
        .Returns(Function() New Mock(Of IDbDataParameter)().Object)

        saver.Create(providerFactory.Object)
        providerFactory.Verify(Sub(f As IDbProviderFactory) f.EstablishTransaction(settings.Object), Times.Once)
        command.Verify(Of Integer)(Function(c As IDbCommand) c.ExecuteNonQuery(), Times.Once)
        command.VerifySet(Sub(c As IDbCommand) c.CommandType = CommandType.StoredProcedure, Times.AtLeastOnce)
        command.VerifySet(Sub(c As IDbCommand) c.CommandText = "adp.iEventTask", Times.AtLeastOnce)
    End Sub

End Class
