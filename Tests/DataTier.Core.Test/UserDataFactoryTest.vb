Imports Moq
<TestClass()>
Public Class UserDataFactoryTest

    <TestMethod()>
    Public Sub GetByEmailAddressTest()
        Dim generic As New Mock(Of IGenericDataFactory(Of UserData))()
        Dim factory As New UserDataFactory() With {.GenericDataFactory = generic.Object}
        Dim settings As New Mock(Of ISettings)()
        Dim providerFactory As New Mock(Of IDbProviderFactory)()

        providerFactory.Setup(Of IDbDataParameter)(Function(f As IDbProviderFactory) f.CreateParameter()).Returns(Function() New Mock(Of IDbDataParameter)().Object)

        factory.GetByEmailAddress(settings.Object, providerFactory.Object, "test email address")

        generic.Verify(Of IEnumerable(Of UserData))(Function(f As IGenericDataFactory(Of UserData)) f.GetData(settings.Object, providerFactory.Object, "adp.sUserByEmailAddress", It.IsAny(Of Func(Of UserData)), It.IsAny(Of IEnumerable(Of IDataParameter))), Times.Once)
    End Sub

    <TestMethod()>
    Public Sub GetBySubscriberIdTest()
        Dim generic As New Mock(Of IGenericDataFactory(Of UserData))()
        Dim factory As New UserDataFactory() With {.GenericDataFactory = generic.Object}
        Dim settings As New Mock(Of ISettings)()
        Dim providerFactory As New Mock(Of IDbProviderFactory)()

        providerFactory.Setup(Of IDbDataParameter)(Function(f As IDbProviderFactory) f.CreateParameter()).Returns(Function() New Mock(Of IDbDataParameter)().Object)

        factory.GetBySubscriberId(settings.Object, providerFactory.Object, "test subscriber")

        generic.Verify(Of IEnumerable(Of UserData))(Function(f As IGenericDataFactory(Of UserData)) f.GetData(settings.Object, providerFactory.Object, "adp.sUserBySubscriberId", It.IsAny(Of Func(Of UserData)), It.IsAny(Of IEnumerable(Of IDataParameter))), Times.Once)
    End Sub

    <TestMethod()>
    Public Sub GetAccountCountTest()
        Dim factory As New UserDataFactory()
        Dim settings As New Mock(Of ISettings)()
        Dim providerFactory As New Mock(Of IDbProviderFactory)()
        Dim connection As New Mock(Of IDbConnection)()
        Dim command As New Mock(Of IDbCommand)()
        Dim count As Integer

        command.Setup(Of Object)(Function(c As IDbCommand) c.ExecuteScalar()).Returns(15)

        connection.Setup(Of IDbCommand)(Function(c As IDbConnection) c.CreateCommand).Returns(command.Object)

        providerFactory.Setup(Of IDbConnection)(Function(f As IDbProviderFactory) f.OpenConnection(It.IsAny(Of String))).Returns(connection.Object)

        count = factory.GetAccountCount(settings.Object, providerFactory.Object)

        Assert.AreEqual(15, count)

        command.Verify(Sub(c As IDbCommand) c.ExecuteScalar(), Times.Once)
        command.VerifySet(Sub(c As IDbCommand) c.CommandText = "adp.sUserAccountCount", Times.Once)
    End Sub
End Class
