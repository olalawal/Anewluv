' herein are fictitious.  No association with any real company,
' organization, product, domain name, email address, logo, person,
' places, or events is intended or should be inferred.
'===================================================================================
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection
Imports Microsoft.Practices.Composite.Presentation.Commands
Imports Microsoft.Practices.Composite.Events



Namespace PrismQuickStart1.common.viewmodel
    ''' <summary>
    ''' Base class for view models.
    ''' </summary>
    ''' <remarks>
    ''' This class provides basic support for implementing the <see cref="INotifyPropertyChanged"/> and 
    ''' <see cref="INotifyDataErrorInfo"/> interfaces.
    ''' </remarks>
    Public Class ViewModelBase
        Implements INotifyPropertyChanged
        Implements INotifyDataErrorInfo
        Private ReadOnly errors As New Dictionary(Of String, List(Of String))()






        ''' <summary>
        ''' Raised when a property on this object has a new value.
        ''' </summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged



        ''' <summary>
        ''' Occurs when the validation errors have changed for a property or for the entire object.
        ''' </summary>
        Public Event ErrorsChanged As EventHandler(Of DataErrorsChangedEventArgs) Implements INotifyDataErrorInfo.ErrorsChanged

        Public Overridable ReadOnly Property HasErrors() As Boolean Implements INotifyDataErrorInfo.HasErrors
            Get
                Return Me.errors.Count > 0
            End Get
        End Property

        Public Overridable Function GetErrors(ByVal propertyName As String) As IEnumerable Implements INotifyDataErrorInfo.GetErrors
            Dim [error] As List(Of String)
            If Me.errors.TryGetValue(If(propertyName, String.Empty), [error]) Then
                Return [error]
            End If

            Return Nothing
        End Function

        '  Protected Sub SetError(ByVal propertyName As String, ByVal [error] As String)
        '		Me.SetErrors(propertyName, New List(Of String) With { [error] })
        '  End Sub

        '   Protected Sub SetError(Of T)(ByVal propertyExpresssion As Expression(Of Func(Of T)), ByVal [error] As String)
        'Dim propertyName = ExtractPropertyName(propertyExpresssion)
        '       Me.SetError(propertyName, [error])
        '   End Sub

        Protected Sub ClearErrors(ByVal propertyName As String)
            Me.SetErrors(propertyName, New List(Of String)())
        End Sub

        Protected Sub ClearErrors(Of T)(ByVal propertyExpresssion As Expression(Of Func(Of T)))
            Dim propertyName = ExtractPropertyName(propertyExpresssion)
            Me.ClearErrors(propertyName)
        End Sub

        Protected Sub SetErrors(ByVal propertyName As String, ByVal propertyErrors As List(Of String))
            Dim propertyNameKey = If(propertyName, String.Empty)

            Dim currentPropertyErrors As List(Of String)
            If Me.errors.TryGetValue(propertyNameKey, currentPropertyErrors) Then
                Dim equals = currentPropertyErrors.Zip(propertyErrors, Function(current, newError) current = newError)
                If propertyErrors.Count <> currentPropertyErrors.Count OrElse Not equals.All(Function(b) b) Then
                    If propertyErrors.Count <> 0 AndAlso propertyErrors.Any(Function([error]) Not String.IsNullOrEmpty([error])) Then
                        Me.errors(propertyNameKey) = propertyErrors
                    Else
                        Me.errors.Remove(propertyNameKey)
                    End If

                    Me.RaiseErrorsChanged(propertyNameKey)
                End If
            Else
                If propertyErrors.Count <> 0 AndAlso propertyErrors.Any(Function([error]) Not String.IsNullOrEmpty([error])) Then
                    Me.errors(propertyNameKey) = propertyErrors
                    Me.RaiseErrorsChanged(propertyNameKey)
                End If
            End If
        End Sub

        Protected Sub SetErrors(Of T)(ByVal propertyExpresssion As Expression(Of Func(Of T)), ByVal propertyErrors As List(Of String))
            Dim propertyName = ExtractPropertyName(propertyExpresssion)
            Me.SetErrors(propertyName, propertyErrors)
        End Sub

        ''' <summary>
        ''' Raises this object's ErrorsChangedChanged event.
        ''' </summary>
        ''' <param name="propertyName">The property that has new errors.</param>
        Protected Overridable Sub RaiseErrorsChanged(ByVal propertyName As String)
            RaiseEvent ErrorsChanged(Me, New DataErrorsChangedEventArgs(propertyName))
            'Dim handler As EventHandler(Of DataErrorsChangedEventArgs) = Me.ErrorsChanged
            ' RaiseEvent handler(Me, New DataErrorsChangedEventArgs(propertyName))
        End Sub

        ''' <summary>
        ''' Raises this object's ErrorsChangedChanged event.
        ''' </summary>
        ''' <param name="propertyExpresssion">A Lambda expression representing the property that has new errors.</param>
        Protected Overridable Sub RaiseErrorsChanged(Of T)(ByVal propertyExpresssion As Expression(Of Func(Of T)))
            Dim propertyName = ExtractPropertyName(propertyExpresssion)
            Me.RaiseErrorsChanged(propertyName)
        End Sub

        ''' <summary>
        ''' Raises this object's PropertyChanged event.
        ''' </summary>
        ''' <param name="propertyName">The property that has a new value.</param>
        Protected Overridable Sub RaisePropertyChanged(ByVal propertyName As String)

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))


            'Dim handler As PropertyChangedEventHandler = Me.PropertyChanged
            ' RaiseEvent handler(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        ''' <summary>
        ''' Raises this object's PropertyChanged event for each of the properties.
        ''' </summary>
        ''' <param name="propertyNames">The properties that have a new value.</param>
        Protected Sub RaisePropertyChanged(ByVal ParamArray propertyNames As String())
            For Each name As String In propertyNames
                Me.RaisePropertyChanged(name)
            Next
        End Sub

        ''' <summary>
        ''' Raises this object's PropertyChanged event.
        ''' </summary>
        ''' <param name="propertyExpresssion">A Lambda expression representing the property that has a new value.</param>
        Protected Sub RaisePropertyChanged(Of T)(ByVal propertyExpresssion As Expression(Of Func(Of T)))
            Dim propertyName = ExtractPropertyName(propertyExpresssion)
            Me.RaisePropertyChanged(propertyName)
        End Sub

        Private Function ExtractPropertyName(Of T)(ByVal propertyExpresssion As Expression(Of Func(Of T))) As String
            If propertyExpresssion Is Nothing Then
                Throw New ArgumentNullException("propertyExpression")
            End If

            Dim memberExpression = TryCast(propertyExpresssion.Body, MemberExpression)
            If memberExpression Is Nothing Then
                Throw New ArgumentException("The expression is not a member access expression.", "propertyExpression")
            End If

            Dim [property] = TryCast(memberExpression.Member, PropertyInfo)
            If [property] Is Nothing Then
                Throw New ArgumentException("The member access expression does not access a property.", "propertyExpression")
            End If

            If Not [property].DeclaringType.IsAssignableFrom(Me.[GetType]()) Then
                Throw New ArgumentException("The referenced property belongs to a different type.", "propertyExpression")
            End If

            Dim getMethod = [property].GetGetMethod(True)
            If getMethod Is Nothing Then
                ' this shouldn't happen - the expression would reject the property before reaching this far
                Throw New ArgumentException("The referenced property does not have a get method.", "propertyExpression")
            End If

            If getMethod.IsStatic Then
                Throw New ArgumentException("The referenced property is a static property.", "propertyExpression")
            End If

            Return memberExpression.Member.Name
        End Function
    End Class
End Namespace

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik, @toddanglin
'Facebook: facebook.com/telerik
'=======================================================
