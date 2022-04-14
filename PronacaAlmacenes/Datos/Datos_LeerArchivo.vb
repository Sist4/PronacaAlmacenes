Public Class Datos_LeerArchivo
    Public Sub leer_archivo()
        Try
            Dim currentRow As String()
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("Ruta del archivo")
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(";")        'Separador del archivo de texto, en este caso punto y coma
                While Not MyReader.EndOfData ' Mientras no sea el final del archivo
                    currentRow = MyReader.ReadFields()  'esto llenara el string() con las posiciones que contenga el .txt
                    'ej txt:   Lucas;Nahuel;18;Chile
                    'currentRow(0) = Lucas
                    'currentRow(1) = Nahuel
                    'CurrentRow(2) = 18
                    'CurrentRow(3) = Chile   
                End While
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Public Sub Escribir_Archivo()
        Try
            Dim obj As New Object
            Dim archivo As New Object
            Dim ruta As String = "Ruta y nombre del archivo.txt" 'Ej: Documentos\archivo1.txt

            obj = CreateObject("Scripting.FileSystemObject")
            archivo = obj.CreateTextFile(ruta, True)
            'Luego agregas las lineas que quieras al archivo
            archivo.WriteLine("Linea 1")
            archivo.WriteLine("Linea 2")
            archivo.WriteLine("etc..")
            archivo.close() 'Al final cierras el archivo para que se libere de la memoria

        Catch ex As Exception

        End Try

    End Sub

End Class
