Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Web.Http
Imports Connect.DNN.Modules.Projects.Controllers.ProjectTags
Imports Connect.DNN.Modules.Projects.Controllers.ProjectTypes
Imports Connect.DNN.Modules.Projects.Controllers.PTypes
Imports Connect.DNN.Modules.Projects.Controllers.Urls
Imports Connect.DNN.Modules.Projects.Integration
Imports Connect.DNN.Modules.Projects.Models.Projects
Imports DotNetNuke.Security
Imports DotNetNuke.Services.Social.Notifications

Imports DotNetNuke.Web.Api
Imports Newtonsoft.Json

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
    res = GetProjects(ActiveModule.ModuleID).Where(Function(p) (p.Visible And p.NrLiveLinks > 0) Or p.CreatedByUserID = UserInfo.UserID)
   End If
   Return Request.CreateResponse(HttpStatusCode.OK, res)
  End Function

  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
  Public Function Project(id As Integer) As HttpResponseMessage
   If id = -1 Then
    Dim res As New Project
    res.ProjectTypes = PTypesController.GetSelectionList(-1)
    Return Request.CreateResponse(HttpStatusCode.OK, res)
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

  Public Class ProjectPutDTO
   Public Property project As String
  End Class
  <HttpPost()>
  <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.Edit, PermissionKey:="SUBMITTER")>
  <ValidateAntiForgeryToken()>
  Public Function Put(data As ProjectPutDTO) As HttpResponseMessage
   Dim project As Project = JsonConvert.DeserializeObject(Of Project)(data.project)
   Dim projectToUpdate As New ProjectBase
   Dim projectId As Integer = project.ProjectId
   If project.ProjectId = -1 Then
    projectToUpdate.ReadProjectBase(project)
    projectToUpdate.ModuleId = ActiveModule.ModuleID
    If Security.Moderator Then projectToUpdate.Visible = project.Visible
    projectId = AddProject(projectToUpdate, UserInfo.UserID)
    If Not project.Visible Then
     NotificationController.ProjectPendingApproval(ActiveModule.PortalID, ActiveModule.TabID, ActiveModule.ModuleID, projectId)
    Else
     JournalController.AddProjectToJournal(ActiveModule.PortalID, ActiveModule.TabID, ActiveModule.ModuleID, projectToUpdate.ProjectId)
    End If
   Else
    projectToUpdate = GetProject(ActiveModule.ModuleID, project.ProjectId).GetProjectBase()
    If projectToUpdate.CreatedByUserID = UserInfo.UserID Or Security.Moderator Then
     Dim previouslyApproved As Boolean = projectToUpdate.Visible
     projectToUpdate.ReadProjectBase(project)
     If Security.Moderator Then projectToUpdate.Visible = project.Visible
     UpdateProject(projectToUpdate, UserInfo.UserID)
     If Not previouslyApproved And projectToUpdate.Visible Then
      JournalController.AddProjectToJournal(ActiveModule.PortalID, ActiveModule.TabID, ActiveModule.ModuleID, projectToUpdate.ProjectId)
     End If
    End If
   End If
   If project.SelectedProjectTypes IsNot Nothing Then
    ProjectTypesController.SetProjectTypes(projectId, project.SelectedProjectTypes)
   End If
   If project.Urls IsNot Nothing Then
    UrlsController.SetProjectUrls(projectId, project.Urls)
   End If
   If project.ProjectTags IsNot Nothing Then
    ProjectTagsController.SetProjectTags(projectId, project.ProjectTags)
   End If
   Return Request.CreateResponse(HttpStatusCode.OK, projectId)
  End Function

  <HttpPost()>
  <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.Edit, PermissionKey:="MODERATOR")>
  <ValidateAntiForgeryToken()>
  Public Function Approve(id As Integer, approved As Boolean) As HttpResponseMessage
   Dim projectToUpdate As ProjectBase = GetProject(ActiveModule.ModuleID, id).GetProjectBase()
   projectToUpdate.Visible = approved
   UpdateProject(projectToUpdate, UserInfo.UserID)
   JournalController.AddProjectToJournal(ActiveModule.PortalID, ActiveModule.TabID, ActiveModule.ModuleID, projectToUpdate.ProjectId)
   Return Request.CreateResponse(HttpStatusCode.OK, True)
  End Function

  Public Class NotificationDTO
   Public Property NotificationId As Integer
  End Class

  <HttpPost()>
  <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.Edit, PermissionKey:="MODERATOR")>
  <ValidateAntiForgeryToken()>
  Public Function Approve(postData As NotificationDTO) As HttpResponseMessage
   Dim notify As Notification = NotificationsController.Instance.GetNotification(postData.NotificationId)
   Dim nKey As New NotificationKey(notify.Context)
   Dim projectToUpdate As ProjectBase = GetProject(ActiveModule.ModuleID, nKey.ProjectId).GetProjectBase()
   projectToUpdate.Visible = True
   UpdateProject(projectToUpdate, UserInfo.UserID)
   NotificationsController.Instance().DeleteNotification(postData.NotificationId)
   JournalController.AddProjectToJournal(ActiveModule.PortalID, ActiveModule.TabID, ActiveModule.ModuleID, projectToUpdate.ProjectId)
   Return Request.CreateResponse(HttpStatusCode.OK, New With {.Result = "success"})
  End Function

  <HttpPost()>
  <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.Edit, PermissionKey:="SUBMITTER")>
  <ValidateAntiForgeryToken()>
  Public Function Delete(id As Integer) As HttpResponseMessage
   Dim projectToDelete As ProjectBase = GetProject(ActiveModule.ModuleID, id).GetProjectBase()
   If projectToDelete.CreatedByUserID = UserInfo.UserID Or Security.Moderator Then
    DeleteProject(projectToDelete)
    NotificationController.RemoveProjectPendingNotification(ActiveModule.ModuleID, id)
   End If
   Return Request.CreateResponse(HttpStatusCode.OK, True)
  End Function

  <HttpPost()>
  <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.Edit, PermissionKey:="MODERATOR")>
  <ValidateAntiForgeryToken()>
  Public Function Delete(postData As NotificationDTO) As HttpResponseMessage
   Dim notify As Notification = NotificationsController.Instance.GetNotification(postData.NotificationId)
   Dim nKey As New NotificationKey(notify.Context)
   Dim projectToDelete As ProjectBase = GetProject(ActiveModule.ModuleID, nKey.ProjectId).GetProjectBase()
   If projectToDelete.CreatedByUserID = UserInfo.UserID Or Security.Moderator Then
    DeleteProject(projectToDelete)
   End If
   NotificationsController.Instance().DeleteNotification(postData.NotificationId)
   Return Request.CreateResponse(HttpStatusCode.OK, New With {.Result = "success"})
  End Function

  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
  Public Function Pdf(id As Integer) As HttpResponseMessage
   Dim res As New HttpResponseMessage(HttpStatusCode.OK)
   Dim doc As New Documents.ProjectDocument(ActiveModule.ModuleID, id)
   Dim mem As New IO.MemoryStream
   doc.Save(mem)
   mem.Seek(0, IO.SeekOrigin.Begin)
   res.Content = New StreamContent(mem)
   res.Content.Headers.ContentType = New MediaTypeHeaderValue("application/pdf")
   res.Content.Headers.ContentDisposition = New ContentDispositionHeaderValue("attachment")
   res.Content.Headers.ContentDisposition.FileName = String.Format("Project-{0}.pdf", id)
   Return res
  End Function
#End Region

 End Class
End Namespace
