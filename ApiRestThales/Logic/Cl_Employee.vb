Imports System.Data.SqlClient
Imports System.Threading.Tasks

Public Class Cl_Employee

    Dim ConexionSqlServer As New SqlConnection(My.Settings.StringConnection)
    Dim Comando As SqlCommand
    Dim AdaptadorSql As New SqlDataAdapter()
    Dim RespuestaBaseDatos As String

    Public Function InsertEmployee(Employee As EmployeeModel) As String
        Try
            ConexionSqlServer.Open()
            Comando = New SqlCommand("SPR_INS_EMPLOYEE", ConexionSqlServer)
            Comando.CommandType = CommandType.StoredProcedure
            Comando.Parameters.AddWithValue("@PAR_NAME", Employee.NameEmployee)
            Comando.Parameters.AddWithValue("@PAR_MONTHSALARY", Employee.SalaryEmployee)
            Comando.Parameters.AddWithValue("@PAR_AGE", Employee.AgeEmployee)
            Comando.Parameters.AddWithValue("@PAR_IMAGE", Employee.ImageEmployee)

            Using RespuestaReader = Comando.ExecuteReader()
                If RespuestaReader.Read() Then
                    RespuestaBaseDatos = RespuestaReader.GetString(0)
                    Return RespuestaBaseDatos
                Else
                    Return Nothing
                End If
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            ConexionSqlServer.Close()
        End Try
    End Function

    Public Async Function SelectEmployee() As Task(Of List(Of EmployeeModel))
        Dim GetEmployees As New List(Of EmployeeModel)
        Await ConexionSqlServer.OpenAsync()


        Comando = New SqlCommand("SPR_GET_EMPLOYEE", ConexionSqlServer)
        Comando.CommandType = CommandType.StoredProcedure
        ' Agrega los parámetros necesarios al comando
        ' command.Parameters.Add(New SqlParameter("@ParameterName", "ParameterValue"))
        Using reader As SqlDataReader = Await Comando.ExecuteReaderAsync()
            While Await reader.ReadAsync()
                Dim Empleado As New EmployeeModel()
                Empleado.IdEmployee = reader.GetInt32(reader.GetOrdinal("Id_Employee"))
                Empleado.NameEmployee = reader.GetString(reader.GetOrdinal("Name_Employee"))
                Empleado.SalaryEmployee = reader.GetDouble(reader.GetOrdinal("Salary_Employee"))
                Empleado.AgeEmployee = reader.GetInt32(reader.GetOrdinal("Age_Employee"))
                Empleado.ImageEmployee = reader.GetString(reader.GetOrdinal("Image_Employee"))
                GetEmployees.Add(Empleado)
            End While
        End Using

        Dim GetAllEmployees = (From Employees In GetEmployees Select New EmployeeModel With {
                                                                              .IdEmployee = Employees.IdEmployee,
                                                                              .NameEmployee = Employees.NameEmployee,
                                                                              .SalaryEmployee = Employees.SalaryEmployee,
                                                                              .AnualSalary = Employees.SalaryEmployee * 12,
                                                                              .AgeEmployee = Employees.AgeEmployee,
                                                                              .ImageEmployee = Employees.ImageEmployee}).ToList()


        Return GetAllEmployees
    End Function

End Class
