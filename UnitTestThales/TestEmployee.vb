Imports ApiRestThales

<TestClass()>
Public Class TestEmployee
    Dim GetEmployeClass As New Cl_Employee

    <TestMethod()>
    Public Sub TestGetEmployee()
        Try
            Dim GetEmployeeModelText As New EmployeeModel With
                {
                .NameEmployee = "Fernando",
                .SalaryEmployee = "450000",
                .AgeEmployee = "56",
                .ImageEmployee = "Imagen"
                }
            Dim retorno As String = GetEmployeClass.InsertEmployee(GetEmployeeModelText)
            Assert.AreEqual("GUARDADO CON EXITO", retorno)
        Catch ex As Exception

        End Try

    End Sub

End Class
