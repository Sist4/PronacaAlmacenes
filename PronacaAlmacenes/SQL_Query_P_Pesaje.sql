USE PRONACA_TIENDAS
DECLARE @ID_Indicador varchar(2) = '16'
DECLARE @secuencial int = 1234
DECLARE @Cod_Operador varchar(10)= '1'
DECLARE @Cod_Producto nvarchar(10)= '9026'
DECLARE @Orden_Produccion nvarchar(20)= 'TRA-1066572'
DECLARE @Cod_Tara nvarchar(10)= '2'
DECLARE @Peso nvarchar(10)= '0'
DECLARE @Unidades nvarchar(6)= '0'
DECLARE @Estado nvarchar(2)= 'P'
DECLARE @Pes_Gavetas nvarchar(10)= '2.86'
DECLARE @Lote nvarchar(50)= ''

EXEC P_Pesaje @ID_indicador,@secuencial,@Cod_Operador,@Cod_Producto,@Orden_Produccion,@Cod_Tara,@Peso,@Unidades,@Estado,@Pes_Gavetas,@Lote