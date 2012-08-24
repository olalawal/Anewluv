Imports Microsoft.VisualBasic



Public Class ValidateStrings

    'function to make a certain string does not contain a charcter you do not want

    Public Shared Function BadChars(ByRef strText As String, _
    ByRef strUnwanted As String) As Boolean
        Dim currLoc As Integer
        Dim StringLength As Integer
        Dim tmpChar As String

        StringLength = Len(strText)
        For currLoc = 1 To StringLength
            tmpChar = Mid(strText, currLoc, 1)
            If InStr(strUnwanted, tmpChar) Then
                ' Replace with a space
                Return True
            End If
        Next

        Return False
    End Function




End Class


''' <summary>
''' Custom string utility methods.
''' </summary>
Public NotInheritable Class StringTool
    Private Sub New()
    End Sub
    ''' <summary>
    ''' Get a substring of the first N characters.
    ''' </summary>
    Public Shared Function Truncate(ByVal source As String, ByVal length As Integer) As String
        If source.Length > length Then
            source = source.Substring(0, length)
        End If
        Return source
    End Function

    ''' <summary>
    ''' Get a substring of the first N characters. [Slow]
    ''' </summary>
    Public Shared Function Truncate2(ByVal source As String, ByVal length As Integer) As String
        Return source.Substring(0, Math.Min(length, source.Length))
    End Function
End Class

