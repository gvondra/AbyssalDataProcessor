Imports System.Security.Claims
Public Interface IUserFactory
    Inherits AbyssalDataProcessor.client.Framework.IUserFactory

    Overloads Function [Get](ByVal principal As ClaimsPrincipal) As IUser
End Interface
