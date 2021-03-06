﻿Imports Moq
<TestClass()>
Public Class FormDataFactoryTest

    <TestMethod()>
    Public Sub GetTest()
        Dim generic As New Mock(Of IGenericDataFactory(Of FormData))()
        Dim factory As New FormDataFactory() With {.GenericDataFactory = generic.Object}
        Dim settings As New Mock(Of ISettings)()
        Dim providerFactory As New Mock(Of IDbProviderFactory)()

        providerFactory.Setup(Of IDbDataParameter)(Function(f As IDbProviderFactory) f.CreateParameter()).Returns(Function() New Mock(Of IDbDataParameter)().Object)

        factory.Get(settings.Object, providerFactory.Object, Guid.Empty)

        generic.Verify(Of IEnumerable(Of FormData))(
            Function(f As IGenericDataFactory(Of FormData)) f.GetData(settings.Object,
                                                                      providerFactory.Object,
                                                                      "adp.sForm",
                                                                      It.IsAny(Of Func(Of FormData)),
                                                                      It.IsAny(Of Action(Of IEnumerable(Of FormData))),
                                                                      It.IsAny(Of IEnumerable(Of IDataParameter))),
            Times.Once
        )
    End Sub

End Class
