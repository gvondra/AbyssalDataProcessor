Public Module Util
    Public Function CreateParameter(ByVal providerFactory As IDbProviderFactory, ByVal name As String, ByVal type As DbType) As IDbDataParameter
        Dim parameter As IDbDataParameter = providerFactory.CreateParameter()
        If String.IsNullOrEmpty(name) = False Then
            parameter.ParameterName = name
        End If
        parameter.DbType = type
        Return parameter
    End Function
End Module
