Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace Models.ProjectTypes

  <TableName("Connect_Projects_ProjectTypes")>
  <PrimaryKey("ProjectTypeId", AutoIncrement:=True)>
  <DataContract()>
  Partial Public Class ProjectType
  Inherits ProjectTypeBase

#Region " Private Members "
#End Region
	
#Region " Constructors "
  Public Sub New()
   MyBase.New()
  End Sub
#End Region

#Region " Public Methods "
  Public Function GetProjectTypeBase() As ProjectTypeBase
   Dim base As New ProjectTypeBase
   base.ProjectTypeId = ProjectTypeId
   base.TypeDescription = TypeDescription
   Return base
  End Function
#End Region

 End Class
End Namespace

