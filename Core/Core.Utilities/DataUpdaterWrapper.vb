Public Class DataUpdateWrapper
    Implements Framework.IDataUpdater

    Private m_innerDataUpdater As AbyssalDataProcessor.DataTier.Core.IDataUpdater
    Private m_updateDelegate As Action

    Public Sub New()
    End Sub

    Public Sub New(dataUpdater As AbyssalDataProcessor.DataTier.Core.IDataUpdater)
        m_innerDataUpdater = dataUpdater
    End Sub

    Public Sub New(ByVal updateDelegate As Action)
        m_updateDelegate = updateDelegate
    End Sub

    Public Sub Update() Implements IDataUpdater.Update
        If m_innerDataUpdater IsNot Nothing Then
            m_innerDataUpdater.Update()
        End If
        If m_updateDelegate IsNot Nothing Then
            m_updateDelegate.Invoke()
        End If
    End Sub
End Class
