﻿Imports DotNetNuke.Web.Api

Namespace Common
 Public Class RouteMapper
  Implements IServiceRouteMapper

#Region " IServiceRouteMapper "
  Public Sub RegisterRoutes(mapRouteManager As DotNetNuke.Web.Api.IMapRoute) Implements DotNetNuke.Web.Api.IServiceRouteMapper.RegisterRoutes
   mapRouteManager.MapHttpRoute("Connect/Projects", "Projects1", "{controller}/{action}/{id}", Nothing, New With {.id = "\d*"}, New String() {
   "Connect.DNN.Modules.Projects.Controllers",
   "Connect.DNN.Modules.Projects.Controllers.Projects"})
   mapRouteManager.MapHttpRoute("Connect/Projects", "Projects2", "{controller}/{action}", Nothing, Nothing, New String() {
                                "Connect.DNN.Modules.Projects.Controllers",
                                "Connect.DNN.Modules.Projects.Controllers.Projects"})
  End Sub
#End Region

 End Class
End Namespace