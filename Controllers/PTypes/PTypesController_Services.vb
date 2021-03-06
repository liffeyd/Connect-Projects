Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Imports DotNetNuke.Web.Api

Namespace Controllers.PTypes

 Partial Public Class PTypesController
  Inherits DnnApiController

#Region " Service Methods "
  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
  Public Function [Get]() As HttpResponseMessage
   Return Request.CreateResponse(HttpStatusCode.OK, GetPTypes())
  End Function
#End Region

 End Class
End Namespace
