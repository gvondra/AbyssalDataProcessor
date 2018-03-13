Imports System.Xml
Public Interface IForm
    Inherits ISavable

    ReadOnly Property FormId As Guid
    ReadOnly Property [Type] As enumFormType
    Property Style As enumFormStyle
    ReadOnly Property Content As XmlNode
End Interface
