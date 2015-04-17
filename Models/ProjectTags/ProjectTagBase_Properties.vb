Imports System
Imports System.Runtime.Serialization
Imports DotNetNuke.ComponentModel.DataAnnotations
Imports Connect.DNN.Modules.Projects.Common

Namespace Models.ProjectTags
 <TableName("Connect_Projects_ProjectTags")> <DataContract()>
 Partial Public Class ProjectTagBase

#Region " Private Members "
#End Region

#Region " Constructors "
  Public Sub New()
  End Sub

  Public Sub New(projectId As Int32, termId As Int32)
   Me.ProjectId = projectId
   Me.TermId = termId
  End Sub
#End Region

#Region " Public Properties "
  <DataMember()>
  Public Property ProjectId As Int32 = -1
  <DataMember()>
  Public Property TermId As Int32 = -1
#End Region

#Region " Methods "
  Public Sub ReadProjectTagBase(projecttag As ProjectTagBase)
   If projecttag.ProjectId > -1 Then ProjectId = projecttag.ProjectId
   If projecttag.TermId > -1 Then TermId = projecttag.TermId
  End Sub
#End Region

 End Class
End Namespace


