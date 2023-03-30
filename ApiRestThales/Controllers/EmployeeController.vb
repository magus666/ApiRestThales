Imports System.Data.SqlClient
Imports System.Web.Http

Namespace Controllers
    Public Class EmployeeController
        Inherits ApiController

        Dim ConexionSqlServer As New SqlConnection(My.Settings.StringConnection)
        Dim EmployeeClass As New Cl_Employee
        Dim Comando As SqlCommand
        Dim AdaptadorSql As New SqlDataAdapter()
        Dim EmployeeDataTable As New DataTable()
        ''' <summary>
        ''' Get Method to obtain all the Employees
        ''' </summary>
        ''' <returns></returns>
        <HttpGet>
        Public Function GetlEmployees() As List(Of EmployeeModel)
            Try
                Dim GetAllEmployees As New List(Of EmployeeModel)()
                ConexionSqlServer.Open()
                Comando = New SqlCommand("SPR_GET_EMPLOYEE", ConexionSqlServer) With {
                .CommandType = CommandType.StoredProcedure,
                .Connection = ConexionSqlServer
                }
                AdaptadorSql.SelectCommand = Comando
                AdaptadorSql.Fill(EmployeeDataTable)
                If EmployeeDataTable.Rows.Count > 0 Then
                    For i As Integer = 0 To EmployeeDataTable.Rows.Count - 1
                        Dim Employees As New EmployeeModel With {
                        .IdEmployee = Convert.ToInt32(EmployeeDataTable.Rows(i)(0)),
                        .NameEmployee = EmployeeDataTable.Rows(i)(1),
                        .SalaryEmployee = Convert.ToDouble(EmployeeDataTable(i)(2)),
                        .AgeEmployee = Convert.ToInt32(EmployeeDataTable.Rows(i)(3)),
                        .ImageEmployee = EmployeeDataTable.Rows(i)(4)
                    }
                        GetAllEmployees.Add(Employees)
                    Next
                    Return GetAllEmployees
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message, ex)
            Finally
                ConexionSqlServer.Close()
            End Try
        End Function
        ''' <summary>
        ''' Post Method to Insert a Employee
        ''' </summary>
        ''' <param name="Employee">Parametros de Entrada</param>
        ''' <returns></returns>

        <AcceptVerbs("POST")>
        Public Function PostEmployee(Employee As EmployeeModel) As String
            Dim response = EmployeeClass.InsertEmployee(Employee)
            Return response
        End Function

    End Class
End Namespace