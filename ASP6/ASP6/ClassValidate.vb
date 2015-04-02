'Author: Louis Andres
'Assignment: ASP4
'Date: 3/2/15
'Program Description: Customer Validate class

Public Class ClassValidate



    Public Function CheckDecimal(strInput As String) As Decimal
        'Purpose: To validate decimals
        'Arguments: String input
        'Returns: Decimal output or -1 if invalid
        'Author: Louis Andres
        'Date: 1/22/15

        Dim decResult As Decimal
        Try
            decResult = Convert.ToDecimal(strInput)
        Catch ex As Exception
            Return -1
        End Try

        'Test for greater than zero
        If decResult < 0 Then
            Return -1
        End If
        Return decResult
    End Function


    Public Function CheckInteger(strInput As String) As Integer
        'Purpose: To validate intgers
        'Arguments: String input
        'Returns: Integer output or -1 if invalid
        'Author: Louis Andres
        'Date: 1/22/15
        Dim intResult As Integer
        Try
            intResult = Convert.ToInt32(strInput)
        Catch ex As Exception
            Return -1
        End Try

        'Test for greater than zero
        If intResult < 0 Then
            Return -1
        End If
        Return intResult
    End Function

    

    'Public Function CheckPassword(strInput As String) As String
    '    'Purpose:To validate password
    '    'Arguments: String input
    '    'Returns: donut or -1 if not valid
    '    'Author: Louis Andres
    '    'Date: 1/22/15
    '    If strInput = "donut" Then
    '        Return "donut"
    '    End If

    '    Return "-1"
    'End Function



    Public Function CheckForDigits(strIN As String) As Boolean
        'purpose: see if each character is a number
        'arguement: one string
        'return: true/false
        'Author: Louis Andres
        'Date: 2/17/15

        'check to see that each character is a number
        Dim i As Integer
        Dim strOne As String

        For i = 0 To Len(strIN) - 1
            'get one character from string
            strOne = strIN.Substring(i, 1)
            Select Case strOne
                'if character is 0-9 then keep going
                Case "0" To "9"
                    'if the character is anything else, stop looking and return false
                Case Else
                    Return False
            End Select
        Next

        'if we made it thru the loop, return true
        Return True
    End Function


    Public Function CheckForLetters(strIN As String) As Boolean
        'purpose: see if each character is a letter
        'arguement: one string
        'return: true/false
        'Author: Louis Andres
        'Date: 2/17/15

        'check to see that each character is a letter
        Dim i As Integer
        Dim strOne As String

        For i = 0 To Len(strIN) - 1
            'get one character from string
            strOne = strIN.Substring(i, 1)
            Select Case strOne.ToLower
                'if character is a-z then keep going
                Case "a" To "z"
                    'if the character is anything else, stop looking and return false
                Case Else
                    Return False
            End Select
        Next

        'if we made it thru the loop, return true
        Return True
    End Function

    Public Function CheckPhone(strIN As String) As Boolean
        'purpose: check if valid 10 digit phone number
        'arguement: one string
        'return: true/false
        'Author: Louis Andres
        'Date: 2/17/15
        'check length of input
        If strIN.Length <> 10 Then
            Return False
        End If
        'make sure only digits
        If CheckForDigits(strIN) = False Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Function CheckZip(strIN As String) As Boolean
        'purpose: check if valid zipcode 5 or 9 digits
        'arguement: one string
        'return: true/false
        'Author: Louis Andres
        'Date: 2/17/15
        'check length of input, 5 or 9
        Select Case strIN.Length
            Case 5
                Return True
            Case 9
                Return True
            Case Else
                Return False
        End Select

        'make sure only digits
        If CheckForDigits(strIN) = False Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function CheckState(strIN As String) As Boolean
        'purpose: check if state - 2 letters
        'arguement: one string
        'return: true/false
        'Author: Louis Andres
        'Date: 2/17/15
        'check length of input
        If strIN.Length <> 2 Then
            Return False
        End If
        'make sure only letters
        If CheckForLetters(strIN) = False Then
            Return False
        Else
            Return True
        End If
    End Function


    Public Function CheckInital(strIN As String) As Boolean
        'purpose: check if valid initial - 1 letter
        'arguement: one string
        'return: true/false
        'Author: Louis Andres
        'Date: 2/17/15
        'check length of input
        If strIN.Length <> 1 Then
            Return False
        End If
        'make sure only letters
        If CheckForLetters(strIN) = False Then
            Return False
        Else
            Return True
        End If
    End Function


    Public Function CheckPassword(strIN As String) As Boolean
        'purpose: check if valid password - starts with a letter, has one number and one letter and is 6-10 characters
        'arguement: one string
        'return: true/false
        'Author: Louis Andres
        'Date: 2/17/15
        'check first character for letter
        If CheckForLetters(strIN.Substring(0, 1)) = False Then
            Return False
        End If

        'check length 6-10
        Select Case Len(strIN)
            Case 6 To 10

            Case Else
                Return False
        End Select

        'integer counter
        Dim intDigitCount As Integer
        Dim strOne As String

        'check numbers and letters
        For i = 1 To Len(strIN) - 1
            strOne = strIN.Substring(i, 1)
            Select Case strOne.ToLower

                Case "a" To "z"

                Case "0" To "9"
                    intDigitCount += 1
                Case Else
                    'bad data
                    Return False
            End Select

        Next

        'check digits
        If intDigitCount > 0 Then
            Return True
        Else
            Return False
        End If
        Return True



    End Function

End Class
