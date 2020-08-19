Public Class FileEventArgs
    Inherits EventArgs
    Public data As ExtraData

    Public Sub New(v As ExtraData)
        data = v
    End Sub
End Class

Public Class ListViewEx
    Inherits ListView

    Private imgList As New ImageList

    Public Const BUTTONWIDTH = 105
    Public Const BUTTONPADING = 10
    Public Event Button1Click As EventHandler(Of FileEventArgs)
    Public Event Button2Click As EventHandler(Of FileEventArgs)


    Public Sub New()
        Me.OwnerDraw = True

        ' The control flickers if it's not double-buffered
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        ' Configure the columns and such
        Me.View = View.Details
        Me.BackColor = Color.White
        Me.FullRowSelect = True

        ' Set the imagelist to change the height of the Listview items.
        imgList.ColorDepth = ColorDepth.Depth32Bit
        imgList.ImageSize = New Size(64, 64)
        Me.SmallImageList = imgList
    End Sub

    Private Sub Me_DrawColumnHeader(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawListViewColumnHeaderEventArgs) Handles Me.DrawColumnHeader
        e.DrawDefault = True
    End Sub

    Private Sub Me_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs) Handles Me.DrawItem
        e.DrawDefault = False
    End Sub

    Private Sub Me_DrawSubItem(ByVal sender As Object, ByVal e As DrawListViewSubItemEventArgs) Handles Me.DrawSubItem
        If (e.ItemState And ListViewItemStates.Selected) <> 0 Then
            'e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds) 'item is highlighted
        End If

        ' Draw the image and text
        If e.ColumnIndex = 0 Then
            Dim lvw As ExtraData = DirectCast(e.Item.Tag, ExtraData)
            Dim f1 As New Font("Arial", 12, FontStyle.Bold)
            Dim f2 As New Font("Arial", 8, FontStyle.Bold)
            Dim f3 As New Font("Arial", 8, FontStyle.Italic)
            Dim Rectf1 As RectangleF = New Rectangle(e.Bounds.X + 5 + 40, e.Bounds.Y + 1, e.Bounds.Width - 40, f1.Height)
            Dim Rectf2 As RectangleF = New Rectangle(e.Bounds.X + 5 + 40, e.Bounds.Y + 23, e.Bounds.Width - 40, f2.Height)
            Dim Rectf3 As RectangleF = New Rectangle(e.Bounds.X + 5 + 40, e.Bounds.Y + 40, e.Bounds.Width - 40, f3.Height)
            Dim sf As New StringFormat
            sf.Alignment = StringAlignment.Near
            sf.Trimming = StringTrimming.EllipsisCharacter
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            e.Graphics.DrawImage(lvw.MainImage, e.Bounds.X + 4 + 5, e.Bounds.Y + 4, 32, 32)
            e.Graphics.DrawString(lvw.HeaderText, f1, New SolidBrush(lvw.HeaderColor), Rectf1, sf)
            e.Graphics.DrawString(lvw.MinorText, f2, Brushes.Blue, Rectf2, sf)
            e.Graphics.DrawString(lvw.DescText, f3, Brushes.Black, Rectf3, sf)
            sf.Alignment = StringAlignment.Center
            If lvw.ButtonText1 <> "" Then
                Using br = New SolidBrush(Color.FromArgb(25, 152, 254))
                    e.Graphics.FillRectangle(br, New Rectangle(e.Bounds.X + e.Bounds.Width - BUTTONWIDTH - BUTTONPADING, e.Bounds.Y + 5, BUTTONWIDTH, 30)) 'item is highlighted
                    e.Graphics.DrawString(lvw.ButtonText1, f1, Brushes.White, New Rectangle(e.Bounds.X + e.Bounds.Width - BUTTONWIDTH - BUTTONPADING + 2, e.Bounds.Y + 10, BUTTONWIDTH, 30), sf)
                End Using
            End If
            If lvw.ButtonText2 <> "" Then
                e.Graphics.FillRectangle(SystemBrushes.ControlLight, New Rectangle(e.Bounds.X + e.Bounds.Width - 2 * (BUTTONWIDTH + BUTTONPADING), e.Bounds.Y + 5, BUTTONWIDTH, 30)) 'item is highlighted
                e.Graphics.DrawString(lvw.ButtonText2, f1, Brushes.Black, New Rectangle(e.Bounds.X + e.Bounds.Width - 2 * (BUTTONWIDTH + BUTTONPADING) + 2, e.Bounds.Y + 10, BUTTONWIDTH, 30), sf)
            End If
        Else
            e.DrawDefault = True

        End If

    End Sub

    Private Function isInRect(x As Integer, y As Integer, rec As Rectangle)
        If x > rec.X And x < rec.X + rec.Width And y > rec.Y And y < rec.Y + rec.Height Then
            Return True
        End If
        Return False
    End Function

    Private Sub ListViewEx_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If Me.SelectedItems.Count > 0 Then
            Dim lstItem As ListViewItem = Me.SelectedItems.Item(0)
            Dim lvw As ExtraData = DirectCast(lstItem.Tag, ExtraData)
            If (lvw.ButtonText1 <> "" And isInRect(e.X, e.Y, New Rectangle(lstItem.Bounds.X + lstItem.Bounds.Width - BUTTONWIDTH - BUTTONPADING, lstItem.Bounds.Y + 5, BUTTONWIDTH, 30))) Then
                RaiseEvent Button1Click(Me, New FileEventArgs(lvw))
            ElseIf (lvw.ButtonText2 <> "" And isInRect(e.X, e.Y, New Rectangle(lstItem.Bounds.X + lstItem.Bounds.Width - 2 * (BUTTONWIDTH + BUTTONPADING), lstItem.Bounds.Y + 5, BUTTONWIDTH, 30))) Then
                RaiseEvent Button2Click(Me, New FileEventArgs(lvw))
            End If
        End If
    End Sub

    Private Sub ListViewEx_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        Dim lstItem As ListViewItem = Me.GetItemAt(e.X, e.Y)
        If Not lstItem Is Nothing Then
            Dim lvw As ExtraData = DirectCast(lstItem.Tag, ExtraData)
            If (lvw.ButtonText1 <> "" And isInRect(e.X, e.Y, New Rectangle(lstItem.Bounds.X + lstItem.Bounds.Width - BUTTONWIDTH - BUTTONPADING, lstItem.Bounds.Y + 5, BUTTONWIDTH, 30))) Then
                Me.Cursor = Cursors.Hand
            ElseIf (lvw.ButtonText2 <> "" And isInRect(e.X, e.Y, New Rectangle(lstItem.Bounds.X + lstItem.Bounds.Width - 2 * (BUTTONWIDTH + BUTTONPADING), lstItem.Bounds.Y + 5, BUTTONWIDTH, 30))) Then
                Me.Cursor = Cursors.Hand
            Else
                Me.Cursor = Me.DefaultCursor
            End If
        End If
    End Sub
End Class