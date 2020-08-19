Public Class ReturnModel
    Public Property return_code() As Integer
        Get
            Return m_return_code
        End Get
        Set
            m_return_code = Value
        End Set
    End Property
    Private m_return_code As Integer

    Public Property message() As String
        Get
            Return m_message
        End Get
        Set
            m_message = Value
        End Set
    End Property
    Private m_message As String
End Class
