Imports Moq
<TestClass()>
Public Class EventTypeDataFactoryTest

    <TestMethod()>
    Public Sub GetTest()
        Dim generic As New Mock(Of IGenericDataFactory(Of EventTypeData))()
        Dim factory As New EventTypeDataFactory() With {.GenericDataFactory = generic.Object}
        Dim settings As New Mock(Of ISettings)()
        Dim providerFactory As New Mock(Of IDbProviderFactory)()

        providerFactory.Setup(Of IDbDataParameter)(Function(f As IDbProviderFactory) f.CreateParameter()).Returns(Function() New Mock(Of IDbDataParameter)().Object)

        factory.Get(settings.Object, providerFactory.Object, 2S)

        generic.Verify(Of IEnumerable(Of EventTypeData))(Function(f As IGenericDataFactory(Of EventTypeData)) f.GetData(settings.Object, providerFactory.Object, "adp.sEventType", It.IsAny(Of Func(Of EventTypeData)), It.IsAny(Of IEnumerable(Of IDataParameter))), Times.Once)
    End Sub

End Class
