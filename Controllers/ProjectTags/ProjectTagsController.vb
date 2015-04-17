Imports System
Imports Connect.DNN.Modules.Projects.Repositories
Imports Connect.DNN.Modules.Projects.Models.ProjectTags
Imports DotNetNuke.Data

Namespace Controllers.ProjectTags

 Partial Public Class ProjectTagsController

  Public Shared Function GetProjectTags(projectId As Int32) As IEnumerable(Of ProjectTag)

   Dim repo As New ProjectTagRepository
   Return repo.Find("WHERE ProjectId = @0", projectId)

  End Function

  Public Shared Sub SetProjectTags(projectId As Integer, tagIds As List(Of Integer))
   DataProvider.Instance().ExecuteNonQuery("Connect_Projects_SetProjectTags", projectId, String.Join(",", tagIds))
  End Sub

 End Class
End Namespace

