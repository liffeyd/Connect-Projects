Imports System
Imports System.Runtime.Serialization
Imports DotNetNuke.ComponentModel.DataAnnotations
Imports Connect.DNN.Modules.Projects.Common

Namespace Models.ProjectTypes
  <TableName("Connect_Projects_ProjectTypes")>
  <PrimaryKey("ProjectTypeId", AutoIncrement:=True)>
  <DataContract()>
  Partial Public Class ProjectTypeBase

#Region " Private Members "
#End Region
	
#Region " Constructors "
  Public Sub New()
  End Sub

  Public Sub New(projectTypeId As Int32, typeDescription As String)
   Me.ProjectTypeId = projectTypeId
   Me.TypeDescription = typeDescription
  End Sub
#End Region
	
#Region " Public Properties "
  <DataMember()>
  Public Property ProjectTypeId As Int32 = -1
  <DataMember()>
  Public Property TypeDescription As String = ""
#End Region

#Region " Methods "
  Public Sub ReadProjectTypeBase(projecttype As ProjectTypeBase)
   If projecttype.ProjectTypeId > -1 Then ProjectTypeId = projecttype.ProjectTypeId
   If projecttype.TypeDescription IsNot Nothing Then TypeDescription = projecttype.TypeDescription
  End Sub
#End Region

 End Class
End Namespace


