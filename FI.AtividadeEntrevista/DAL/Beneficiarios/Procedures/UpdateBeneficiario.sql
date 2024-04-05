﻿CREATE PROC UpdateBeneficiario
    @NOME          VARCHAR (50) ,
    @CPF           VARCHAR (11),
	@Id           BIGINT
AS
BEGIN
	UPDATE BENEFICIARIOS
	SET 
		NOME = @NOME, 
		CPF = @CPF
	WHERE Id = @Id
END