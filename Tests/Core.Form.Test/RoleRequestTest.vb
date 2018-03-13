Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
<TestClass()> Public Class RoleRequestTest

    <TestMethod()> Public Sub CreateFormTest()
        Dim factory As New FormFactory(New FormSerializerFactory)
        Dim roleRequest As IRoleRequest = factory.CreateRoleRequest()
        Dim form As IForm
        Dim user As New Mock(Of IUser)()

        Assert.IsInstanceOfType(roleRequest, GetType(RoleRequest))

        roleRequest.FullName = "Test Full Name"
        roleRequest.Comment = "Detailed comment here"

        form = roleRequest.CreateForm(user.Object)

        Assert.IsInstanceOfType(form, GetType(AbyssalDataProcessor.Core.Form.Form))
        Assert.AreEqual(Of enumFormType)(enumFormType.RoleRequest, form.Type)
        Assert.AreEqual(Of enumFormStyle)(enumFormStyle.RoleRequest, form.Style)
        Assert.IsNotNull(form.Content)
    End Sub

End Class