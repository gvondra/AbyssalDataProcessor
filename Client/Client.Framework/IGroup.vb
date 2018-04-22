Public Interface IGroup
    Inherits ISavable

    ReadOnly Property GroupId As Guid
    ReadOnly Property OrganizationId As Guid
    Property Name As String
End Interface
