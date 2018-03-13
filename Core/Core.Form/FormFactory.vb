Imports AbyssalDataProcessor.DataTier.Core.Models
Public Class FormFactory
    Implements IFormFactory

    Private m_formSerializerFactory As IFormSerializerFactory

    Public Sub New(ByVal formSerializerFactory As IFormSerializerFactory)
        m_formSerializerFactory = formSerializerFactory
    End Sub

    Public Function CreateForm(user As IUser, formType As enumFormType, style As enumFormStyle, content As XmlNode) As IForm Implements IFormFactory.CreateForm
        Return New Form(user, formType, style, content)
    End Function

    Public Function CreateRoleRequest() As IRoleRequest Implements IFormFactory.CreateRoleRequest
        Return New Forms.RoleRequest(Me, m_formSerializerFactory)
    End Function
End Class
