
Imports System.Net



'this class contians allot of functions that are generic to handling stuff on any user profile
Public Class CommonFunctions





    'date time functions '
    '***********************************************************
    'this function will send back when the member last logged in
    'be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
    'Ola Lawal 7/10/2009 feel free to drill down even to the day
#Region "Date and Time Functions"
    'date time functions '
    '***********************************************************
    'this function will send back when the member last logged in
    'be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
    'Ola Lawal 7/10/2009 feel free to drill down even to the day

    Public Shared Function CheckLastLoggedIn(ByVal strProfileID As String, ByVal LoginDate As Date) As String


        If LoginDate = Nothing Or CType(LoginDate, String) = "" Then
            Return "Last Six Months"
        End If

        ''you can compare dates and times the same as you would any other number
        Dim SomeDate As Date

        Dim DateThreeDaysAgo As Date
        Dim DateThreeWeeksago As Date
        Dim DateOneWeekAgo As Date
        Dim DateThreeMonthsAgo As Date
        Dim DateSixMonthsAgo As Date
        SomeDate = Now

        DateThreeDaysAgo = DateAdd("d", -3, SomeDate) 'Subtract 3 days
        DateOneWeekAgo = DateAdd("ww", -1, SomeDate) 'Subtract 1 weeks
        DateThreeWeeksago = DateAdd("ww", -3, SomeDate) 'Subtract 3 weeks
        DateThreeMonthsAgo = DateAdd("ww", -12, SomeDate) 'Subtract 12 weeks =3 months
        DateSixMonthsAgo = DateAdd("ww", -24, SomeDate) 'Subtract 24 weeks =6 months

        If LoginDate > DateThreeDaysAgo Then
            Return " Last <b>Three Days</b>"
        ElseIf LoginDate > DateOneWeekAgo Then
            Return " Last <b>Week</b> "
        ElseIf LoginDate > DateThreeWeeksago Then
            Return " Last <b>Three Weeks</b> "
        ElseIf LoginDate > DateThreeMonthsAgo Then
            Return " The Last <b>Three Months</b> "
        ElseIf LoginDate > DateSixMonthsAgo Then
            Return " the Last <b>Six Months</b>"
        Else
            Return " Last Year"
        End If

        'if we get here return the genric item 
        Return " the Last Week"
        'Dim Label3 As Date = DateAdd("ww", -52, SomeDate) 'subtract 52 weeks

    End Function

    Public Shared Function GetAgeFromDateTime(ByVal strDate As String) As Integer

        Dim dateBirthDate As Date = CType(strDate, Date)

        Dim years As Integer = DateTime.Now.Year - dateBirthDate.Year
        ' subtract another year if we're before the
        ' birth day in the current year
        If DateTime.Now.Month < dateBirthDate.Month Or (DateTime.Now.Month = dateBirthDate.Month And DateTime.Now.Day < dateBirthDate.Day) Then
            years = years - 1
        End If

        Return years
    End Function


    '


    

#End Region



    'this function formats the location feild on the persons search profile
    'i.e if it is a USA person it will get the person's City and State
    'it is a foreiner it will only get ther data the person entered in the location feild
    '**********************************************************************************************************
    Public Shared Function FormatDistance(ByVal lat1 As Double, ByVal long1 As Double, ByVal lat2 As Double, ByVal long2 As Double) As String
        Dim dblTemp As Double


        dblTemp = SpatialFunctions.GetdistanceBetweenMembers(lat1, long1, lat2, long2, "N")
        ' MsgBox(dblTemp)
        If dblTemp >= 0 Then
            Return CType(dblTemp, Integer)
            'on any data errors return "NA"
        Else : Return "NA"
        End If
        'no resposne redirect in the generic search since we want them to still have access
        'to thier search resuslts
        ' Else
        'if user is not authenticated , force a login
        '    Response.Redirect("Login.aspx")



    End Function






    

    

    
End Class



