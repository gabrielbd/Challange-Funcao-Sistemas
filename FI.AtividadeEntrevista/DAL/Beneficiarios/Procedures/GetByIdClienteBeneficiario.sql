﻿CREATE PROC GetByIdClienteBeneficiario
	@IDCLIENTE BIGINT
AS
BEGIN
	IF(ISNULL(@IDCLIENTE,0) = 0)
		SELECT NOME, CPF,IDCLIENTE, ID FROM BENEFICIARIOS WITH(NOLOCK)
	ELSE
		SELECT NOME,CPF,IDCLIENTE, ID FROM BENEFICIARIOS WITH(NOLOCK) WHERE IDCLIENTE = @IDCLIENTE
END