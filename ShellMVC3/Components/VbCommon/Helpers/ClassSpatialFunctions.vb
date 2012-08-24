Imports Microsoft.VisualBasic
Imports System







Public Class SpatialFunctions

    'a group of functions designed to translate items from the proifles table and return the correspointing
    'value from another table, i.e the formattibe takes a tribID value and transforms it into an actual value


    '7/10/09 needs to be fixed
    'this function returns the the statename
    'updated 1/28/2008 ola lawal, error checking and change to return nothing
    'Public Shared Function DistBTWMembers(ByVal ProfileID1 As String, ByVal profileID2 As String, ByVal strConnectionString As String) As Double
    '    Dim StrSQL As String = "Select PostalCode from profilesData Where profileID="
    '    Data_Access.OpenDatingDB()
    '    Dim ConnectionReader As Data.SqlClient.SqlDataReader
    '    Dim intLatLonProfile1(2) As Double
    '    Dim intLatLonProfile2(2) As Double


    '    Try
    '        ' create a reader for the 1s user ID
    '        ConnectionReader = Data_Access.openDatingDBReader(StrSQL & ProfileID1)

    '        'if the connection is read ie profileID1 exissts create a join on the zipcodes table to determine
    '        'if profile1's zipcode value matches a value in the zipcodes table, if yes get the long and lat
    '        While ConnectionReader.Read
    '            intLatLonProfile1 = GetLatandLon((ConnectionReader("ZIPID")))
    '        End While

    '        'now get the lat long for the 2nd profileID
    '        ' create a reader for the 1s user ID
    '        ConnectionReader = Data_Access.openDatingDBReader(StrSQL & profileID2)
    '        While ConnectionReader.Read
    '            intLatLonProfile2 = GetLatandLon((ConnectionReader("ZIPID")))
    '        End While

    '        If intLatLonProfile1(0) <> Nothing And intLatLonProfile2(0) <> Nothing Then
    '            'now you have the lat and long you can determine the dist btw both points

    '            ' MsgBox(intLatLonProfile1(0) & " " & intLatLonProfile1(1) & " " & intLatLonProfile2(0) & " " & intLatLonProfile2(1))

    '            Return SpatialFunctions.distance(intLatLonProfile1(0), intLatLonProfile1(1), _
    '                            intLatLonProfile2(0), intLatLonProfile2(1), "M")
    '        Else
    '            Return Nothing
    '        End If
    '    Catch
    '        Return Nothing
    '    End Try

    'End Function

    'Public Shared Function DistBTWMembersByLatLon(ByVal ProfileID1 As String, ByVal profileID2 As String, ByVal strConnectionString As String) As Double
    '    Dim StrSQL As String = "Select PostalCode from profilesData Where profileID="
    '    Data_Access.OpenDatingDB()
    '    Dim ConnectionReader As Data.SqlClient.SqlDataReader
    '    Dim intLatLonProfile1(2) As Double
    '    Dim intLatLonProfile2(2) As Double


    '    Try
    '        ' create a reader for the 1s user ID
    '        ConnectionReader = Data_Access.openDatingDBReader(StrSQL & ProfileID1)

    '        'if the connection is read ie profileID1 exissts create a join on the zipcodes table to determine
    '        'if profile1's zipcode value matches a value in the zipcodes table, if yes get the long and lat
    '        While ConnectionReader.Read
    '            intLatLonProfile1 = GetLatandLon((ConnectionReader("ZIPID")))
    '        End While

    '        'now get the lat long for the 2nd profileID
    '        ' create a reader for the 1s user ID
    '        ConnectionReader = Data_Access.openDatingDBReader(StrSQL & profileID2)
    '        While ConnectionReader.Read
    '            intLatLonProfile2 = GetLatandLon((ConnectionReader("ZIPID")))
    '        End While

    '        If intLatLonProfile1(0) <> Nothing And intLatLonProfile2(0) <> Nothing Then
    '            'now you have the lat and long you can determine the dist btw both points

    '            ' MsgBox(intLatLonProfile1(0) & " " & intLatLonProfile1(1) & " " & intLatLonProfile2(0) & " " & intLatLonProfile2(1))

    '            Return SpatialFunctions.GetdistanceBetweenMembers(intLatLonProfile1(0), intLatLonProfile1(1), _
    '                            intLatLonProfile2(0), intLatLonProfile2(1), "M")
    '        Else
    '            Return Nothing
    '        End If
    '    Catch
    '        Return Nothing
    '    End Try

    'End Function

    ' 'Public Shared Function GetLatandLon(ByVal argZIPID) As Double()
    ' Dim intLatLong(2) As Double 'This int array will be returned with values IF and only IF the join works
    ' ' create a join on the zipcodes table and profileID
    ' Dim strSQLZIPCODES As String = "SELECT zipcodes.LATITUDE, zipcodes.LONGITUDE FROM zipcodes INNER JOIN " & _
    '"profiles ON zipcodes.ZIPid = profiles.ZipID WHERE (zipcodes.ZIPid = '" & argZIPID & "')"

    '     Data_Access.OpenDatingDB()
    ' Dim ConnectionReaderLATLONG As Data.SqlClient.SqlDataReader  'this reader gets the lat and longitude for a profileID
    ' 'variables to hold the lattitude and longitutes
    '     ConnectionReaderLATLONG = Data_Access.openDatingDBReader(strSQLZIPCODES) 'this reader will try and
    ' 'open a connection on the sql string above

    ' 'using as a parameter the ZIPID of the profileID1
    '     While ConnectionReaderLATLONG.Read

    '         intLatLong(0) = ConnectionReaderLATLONG("LATITUDE")
    '         intLatLong(1) = ConnectionReaderLATLONG("Longitude")
    '     End While
    '     Data_Access.CloseDatingDB()

    ' 'MsgBox(intLatLong(0))
    '     Return intLatLong
    ' End Function







    ':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    ':::                                                                         :::
    ':::  This routine calculates the distance between two points (given the     :::
    ':::  latitude/longitude of those points). It is being used to calculate     :::
    ':::  the distance between two ZIP Codes or Postal Codes using our           :::
    ':::  ZIPCodeWorld(TM) and PostalCodeWorld(TM) products.                     :::
    ':::                                                                         :::
    ':::  Definitions:                                                           :::
    ':::    South latitudes are negative, east longitudes are positive           :::
    ':::                                                                         :::
    ':::  Passed to function:                                                    :::
    ':::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
    ':::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
    ':::    unit = the unit you desire for results                               :::
    ':::           where: 'M' is statute miles                                   :::
    ':::                  'K' is kilometers (default)                            :::
    ':::                  'N' is nautical miles                                  :::
    ':::                                                                         :::
    ':::  United States ZIP Code/ Canadian Postal Code databases with latitude   :::
    ':::  & longitude are available at http://www.zipcodeworld.com               :::
    ':::                                                                         :::
    ':::  For enquiries, please contact sales@zipcodeworld.com                   :::
    ':::                                                                         :::
    ':::  Official Web site: http://www.zipcodeworld.com                         :::
    ':::                                                                         :::
    ':::  Hexa Software Development Center © All Rights Reserved 2005            :::
    ':::                                                                         :::
    ':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    'gets distance between lat long values
    Public Shared Function GetdistanceBetweenMembers(ByVal lat1 As Double, ByVal lon1 As Double, ByVal lat2 As Double, ByVal lon2 As Double, ByVal unit As Char) As Double

        Dim theta As Double = lon1 - lon2
        Dim dist As Double = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta))
        dist = Math.Acos(dist)
        dist = rad2deg(dist)
        dist = dist * 60 * 1.1515
        If unit = "K" Then
            dist = dist * 1.609344
        ElseIf unit = "N" Then
            dist = dist * 0.8684
        End If
        Return dist
    End Function

    Private Shared Function deg2rad(ByVal deg As Double) As Double
        Return (deg * Math.PI / 180.0)
    End Function

    Private Shared Function rad2deg(ByVal rad As Double) As Double
        Return rad / Math.PI * 180.0
    End Function



    ''these functons are only used in the profile page, we have a new 
    'Public Shared Function Longitude(ByVal strArgument As String) As String

    '    Dim settings As ConnectionStringSettings
    '    Dim strValue As String
    '    Dim strSQL As String = "SELECT Longitude FROM Zipcodes WHERE ZipID=" & strArgument
    '    settings = ConfigurationManager.ConnectionStrings("dating_dbConnectionString") 'this has to come first since it is used below
    '    Dim Connection As New Data.SqlClient.SqlConnection(settings.ConnectionString)
    '    'connection commands for data styff
    '    Dim ConnectionReader As Data.SqlClient.SqlDataReader
    '    Dim ConnectionCommand As New Data.SqlClient.SqlCommand(strSQL, Connection)

    '    strValue = ""

    '    ''msgbox(strArgument.ToString)

    '    Connection.Open()

    '    ConnectionReader = ConnectionCommand.ExecuteReader()

    '    While ConnectionReader.Read()
    '        strValue = ConnectionReader("Longitude")
    '    End While

    '    ConnectionReader.Close()
    '    Connection.Close()

    '    Return strValue



    'End Function

    'Public Shared Function Latitude(ByVal strArgument As String) As String

    '    Dim settings As ConnectionStringSettings
    '    Dim strValue As String
    '    Dim strSQL As String = "SELECT Latitude FROM Zipcodes WHERE ZipID=" & strArgument
    '    settings = ConfigurationManager.ConnectionStrings("dating_dbConnectionString") 'this has to come first since it is used below
    '    Dim Connection As New Data.SqlClient.SqlConnection(settings.ConnectionString)
    '    'connection commands for data styff
    '    Dim ConnectionReader As Data.SqlClient.SqlDataReader
    '    Dim ConnectionCommand As New Data.SqlClient.SqlCommand(strSQL, Connection)

    '    strValue = ""

    '    ''msgbox(strArgument.ToString)

    '    Connection.Open()

    '    ConnectionReader = ConnectionCommand.ExecuteReader()

    '    While ConnectionReader.Read()
    '        strValue = ConnectionReader("Latitude")
    '    End While

    '    ConnectionReader.Close()
    '    Connection.Close()

    '    Return strValue



    'End Function






End Class


