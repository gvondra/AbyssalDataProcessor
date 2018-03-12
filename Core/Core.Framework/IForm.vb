Imports System.Xml
Public Interface IForm
    ReadOnly Property FormId As Guid
    ReadOnly Property FormTypeId As enumFormType
    Property Style As enumFormStyle
    ReadOnly Property Content As XmlNode
End Interface
