Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Imports DotNetNuke.Web.Api

Namespace Controllers.Projects

 Partial Public Class ProjectsController
  Inherits DnnApiController

#Region " Service Methods "
  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
  Public Function MyMethod(id As Integer) As HttpResponseMessage
   Dim res As Boolean = True
   Return Request.CreateResponse(HttpStatusCode.OK, res)
  End Function
#End Region

 End Class
End Namespace
