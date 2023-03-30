﻿Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        ' Configuración y servicios de Web API

        ' Rutas de Web API
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
                name:="ActionApi",
                routeTemplate:="api/{controller}/{action}/{id}",
                defaults:=New With {.id = RouteParameter.Optional}
           )
    End Sub
End Module