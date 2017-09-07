USE [Teste]
GO

------------------------------------------------------------------------------------------------------------------------------------

ALTER TABLE dbo.NotaFiscalItem ADD
	BaseIPI decimal(18, 5) NULL,
	AliquotaIPI decimal(18, 5) NULL,
	ValorIPI decimal(18, 5) NULL,
	Valor decimal(18, 5) NULL
	Desconto decimal(18, 5) NULL
GO

------------------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[P_NOTA_FISCAL_ITEM]
(
	@pId int,
    @pIdNotaFiscal int,
    @pCfop varchar(5),
    @pTipoIcms varchar(20),
    @pBaseIcms decimal(18,5),
    @pAliquotaIcms decimal(18,5),
    @pValorIcms decimal(18,5),
    @pNomeProduto varchar(50),
    @pCodigoProduto varchar(20),
	@BaseIPI decimal(18, 5),
	@AliquotaIPI decimal(18, 5),
	@ValorIPI decimal(18, 5),
	@Valor decimal(18,5),
	@Desconto decimal(18,5)
)
AS
BEGIN
	IF (@pId = 0)
	BEGIN 		
		INSERT INTO [dbo].[NotaFiscalItem]
           ([IdNotaFiscal]
           ,[Cfop]
           ,[TipoIcms]
           ,[BaseIcms]
           ,[AliquotaIcms]
           ,[ValorIcms]
           ,[NomeProduto]
           ,[CodigoProduto]
		   ,[BaseIPI]
		   ,[AliquotaIPI]
		   ,[ValorIPI]
		   ,[Valor]
		   ,[Desconto])
		VALUES
           (@pIdNotaFiscal,
			@pCfop,
			@pTipoIcms,
			@pBaseIcms,
			@pAliquotaIcms,
			@pValorIcms,
			@pNomeProduto,
			@pCodigoProduto,
			@BaseIPI,
			@AliquotaIPI,
			@ValorIPI,
			@Valor,
			@Desconto)

		SET @pId = @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE [dbo].[NotaFiscalItem]
		SET [IdNotaFiscal] = @pIdNotaFiscal
			,[Cfop] = @pCfop
			,[TipoIcms] = @pTipoIcms
			,[BaseIcms] = @pBaseIcms
			,[AliquotaIcms] = @pAliquotaIcms
			,[ValorIcms] = @pValorIcms
			,[NomeProduto] = @pNomeProduto
			,[CodigoProduto] = @pCodigoProduto
			,[BaseIPI] = @BaseIPI
			,[AliquotaIPI] = @AliquotaIPI
			,[ValorIPI] = @ValorIPI
			,[Valor] = @Valor
			,[Desconto] = @Desconto
		 WHERE Id = @pId
	END	    
END
GO

------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[P_REL_NOTA_FISCAL]
AS
BEGIN
	SELECT
		Cfop AS CFOP,
		SUM(ISNULL(BaseIcms, 0)) AS ValorBaseICMS ,
		SUM(ISNULL(ValorIcms, 0)) AS ValorICMS,
		SUM(ISNULL(BaseIPI, 0)) AS ValorBaseIPI,
		SUM(ISNULL(ValorIPI, 0)) AS ValorIPI
	FROM
		NotaFiscalItem (NOLOCK)
	GROUP BY
		Cfop
END
GO