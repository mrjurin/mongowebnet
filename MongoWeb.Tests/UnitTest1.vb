Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub ItShouldOutputHelloWorld()
        Dim exp = "Hello World"
        Dim act = "Hello World2"
        Assert.AreEqual(exp, act)
    End Sub

End Class