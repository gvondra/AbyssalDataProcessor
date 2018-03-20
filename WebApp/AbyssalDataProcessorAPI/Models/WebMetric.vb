Public Interface WebMetric
    Property WebMetricId As Integer
    Property Url As String
    Property Method As String
    Property CreateTimestamp As Date
    Property Duration As Double
    Property Status As String
    Property Controller As String
    Property Attributes As List(Of WebMetricAttribute)
End Interface
