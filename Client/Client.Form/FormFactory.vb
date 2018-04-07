Public Class FormFactory
    Implements IFormFactory

    Public Function CreateForm(organizationId As Guid, user As IUser, formType As Int16, style As Int16, content As String) As IForm Implements IFormFactory.CreateForm
        Return New Form(organizationId, user, formType, content) With {.Style = style}
    End Function
End Class
