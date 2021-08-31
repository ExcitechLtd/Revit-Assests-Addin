<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectElementParamaters
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.chkListParamaters = New DevExpress.XtraEditors.CheckedListBoxControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.RadioGroup1 = New DevExpress.XtraEditors.RadioGroup()
        CType(Me.chkListParamaters, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioGroup1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkListParamaters
        '
        Me.chkListParamaters.Location = New System.Drawing.Point(7, 85)
        Me.chkListParamaters.Name = "chkListParamaters"
        Me.chkListParamaters.Size = New System.Drawing.Size(349, 353)
        Me.chkListParamaters.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 66)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(230, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Select model paramaters to show in the preview"
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(147, 451)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 2
        Me.btnOk.Text = "OK"
        '
        'RadioGroup1
        '
        Me.RadioGroup1.EditValue = "bimatt"
        Me.RadioGroup1.Location = New System.Drawing.Point(7, 12)
        Me.RadioGroup1.Name = "RadioGroup1"
        Me.RadioGroup1.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem("bimatt", "Show BIM Attributes"), New DevExpress.XtraEditors.Controls.RadioGroupItem("modelatt", "Show Model Attributes")})
        Me.RadioGroup1.Size = New System.Drawing.Size(349, 37)
        Me.RadioGroup1.TabIndex = 14
        '
        'frmSelectElementParamaters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(368, 480)
        Me.Controls.Add(Me.RadioGroup1)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.chkListParamaters)
        Me.Name = "frmSelectElementParamaters"
        Me.Text = "frmSelectElementParamaters"
        CType(Me.chkListParamaters, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioGroup1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chkListParamaters As DevExpress.XtraEditors.CheckedListBoxControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RadioGroup1 As DevExpress.XtraEditors.RadioGroup
End Class
