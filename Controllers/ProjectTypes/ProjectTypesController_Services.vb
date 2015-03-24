Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Imports DotNetNuke.Web.Api

Namespace Controllers.ProjectTypes

 Partial Public Class ProjectTypesController
  Inherits DnnApiController

#Region " Service Methods "
  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
  Public Function Types() As HttpResponseMessage
   Return Request.CreateResponse(HttpStatusCode.OK, GetProjectTypes)
  End Function
#End Region

 End Class
End Namespace
