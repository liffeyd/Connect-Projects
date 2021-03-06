Imports System
Imports System.Data

Imports DotNetNuke.ComponentModel.DataAnnotations
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Tokens

Namespace Models.PTypes
  Partial Public Class PTypeBase
  Implements IHydratable
  Implements IPropertyAccess

#Region " IHydratable Methods "
  Public Overridable Sub Fill(dr As IDataReader) Implements IHydratable.Fill


  TypeDescription = Convert.ToString(Null.SetNull(dr.Item("TypeDescription"), TypeDescription))
  TypeIcon = Convert.ToString(Null.SetNull(dr.Item("TypeIcon"), TypeIcon))
  TypeId = Convert.ToInt32(Null.SetNull(dr.Item("TypeId"), TypeId))

 End Sub

 <IgnoreColumn()>
  Public Property KeyID() As Integer Implements IHydratable.KeyID
  Get
   Return TypeId
  End Get
  Set(value As Integer)
   TypeId = value
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
   Case "typedescription"
    Return PropertyAccess.FormatString(TypeDescription, strFormat)
   Case "typeicon"
    If TypeIcon Is Nothing Then Return ""
    Return PropertyAccess.FormatString(CStr(TypeIcon), strFormat)
   Case "typeid"
    Return TypeId.ToString(outputFormat, formatProvider)
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
