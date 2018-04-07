Imports Moq
<TestClass()>
Public Class UserDataFactoryTest

    <TestMethod()>
    Public Sub GetBySubscriberIdTest()
        Dim generic As New Mock(Of IGenericDataFactory(Of UserData))()
        Dim factory As New UserDataFactory() With {.GenericDataFactory = generic.Object}
        Dim settings As New Mock(Of ISettings)()
        Dim providerFactory As New Mock(Of IDbProviderFactory)()

        providerFactory.Setup(Of IDbDataParameter)(Function(f As IDbProviderFactory) f.CreateParameter()).Returns(Function() New Mock(Of IDbDataParameter)().Object)

        factory.GetBySubscriberId(settings.Object, providerFactory.Object, Guid.Empty, "test subscriber")

        generic.Verify(Of IEnumerable(Of UserData))(
            Function(f As IGenericDataFactory(Of UserData)) f.GetData(settings.Object,
                                                                      providerFactory.Object,
                                                                      "clnt.sUserBySubscriberId",
                                                                      It.IsAny(Of Func(Of UserData)),
                                                                      It.IsAny(Of Action(Of IEnumerable(Of UserData))),
                                                                      It.IsAny(Of IEnumerable(Of IDataParameter))),
            Times.Once
        )
    End Sub

    <TestMethod()>
    Public Sub GetTest()
        Dim generic As New Mock(Of IGenericDataFactory(Of UserData))()
        Dim factory As New UserDataFactory() With {.GenericDataFactory = generic.Object}
        Dim settings As New Mock(Of ISettings)()
        Dim providerFactory As New Mock(Of IDbProviderFactory)()

        providerFactory.Setup(Of IDbDataParameter)(Function(f As IDbProviderFactory) f.CreateParameter()).Returns(Function() New Mock(Of IDbDataParameter)().Object)

        factory.Get(settings.Object, providerFactory.Object, Guid.Empty, Guid.Empty)

        generic.Verify(Of IEnumerable(Of UserData))(
            Function(f As IGenericDataFactory(Of UserData)) f.GetData(settings.Object,
                                                                      providerFactory.Object,
                                                                      "clnt.sUser",
                                                                      It.IsAny(Of Func(Of UserData)),
                                                                      It.IsAny(Of Action(Of IEnumerable(Of UserData))),
                                                                      It.IsAny(Of IEnumerable(Of IDataParameter))),
            Times.Once
        )
    End Sub

    <TestMethod()>
    Public Sub SearchTest()
        Dim generic As New Mock(Of IGenericDataFactory(Of UserData))()
        Dim factory As New UserDataFactory() With {.GenericDataFactory = generic.Object}
        Dim settings As New Mock(Of ISettings)()
        Dim providerFactory As New Mock(Of IDbProviderFactory)()

        providerFactory.Setup(Of IDbDataParameter)(Function(f As IDbProviderFactory) f.CreateParameter()).Returns(Function() New Mock(Of IDbDataParameter)().Object)

        factory.Search(settings.Object, providerFactory.Object, Guid.Empty, "test search")

        generic.Verify(Of IEnumerable(Of UserData))(
            Function(f As IGenericDataFactory(Of UserData)) f.GetData(settings.Object,
                                                                      providerFactory.Object,
                                                                      "clnt.sUserSearch",
                                                                      It.IsAny(Of Func(Of UserData)),
                                                                      It.IsAny(Of Action(Of IEnumerable(Of UserData))),
                                                                      It.IsAny(Of IEnumerable(Of IDataParameter))),
            Times.Once
        )
    End Sub
End Class
