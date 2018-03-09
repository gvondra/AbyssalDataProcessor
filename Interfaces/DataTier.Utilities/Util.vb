Public Module Util
    Public Function CreateParameter(ByVal providerFactory As IDbProviderFactory, ByVal name As String, ByVal type As DbType) As IDbDataParameter
        Dim parameter As IDbDataParameter = providerFactory.CreateParameter()
        If String.IsNullOrEmpty(name) = False Then
            parameter.ParameterName = name
        End If
        parameter.DbType = type
        Return parameter
    End Function

    Public Function GetParameterValue(ByVal value As Guid?) As Object
        If value.HasValue AndAlso value.Value.Equals(Guid.Empty) = False Then
            Return value.Value
        Else
            Return DBNull.Value
        End If
    End Function

    Public Function GetParameterValue(ByVal value As String) As Object
        If value Is Nothing Then
            value = String.Empty
        End If
        Return value.TrimEnd
    End Function

    Public Function GetParameterValue(ByVal value As Decimal?) As Object
        If value.HasValue Then
            Return value.Value
        Else
            Return DBNull.Value
        End If
    End Function

    Public Function GetParameterValue(ByVal value As Integer?) As Object
        If value.HasValue Then
            Return value.Value
        Else
            Return DBNull.Value
        End If
    End Function

    Public Function GetParameterValue(ByVal value As Short?) As Object
        If value.HasValue Then
            Return value.Value
        Else
            Return DBNull.Value
        End If
    End Function

    Public Function GetParameterValue(ByVal value As Date?) As Object
        If value.HasValue Then
            Return value.Value
        Else
            Return DBNull.Value
        End If
    End Function

    Public Function GetParameterValue(ByVal value As Byte()) As Object
        If value IsNot Nothing Then
            Return value
        Else
            Return DBNull.Value
        End If
    End Function

    Public Function GetParameterValue(ByVal value As Boolean?) As Object
        If value.HasValue Then
            Return value.Value
        Else
            Return DBNull.Value
        End If
    End Function

    Public Sub AddParameter(ByVal providerFactory As IDbProviderFactory,
                            ByVal parameterCollection As IList,
                            ByVal name As String,
                            ByVal dbType As DbType,
                            ByVal value As Object)

        Dim parameter As IDbDataParameter = CreateParameter(providerFactory, name, dbType)
        parameter.Value = value
        parameterCollection.Add(parameter)
    End Sub

End Module
