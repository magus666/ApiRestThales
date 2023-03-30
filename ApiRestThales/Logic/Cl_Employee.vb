Imports System.Data.SqlClient

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

End Class
