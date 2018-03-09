Public Class Saver
    Public Sub Save(ByVal settings As Framework.ISettings, ByVal save As Action)
        Try
            save.Invoke()

            If settings.DbTransaction IsNot Nothing Then
                settings.DbTransaction.Commit()
            End If
        Catch
            If settings.DbTransaction IsNot Nothing Then
                settings.DbTransaction.Rollback()
            End If
            Throw
        Finally
            If settings.DbTransaction IsNot Nothing Then
                settings.DbTransaction.Dispose()
                settings.DbTransaction = Nothing
            End If
            If settings.DbConnection IsNot Nothing Then
                settings.DbConnection.Dispose()
                settings.DbConnection = Nothing
            End If
        End Try
    End Sub
End Class
