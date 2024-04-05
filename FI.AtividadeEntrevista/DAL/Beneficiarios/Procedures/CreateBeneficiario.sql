﻿CREATE PROC CreateBeneficiario
    @NOME          VARCHAR(50),
    @CPF           VARCHAR(14),
    @IDCLIENTE     BIGINT
AS
BEGIN
    INSERT INTO BENEFICIARIOS(NOME, CPF, IDCLIENTE) 
    VALUES (@NOME, @CPF, @IDCLIENTE)

    SELECT SCOPE_IDENTITY()
END