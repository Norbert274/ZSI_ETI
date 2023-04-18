Imports System.ComponentModel

''' <summary>
''' Custom ToolTip Component that is based on a normal tooltip component but tracks tips
''' for disabled controls
''' Note that the because this is a separate extender, all the controls on a form
''' can have an "Enabled" tip (as normal) AND a "disabled" tip.
'''
''' By Darin Higgins 2008
''' Based on a code example by Linda Lui (MSFT)
'''
''' </summary>
''' <remarks></remarks>
''' <editHistory></editHistory>
Public Class DisabledToolTip
    Inherits ToolTip
    Implements ISupportInitialize

    '---- hold onto a reference to the host form
    '     to monitor the mousemove
    Private WithEvents rParentForm As System.Windows.Forms.Form

    Private _rbActive As Boolean = True
    ''' <summary>
    ''' Active for the Disabled ToolTip has a slightly different meaning
    ''' than "Active" for a regular tooltip
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    <DefaultValue(True)> _
    Public Shadows Property Active() As Boolean
        Get
            Return _rbActive
        End Get
        Set(ByVal value As Boolean)
            If _rbActive <> value Then
                _rbActive = value
            End If
        End Set
    End Property

    '---- hold on to a control temporarily while we wait for things to
    '     settle
    Private rControl As Control

    ''' <summary>
    ''' Shadow the settooltip function so we can intercept and save a control
    ''' reference. NOTE: the form MIGHT not be setup yet, so the control
    ''' might not know what it's parent is yet, so we cache the the first control
    ''' we get, and use it later, if necessary
    ''' </summary>
    ''' <param name="control"></param>
    ''' <param name="caption"></param>
    ''' <remarks></remarks>
    Public Shadows Sub SetToolTip(ByVal control As Control, ByVal caption As String)
        MyBase.SetToolTip(control, caption)

        '---- if we don't have the parent form yet...
        If rParentForm Is Nothing Then
            '---- attempt to get it from the control
            rParentForm = control.FindForm
            '---- if that doesn't work
            If rParentForm Is Nothing Then
                '---- cache the control for use a little later
                rControl = control
            End If
        End If
    End Sub

    Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        '---- Our base tooltip is disabled by default
        '     because we don't want to show disabled tooltips when
        '     a control is NOT disabled!
        MyBase.Active = False
    End Sub

    ''' <summary>
    ''' Supports end of initialization phase tasks for this control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EndInit() Implements ISupportInitialize.EndInit
        '---- if we weren't able to retrieve the form from the control
        '     before, we should be able to now
        If rControl IsNot Nothing Then
            rParentForm = rControl.FindForm
        End If
    End Sub

    Public Sub New(ByVal IContainer As IContainer)
        MyBase.New(IContainer)
    End Sub

    ''' <summary>
    ''' Monitor the MouseMove event on the host form
    ''' If we see it move over a disabled control
    ''' Check for a tooltip and show it
    ''' If the cursor moved off the control we're displaying
    ''' a tip for, hide the tip.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rParentForm_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rParentForm.MouseMove
        Static ctrlWithToolTip As Control = Nothing

        Dim ctrl = rParentForm.GetChildAtPoint(e.Location)
        
        If ctrl IsNot Nothing Then
            If Not ctrl.Enabled Then
                If ctrlWithToolTip IsNot Nothing Then
                    If ctrl IsNot ctrlWithToolTip Then
                        '---- if we're not over the control we last showed
                        '     a tip for, close down the tip
                        Me.Hide(ctrlWithToolTip)
                        ctrlWithToolTip = Nothing
                        MyBase.Active = False
                    End If
                End If
                If ctrlWithToolTip Is Nothing Then
                    Dim tipstring = Me.GetToolTip(ctrl)
                    If Len(tipstring) And Me.Active Then
                        '---- only enable the base tooltip if we're going to show one
                        MyBase.Active = True
                        Me.Show(tipstring, ctrl, ctrl.Width / 2, ctrl.Height / 2)
                        ctrlWithToolTip = ctrl
                    End If
                End If

            ElseIf ctrlWithToolTip IsNot Nothing Then
                '---- if we're over an enabled control
                '     the tip doesn't apply anymore
                Me.Hide(ctrlWithToolTip)
                ctrlWithToolTip = Nothing
                MyBase.Active = False
            End If
        ElseIf ctrlWithToolTip IsNot Nothing Then
            '---- if we're not over a control at all, but we've got a
            '     tip showing, hide it, it's no longer applicable
            Me.Hide(ctrlWithToolTip)
            ctrlWithToolTip = Nothing
            MyBase.Active = False
        End If
    End Sub
End Class
