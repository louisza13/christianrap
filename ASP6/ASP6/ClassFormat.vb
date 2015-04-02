'Author: Louis Andres
'Assignment: ASP4
'Date: 3/2/15
'Program Description: Format class

Public Class ClassFormat
    'Purpose: format phone number
    'Arguments: phone number
    'Returns: formatted number
    'Author: Louis Andres
    'Date: 2/11/15
    Public Function FormatPhone(strIN As String) As String

        If strIN <> "" Then
            Dim dblPhone As Double = strIN
            Dim strResult As String
            strResult = dblPhone.ToString("(###) ###-####")
            Return strResult
        End If

        Return ""

    End Function

End Class
