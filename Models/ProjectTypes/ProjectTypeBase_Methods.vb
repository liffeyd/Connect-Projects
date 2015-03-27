Imports System
Imports System.Data

Imports DotNetNuke.ComponentModel.DataAnnotations
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Tokens

Namespace Models.ProjectTypes
  Partial Public Class ProjectTypeBase
  Implements IHydratable
  Implements IPropertyAccess

#Region " IHydratable Methods "
  Public Overridable Sub Fill(dr As IDataReader) Implements IHydratable.Fill


  ProjectColor = Convert.ToString(Null.SetNull(dr.Item("ProjectColor"), ProjectColor))
  ProjectTypeId = Convert.ToInt32(Null.SetNull(dr.Item("ProjectTypeId"), ProjectTypeId))
  TypeDescription = Convert.ToString(Null.SetNull(dr.Item("TypeDescription"), TypeDescription))

 End Sub

 <IgnoreColumn()>
  Public Property KeyID() As Integer Implements IHydratable.KeyID
  Get
   Return ProjectTypeId
  End Get
  Set(value As Integer)
   ProjectTypeId = value
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
   Case "projectcolor"
    Return PropertyAccess.FormatString(ProjectColor, strFormat)
   Case "projecttypeid"
    Return ProjectTypeId.ToString(outputFormat, formatProvider)
   Case "typedescription"
    Return PropertyAccess.FormatString(TypeDescription, strFormat)
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
