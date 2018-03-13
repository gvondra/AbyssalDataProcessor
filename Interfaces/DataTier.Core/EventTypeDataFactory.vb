Public Class EventTypeDataFactory
    Implements IEventTypeDataFactory

    Public Property GenericDataFactory As IGenericDataFactory(Of EventTypeData)

    Public Sub New()
        Me.GenericDataFactory = New GenericDataFactory(Of EventTypeData)()
    End Sub

    Public Function [Get](settings As ISettings, id As Int16) As EventTypeData Implements IEventTypeDataFactory.Get
        Return [Get](settings, New DbProviderFactory(), id)
    End Function

    Public Function [Get](settings As ISettings, ByVal providerFactory As IDbProviderFactory, id As Int16) As EventTypeData
        Dim parameter As IDbDataParameter = CreateParameter(providerFactory, "id", DbType.Int16)
        parameter.Value = id
        Return Me.GenericDataFactory.GetData(settings, providerFactory, "adp.sEventType", Function() New EventTypeData, {parameter}).FirstOrDefault
    End Function
End Class
