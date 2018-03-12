Imports System.Xml
Public Interface IFormFactory
    Function CreateRoleRequest() As Forms.IRoleRequest
    Function CreateForm(ByVal user As IUser, ByVal formType As enumFormType, ByVal style As enumFormStyle, ByVal content As XmlNode) As IForm
End Interface
