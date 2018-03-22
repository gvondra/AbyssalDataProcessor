Public Interface IOrganization
    Inherits ISavable

    ReadOnly Property OrganizationId As Guid
    Property Name As String
End Interface
