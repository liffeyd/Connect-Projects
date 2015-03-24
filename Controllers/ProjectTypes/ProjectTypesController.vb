Imports System
Imports Connect.DNN.Modules.Projects.Repositories
Imports Connect.DNN.Modules.Projects.Models.ProjectTypes

Namespace Controllers.ProjectTypes

 Partial Public Class ProjectTypesController

  Public Shared Function GetProjectTypes() As IEnumerable(Of ProjectType)

   Dim repo As New ProjectTypeRepository
   Return repo.Get()

  End Function

  Public Shared Function GetProjectType(projectTypeId As Int32) As ProjectType

   Dim repo As New ProjectTypeRepository
   Return repo.GetById(projectTypeId)

  End Function

  Public Shared Function AddProjectType(ByRef projecttype As ProjectTypeBase) As Integer

   Dim repo As New ProjectTypeBaseRepository
   repo.Insert(projecttype)
   Return projecttype.ProjectTypeId

  End Function

  Public Shared Sub UpdateProjectType(projecttype As ProjectTypeBase)

   Dim repo As New ProjectTypeBaseRepository
   repo.Update(projecttype)

  End Sub

  Public Shared Sub DeleteProjectType(projecttype As ProjectTypeBase)

   Dim repo As New ProjectTypeBaseRepository
   repo.Delete(projecttype)

  End Sub

 End Class
End Namespace

