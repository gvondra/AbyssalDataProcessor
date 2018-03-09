Public Class DataCreatorWrapper
    Implements Framework.IDataCreator

    Private m_innerDataCreator As AbyssalDataProcessor.DataTier.Core.IDataCreator
    Private m_createDelegate As Action

    Public Sub New()
    End Sub

    Public Sub New(dataCreator As AbyssalDataProcessor.DataTier.Core.IDataCreator)
        m_innerDataCreator = dataCreator
    End Sub

    Public Sub New(ByVal createDelegate As Action)
        m_createDelegate = createDelegate
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        If m_innerDataCreator IsNot Nothing Then
            m_innerDataCreator.Create()
        End If
        If m_createDelegate IsNot Nothing Then
            m_createDelegate.Invoke()
        End If
    End Sub
End Class
