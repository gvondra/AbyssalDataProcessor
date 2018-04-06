Imports System.Xml
Public Interface IForm
    Inherits ISavable

    ReadOnly Property FormId As Guid
    ReadOnly Property OrganizationId As Guid
    ReadOnly Property [Type] As Int16
    Property Style As Int16
    ReadOnly Property Content As String
    ReadOnly Property CreateTimestamp As Date

End Interface
