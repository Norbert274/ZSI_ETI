Public Module CursorProfDataTables

    Public Enum Status
        Ok = 0
        [Error] = -1
        Message = 1
    End Enum

    Partial Public Class RezultatStatus
        Public Property Status() As Status
            Get
                Return m_Status
            End Get
            Set(ByVal value As Status)
                m_Status = value
            End Set
        End Property

        Private m_Status As Status
        Public Property Message() As String
            Get
                Return m_Message
            End Get
            Set(ByVal value As String)
                m_Message = value
            End Set
        End Property
        Private m_Message As String
End Class


    Public Function getListaTypeTbl() As DataTable
        Dim listaTypeTable As DataTable = New DataTable
        listaTypeTable.TableName = "tblListaTypeTbl"
        listaTypeTable.Columns.Add([Enum].GetName(GetType(ListaTypeEnum), ListaTypeEnum.ID), Type.GetType("System.Int32"))
        listaTypeTable.Columns.Add([Enum].GetName(GetType(ListaTypeEnum), ListaTypeEnum.WARTOSC_INT), Type.GetType("System.Int32"))
        listaTypeTable.Columns.Add([Enum].GetName(GetType(ListaTypeEnum), ListaTypeEnum.WARTOSC_NVARCHAR), Type.GetType("System.String"))
        listaTypeTable.Columns.Add([Enum].GetName(GetType(ListaTypeEnum), ListaTypeEnum.WARTOSC_BIN), Type.GetType("System.Byte[]"))
        listaTypeTable.Columns.Add([Enum].GetName(GetType(ListaTypeEnum), ListaTypeEnum.WARTOSC_BIT), Type.GetType("System.Boolean"))

        listaTypeTable.Columns([Enum].GetName(GetType(ListaTypeEnum), ListaTypeEnum.ID)).SetOrdinal(0)
        listaTypeTable.Columns([Enum].GetName(GetType(ListaTypeEnum), ListaTypeEnum.WARTOSC_INT)).SetOrdinal(1)
        listaTypeTable.Columns([Enum].GetName(GetType(ListaTypeEnum), ListaTypeEnum.WARTOSC_NVARCHAR)).SetOrdinal(2)
        listaTypeTable.Columns([Enum].GetName(GetType(ListaTypeEnum), ListaTypeEnum.WARTOSC_BIN)).SetOrdinal(3)
        listaTypeTable.Columns([Enum].GetName(GetType(ListaTypeEnum), ListaTypeEnum.WARTOSC_BIT)).SetOrdinal(4)
        Return listaTypeTable
    End Function
End Module


Public Enum ListaTypeEnum
    ID
    WARTOSC_INT
    WARTOSC_NVARCHAR
    WARTOSC_BIN
    WARTOSC_BIT
End Enum



Public Enum EnumSPAdresStrona
    adres_id
    nazwa
    adres
    kod
    miasto
    DOMYSLNY
End Enum

Public Enum EnumZamowienieStrona
    KOSZYK
    NUMER
    ZLECAJACY
    STATUS_ZLECENIA
    TYP
    DATA_ZLOZENIA
    UWAGI
    NUMER_LISTU_PRZEWOZOWEGO
    STATUS_PRZESYŁKI
    DATA_OSTATNIEJ_ZMIANY
    DATA_REALIZACJI
End Enum

Public Enum EnumZamowienieStatusyLista
    [ZAMOWIENIE_STATUS_ID]
    [NAZWA]
    [OPIS]
End Enum