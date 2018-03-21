<TestClass()>
Public Class TaskDataFactoryTest

    <TestMethod()>
    Public Sub GetTest()
        Dim generic As New Mock(Of IGenericDataFactory(Of TaskData))()
        Dim factory As New TaskDataFactory() With {.GenericDataFactory = generic.Object}
        Dim settings As New Mock(Of ISettings)()
        Dim providerFactory As New Mock(Of IDbProviderFactory)()

        providerFactory.Setup(Of IDbDataParameter)(Function(f As IDbProviderFactory) f.CreateParameter()).Returns(Function() New Mock(Of IDbDataParameter)().Object)

        factory.Get(settings.Object, providerFactory.Object, Guid.Empty)

        generic.Verify(Of IEnumerable(Of TaskData))(
            Function(f As IGenericDataFactory(Of TaskData)) f.GetData(settings.Object,
                                                                      providerFactory.Object,
                                                                      "adp.sTask",
                                                                      It.IsAny(Of Func(Of TaskData)),
                                                                      It.IsAny(Of Action(Of IEnumerable(Of TaskData))),
                                                                      It.IsAny(Of IEnumerable(Of IDataParameter))),
            Times.Once
        )
    End Sub

    <TestMethod()>
    Public Sub GetByUserIdTest()
        Dim generic As New Mock(Of IGenericDataFactory(Of TaskData))()
        Dim factory As New TaskDataFactory() With {.GenericDataFactory = generic.Object}
        Dim settings As New Mock(Of ISettings)()
        Dim providerFactory As New Mock(Of IDbProviderFactory)()

        providerFactory.Setup(Of IDbDataParameter)(Function(f As IDbProviderFactory) f.CreateParameter()).Returns(Function() New Mock(Of IDbDataParameter)().Object)

        factory.GetByUserId(settings.Object, providerFactory.Object, Guid.Empty)

        generic.Verify(Of IEnumerable(Of TaskData))(
            Function(f As IGenericDataFactory(Of TaskData)) f.GetData(settings.Object,
                                                                      providerFactory.Object,
                                                                      "adp.sTaskByUserId",
                                                                      It.IsAny(Of Func(Of TaskData)),
                                                                      It.IsAny(Of Action(Of IEnumerable(Of TaskData))),
                                                                      It.IsAny(Of IEnumerable(Of IDataParameter))),
            Times.Once
        )
    End Sub

End Class
