Imports System.Drawing.Drawing2D

Module CursorProfHelpers
    Public Function ResizeImage(ByVal image As Image, _
ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        If preserveAspectRatio Then
            Dim originalWidth As Integer = image.Width
            Dim originalHeight As Integer = image.Height
            Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
            Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
            Dim percent As Single = If(percentHeight < percentWidth,
        percentHeight, percentWidth)
            newWidth = CInt(originalWidth * percent)
            newHeight = CInt(originalHeight * percent)
        Else
            newWidth = size.Width
            newHeight = size.Height
        End If
        Dim newImage As Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function

    Public Function EnumName(Of T)(value As T) As String
        Return [Enum].GetName(GetType(T), value)
    End Function

    Public Function NZ(ByVal S As Object, ByVal Def As Object) As Object
        If IsDBNull(S) Then
            Return Def
        Else
            If Not (S Is Nothing) Then
                Return (S)
            Else
                Return Def
            End If
        End If
    End Function
End Module
