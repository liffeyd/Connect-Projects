Imports System
Imports System.Runtime.Serialization
Imports DotNetNuke.ComponentModel.DataAnnotations

Namespace Models.ProjectTypes
  <TableName("Connect_Projects_ProjectTypes")>  <DataContract()>
  Partial Public Class ProjectTypeBase

#Region " Private Members "
#End Region
	
#Region " Constructors "
  Public Sub New()
  End Sub

  Public Sub New(projectId As Int32, typeId As Int32)
   Me.ProjectId = projectId
   Me.TypeId = typeId
  End Sub
#End Region
	
#Region " Public Properties "
  <DataMember()>
  Public Property ProjectId As Int32 = -1
  <DataMember()>
  Public Property TypeId As Int32 = -1
#End Region

#Region " Methods "
  Public Sub ReadProjectTypeBase(projecttype As ProjectTypeBase)
   If projecttype.ProjectId > -1 Then ProjectId = projecttype.ProjectId
   If projecttype.TypeId > -1 Then TypeId = projecttype.TypeId
  End Sub
#End Region

 End Class
End Namespace


