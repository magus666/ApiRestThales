﻿Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports System.Web.Http

Namespace Controllers
    Public Class EmployeeController
        Inherits ApiController

        Dim ConexionSqlServer As New SqlConnection(My.Settings.StringConnection)
        Dim EmployeeClass As New Cl_Employee
        Dim Comando As SqlCommand
        Dim AdaptadorSql As New SqlDataAdapter()
        Dim EmployeeDataTable As New DataTable()

        <HttpGet>
        Public Async Function GetEmployee() As Task(Of List(Of EmployeeModel))
            Dim response = Await EmployeeClass.SelectEmployee()
            Return response
        End Function

        Public Function GetlEmployeeById(IdEmployee As Integer) As List(Of EmployeeModel)
            Try
                Dim GetEmployees As New List(Of EmployeeModel)()
                Dim EmployeeDataTable As New DataTable()
                ConexionSqlServer.Open()
                Comando = New SqlCommand("SPR_GET_EMPLOYEE_BYID", ConexionSqlServer)
                Comando.CommandType = CommandType.StoredProcedure
                Comando.Parameters.AddWithValue("@PAR_IDEMPLOYEE", IdEmployee)
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
                        GetEmployees.Add(Employees)
                    Next
                    Dim GetAllEmployees = (From Employees In GetEmployees Select New EmployeeModel With {
                                                                              .IdEmployee = Employees.IdEmployee,
                                                                              .NameEmployee = Employees.NameEmployee,
                                                                              .SalaryEmployee = Employees.SalaryEmployee,
                                                                              .AnualSalary = Employees.SalaryEmployee * 12,
                                                                              .AgeEmployee = Employees.AgeEmployee,
                                                                              .ImageEmployee = Employees.ImageEmployee}).ToList()

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