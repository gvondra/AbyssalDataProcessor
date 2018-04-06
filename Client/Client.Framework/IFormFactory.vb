Public Interface IFormFactory
    Function CreateForm(ByVal organizationId As Guid, ByVal user As IUser, ByVal formType As Int16, ByVal style As Int16, ByVal content As String) As IForm
End Interface
