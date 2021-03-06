Imports System
Imports System.Data

Imports DotNetNuke.ComponentModel.DataAnnotations
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Tokens

Namespace Models.Projects
  Partial Public Class ProjectBase
  Implements IHydratable
  Implements IPropertyAccess

#Region " IHydratable Methods "
  Public Overridable Sub Fill(dr As IDataReader) Implements IHydratable.Fill

   FillAuditFields(dr)

  Aims = Convert.ToString(Null.SetNull(dr.Item("Aims"), Aims))
  ModuleId = Convert.ToInt32(Null.SetNull(dr.Item("ModuleId"), ModuleId))
  Dependencies = Convert.ToString(Null.SetNull(dr.Item("Dependencies"), Dependencies))
  Description = Convert.ToString(Null.SetNull(dr.Item("Description"), Description))
  FirstImage = Convert.ToString(Null.SetNull(dr.Item("FirstImage"), FirstImage))
  LicenseTypeId = Convert.ToInt32(Null.SetNull(dr.Item("LicenseTypeId"), LicenseTypeId))
  Owners = Convert.ToString(Null.SetNull(dr.Item("Owners"), Owners))
  People = Convert.ToString(Null.SetNull(dr.Item("People"), People))
  ProjectId = Convert.ToInt32(Null.SetNull(dr.Item("ProjectId"), ProjectId))
  ProjectName = Convert.ToString(Null.SetNull(dr.Item("ProjectName"), ProjectName))
  Status = Convert.ToString(Null.SetNull(dr.Item("Status"), Status))
  Visible = Convert.ToBoolean(Null.SetNull(dr.Item("Visible"), Visible))

 End Sub

 <IgnoreColumn()>
  Public Property KeyID() As Integer Implements IHydratable.KeyID
  Get
   Return ProjectId
  End Get
  Set(value As Integer)
   ProjectId = value
  End Set
 End Property
#End Region

#Region " IPropertyAccess Methods "
  Public Overridable Function GetProperty(strPropertyName As String, strFormat As String, formatProvider As System.Globalization.CultureInfo, accessingUser As DotNetNuke.Entities.Users.UserInfo, accessLevel As DotNetNuke.Services.Tokens.Scope, ByRef propertyNotFound As Boolean) As String Implements IPropertyAccess.GetProperty
   Dim outputFormat As String = strFormat
   If strFormat = String.Empty Then
    outputFormat = "D"
   End If
  Select Case strPropertyName.ToLower
   Case "aims"
    If Aims Is Nothing Then Return ""
    Return PropertyAccess.FormatString(CStr(Aims), strFormat)
   Case "moduleid"
    Return ModuleId.ToString(outputFormat, formatProvider)
   Case "createdbyuserid"
    Return CreatedByUserID.ToString(outputFormat, formatProvider)
   Case "createdondate"
    Return CreatedOnDate.ToString(outputFormat, formatProvider)
   Case "dependencies"
    If Dependencies Is Nothing Then Return ""
    Return PropertyAccess.FormatString(CStr(Dependencies), strFormat)
   Case "description"
    If Description Is Nothing Then Return ""
    Return PropertyAccess.FormatString(CStr(Description), strFormat)
   Case "firstimage"
    If FirstImage Is Nothing Then Return ""
    Return PropertyAccess.FormatString(CStr(FirstImage), strFormat)
   Case "lastmodifiedbyuserid"
    Return LastModifiedByUserID.ToString(outputFormat, formatProvider)
   Case "lastmodifiedondate"
    Return LastModifiedOnDate.ToString(outputFormat, formatProvider)
   Case "licensetypeid"
    Return LicenseTypeId.ToString(outputFormat, formatProvider)
   Case "owners"
    If Owners Is Nothing Then Return ""
    Return PropertyAccess.FormatString(CStr(Owners), strFormat)
   Case "people"
    If People Is Nothing Then Return ""
    Return PropertyAccess.FormatString(CStr(People), strFormat)
   Case "projectid"
    Return ProjectId.ToString(outputFormat, formatProvider)
   Case "projectname"
    Return PropertyAccess.FormatString(ProjectName, strFormat)
   Case "status"
    If Status Is Nothing Then Return ""
    Return PropertyAccess.FormatString(CStr(Status), strFormat)
   Case "visible"
    Return Visible.ToString
   Case "visibleyesno"
    Return PropertyAccess.Boolean2LocalizedYesNo(Visible, formatProvider)
   Case Else
    propertyNotFound = True
  End Select

  Return Null.NullString
 End Function

  <IgnoreColumn()>
 Public ReadOnly Property Cacheability() As DotNetNuke.Services.Tokens.CacheLevel Implements DotNetNuke.Services.Tokens.IPropertyAccess.Cacheability
  Get
   Return CacheLevel.fullyCacheable
  End Get
 End Property
#End Region

 End Class
End Namespace
