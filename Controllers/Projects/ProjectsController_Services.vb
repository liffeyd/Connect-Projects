Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports Connect.DNN.Modules.Projects.Models.Projects

Imports DotNetNuke.Web.Api

Namespace Controllers.Projects

 Partial Public Class ProjectsController
  Inherits ModuleApiController

#Region " Service Methods "
  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
  Public Function Projects() As HttpResponseMessage
   Dim res As IEnumerable(Of Project)
   If Security.Moderator Then
    res = GetProjects(ActiveModule.ModuleID)
   Else
    res = GetProjects(ActiveModule.ModuleID).Where(Function(p) p.Visible Or p.CreatedByUserID = UserInfo.UserID)
   End If
   Return Request.CreateResponse(HttpStatusCode.OK, res)
  End Function

  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
  Public Function Project(id As Integer) As HttpResponseMessage
   If id = -1 Then
    Return Request.CreateResponse(HttpStatusCode.OK, New Project)
   Else
    Dim res As Project = GetProject(ActiveModule.ModuleID, id)
    If Not res.Visible Then
     If res.CreatedByUserID <> UserInfo.UserID Then
      If Not Security.Moderator Then
       Return Request.CreateResponse(HttpStatusCode.OK, New Project)
      End If
     End If
    End If
    Return Request.CreateResponse(HttpStatusCode.OK, res)
   End If
  End Function

  <HttpPost()>
  <DnnModuleAuthorize(PermissionKey:="SUBMITTER")>
  <ValidateAntiForgeryToken()>
  Public Function Put(project As ProjectBase) As HttpResponseMessage
   If project.ProjectId = -1 Then
    Dim projectToUpdate As New ProjectBase
    projectToUpdate.ReadProjectBase(project)
    projectToUpdate.ModuleId = ActiveModule.ModuleID
    If Security.Moderator Then projectToUpdate.Visible = project.Visible
    AddProject(projectToUpdate, UserInfo.UserID)
   Else
    Dim projectToUpdate As ProjectBase = GetProject(ActiveModule.ModuleID, project.ProjectId).GetProjectBase()
    If projectToUpdate.CreatedByUserID = UserInfo.UserID Or Security.Moderator Then
     projectToUpdate.ReadProjectBase(project)
     If Security.Moderator Then projectToUpdate.Visible = project.Visible
     UpdateProject(projectToUpdate, UserInfo.UserID)
    End If
   End If
   Return Request.CreateResponse(HttpStatusCode.OK, True)
  End Function

  <HttpPost()>
  <DnnModuleAuthorize(PermissionKey:="MODERATOR")>
  <ValidateAntiForgeryToken()>
  Public Function Approve(id As Integer, approved As Boolean) As HttpResponseMessage
   Dim projectToUpdate As ProjectBase = GetProject(ActiveModule.ModuleID, id).GetProjectBase()
   projectToUpdate.Visible = approved
   UpdateProject(projectToUpdate, UserInfo.UserID)
   Return Request.CreateResponse(HttpStatusCode.OK, True)
  End Function

  <HttpPost()>
  <DnnModuleAuthorize(PermissionKey:="SUBMITTER")>
  <ValidateAntiForgeryToken()>
  Public Function Delete(id As Integer) As HttpResponseMessage
   Dim projectToDelete As ProjectBase = GetProject(ActiveModule.ModuleID, id).GetProjectBase()
   If projectToDelete.CreatedByUserID = UserInfo.UserID Or Security.Moderator Then
    DeleteProject(projectToDelete)
   End If
   Return Request.CreateResponse(HttpStatusCode.OK, True)
  End Function
#End Region

 End Class
End Namespace
