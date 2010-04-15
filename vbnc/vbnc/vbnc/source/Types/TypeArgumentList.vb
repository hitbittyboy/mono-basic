' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

''' <summary>
''' TypeArgumentList  ::= TypeName  |	TypeArgumentList  ,  TypeName
''' </summary>
''' <remarks></remarks>
Public Class TypeArgumentList
    Inherits BaseList(Of TypeName)

    Private m_ArgumentCollection As Mono.Cecil.GenericArgumentCollection

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    ReadOnly Property ArgumentCollection() As Mono.Cecil.GenericArgumentCollection
        Get
            If m_ArgumentCollection Is Nothing Then
                m_ArgumentCollection = New Mono.Cecil.GenericArgumentCollection(Nothing)
                For i As Integer = 0 To Count - 1
                    m_ArgumentCollection.Add(Item(i).ResolvedType)
                Next
            End If
            Return m_ArgumentCollection
        End Get
    End Property

    <Obsolete()> _
    Function AsTypeArray() As Mono.Cecil.TypeReference()
        Dim result As New Generic.List(Of Mono.Cecil.TypeReference)
        For Each arg As TypeName In Me
            result.Add(arg.ResolvedType)
        Next
        Return result.ToArray
    End Function

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As TypeArgumentList
        If NewParent Is Nothing Then NewParent = Me.Parent
        Dim result As New TypeArgumentList(NewParent)
        For Each item As TypeName In Me
            result.Add(item.Clone(result))
        Next
        Return result
    End Function
End Class
