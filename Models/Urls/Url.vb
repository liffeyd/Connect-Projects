Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace Models.Urls

  <TableName("Connect_Projects_Urls")>
  <PrimaryKey("UrlId", AutoIncrement:=True)>
  <DataContract()>
  Partial Public Class Url
  Inherits UrlBase

#Region " Private Members "
#End Region
	
#Region " Constructors "
  Public Sub New()
   MyBase.New()
  End Sub
#End Region

#Region " Public Methods "
  Public Function GetUrlBase() As UrlBase
   Dim base As New UrlBase
   base.Description = Description
   base.IsDead = IsDead
   base.LastChange = LastChange
   base.LastChecked = LastChecked
   base.ProjectId = ProjectId
   base.Retries = Retries
   base.Url = Url
   base.UrlId = UrlId
   base.UrlType = UrlType
   Return base
  End Function
#End Region

 End Class
End Namespace

