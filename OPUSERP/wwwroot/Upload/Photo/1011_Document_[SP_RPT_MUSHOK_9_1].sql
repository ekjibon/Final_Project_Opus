USE [db_JSON]
GO
/****** Object:  StoredProcedure [dbo].[SP_RPT_MUSHOK_9_1]    Script Date: 11/19/2019 10:22:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[SP_RPT_MUSHOK_9_1]
(
	@Year INT,
	@Month INT
)
--SP_RPT_MUSHOK_9_1 2019,10
AS
BEGIN	
	
	DECLARE @CFromDate NVARCHAR(8)= CONVERT(NVARCHAR(50), DATEADD(month,@Month-1,DATEADD(year,@Year-1900,0)), 112)
		,@CToDate NVARCHAR(8)= CONVERT(NVARCHAR(50),DATEADD(day,-1,DATEADD(month,@Month,DATEADD(year,@Year-1900,0))), 112)
	--PRINT @CFromDate
	--PRINT @CToDate

	DECLARE @SalesTotalAmount DECIMAL(18,2)
	DECLARE @SalesVATAmount DECIMAL(18,2)
	DECLARE @SalesSDAmount DECIMAL(18,2)=0.00
	SELECT @SalesTotalAmount=SUM(totalAmount) ,@SalesVATAmount=SUM(vatAmount) 
									FROM
									(
									SELECT ISNULL(SUM(totalAmount),0) totalAmount,ISNULL(SUM(VATOnTotal),0) vatAmount FROM SalesInvoiceMasters
									WHERE CONVERT(NVARCHAR(8), invoiceDate, 112) BETWEEN @CFromDate AND @CToDate
									UNION ALL
									SELECT ISNULL(SUM(totalAmount),0) totalAmount,ISNULL(SUM(VATOnTotal),0) vatAmount FROM posInvoiceMasters
									WHERE CONVERT(NVARCHAR(8), invoiceDate, 112) BETWEEN @CFromDate AND @CToDate
									) AA
	
	DECLARE @PurchaseTotalAmount DECIMAL(18,2)								
	DECLARE @PurchaseVATAmount DECIMAL(18,2)
	SELECT @PurchaseTotalAmount=SUM(totalAmount),@PurchaseVATAmount=SUM(vatAmount) 
									FROM(
									SELECT ISNULL(SUM(totalAmount),0) totalAmount,ISNULL(SUM(vatAmount),0) vatAmount FROM PurchaseOrderMasters
									WHERE CONVERT(NVARCHAR(8), poDate, 112) BETWEEN @CFromDate AND @CToDate
									) AA	
									
	DECLARE @DebitNoteTotalAmount DECIMAL(18,2)								
	DECLARE @DebitNoteVATAmount DECIMAL(18,2)
	SELECT @DebitNoteTotalAmount=SUM(totalAmount),@DebitNoteVATAmount=SUM(vatAmount) 
									FROM(
									SELECT ISNULL(SUM(totalAmount),0) totalAmount,ISNULL(SUM(VATOnTotal),0) vatAmount FROM PurchaseReturnMasters
									WHERE CONVERT(NVARCHAR(8), invoiceDate, 112) BETWEEN @CFromDate AND @CToDate
									) AA	
									
	DECLARE @CreditNoteTotalAmount DECIMAL(18,2)								
	DECLARE @CreditNoteVATAmount DECIMAL(18,2)
	SELECT @CreditNoteTotalAmount=SUM(totalAmount) ,@CreditNoteVATAmount=SUM(vatAmount) 
									FROM
									(
									SELECT ISNULL(SUM(totalAmount),0) totalAmount,ISNULL(SUM(VATOnTotal),0) vatAmount FROM SalesReturnInvoiceMasters
									WHERE CONVERT(NVARCHAR(8), invoiceDate, 112) BETWEEN @CFromDate AND @CToDate
									UNION ALL
									SELECT ISNULL(SUM(totalAmount),0) totalAmount,ISNULL(SUM(VATOnTotal),0) vatAmount FROM PosSalesReturnInvoiceMasters
									WHERE CONVERT(NVARCHAR(8), invoiceDate, 112) BETWEEN @CFromDate AND @CToDate
									) AA	
									
	DECLARE @NetTaxPayableTotalVAT DECIMAL(18,2)	
	DECLARE @Note36 DECIMAL(18,2)	
	DECLARE @Note37 DECIMAL(18,2)
	DECLARE @Note38 DECIMAL(18,2)
	DECLARE @Note41 DECIMAL(18,2)=0.00
	DECLARE @Note50 DECIMAL(18,2)	
	DECLARE @Note51 DECIMAL(18,2)

	SELECT @Note50=ISNULL(taxAmount,0),@Note51=ISNULL(SDAmount,0) 
					FROM companyTaxClosingBalances WHERE taxYearId=(SELECT ID FROM TaxYears WHERE LEFT(taxYearName,4)=YEAR(GETDATE())-1)

	SELECT @NetTaxPayableTotalVAT=(@SalesVATAmount + @DebitNoteVATAmount) - (@PurchaseVATAmount + @CreditNoteVATAmount)			
			,@Note36=@NetTaxPayableTotalVAT-@Note50
			,@Note37=(@SalesSDAmount + @DebitNoteVATAmount) - (@CreditNoteVATAmount + @Note41)
			,@Note38=@Note37-@Note51

	DECLARE @Note52 DECIMAL(18,2)
	DECLARE @Note52eCode NVARCHAR(500)
	DECLARE @Note53 DECIMAL(18,2)
	DECLARE @Note53eCode NVARCHAR(500)
	DECLARE @Note54 DECIMAL(18,2)
	DECLARE @Note54eCode NVARCHAR(500)
	DECLARE @Note55 DECIMAL(18,2)
	DECLARE @Note55eCode NVARCHAR(500)
	DECLARE @Note56 DECIMAL(18,2)
	DECLARE @Note56eCode NVARCHAR(500)
	DECLARE @Note57 DECIMAL(18,2)
	DECLARE @Note57eCode NVARCHAR(500)
	DECLARE @Note58 DECIMAL(18,2)
	DECLARE @Note58eCode NVARCHAR(500)
	DECLARE @Note59 DECIMAL(18,2)
	DECLARE @Note59eCode NVARCHAR(500)
	DECLARE @Note60 DECIMAL(18,2)
	DECLARE @Note60eCode NVARCHAR(500)
	DECLARE @Note61 DECIMAL(18,2)
	DECLARE @Note61eCode NVARCHAR(500)

	SELECT TOP 1 @Note52= ISNULL(amount,0),@Note52eCode=economicCode FROM vATPayments P LEFT JOIN vATTypes T ON T.Id=P.vatTypeId 
												WHERE vatTypeid=1 AND taxYearId=(SELECT ID FROM TaxYears WHERE LEFT(taxYearName,4)=@Year)
	SELECT TOP 1 @Note53=ISNULL(amount,0),@Note53eCode=economicCode FROM vATPayments P LEFT JOIN vATTypes T ON T.Id=P.vatTypeId 
												WHERE vatTypeid=2 AND taxYearId=(SELECT ID FROM TaxYears WHERE LEFT(taxYearName,4)=@Year)
	SELECT TOP 1 @Note54= ISNULL(amount,0),@Note54eCode=economicCode FROM vATPayments P LEFT JOIN vATTypes T ON T.Id=P.vatTypeId 
												WHERE vatTypeid=5 AND taxYearId=(SELECT ID FROM TaxYears WHERE LEFT(taxYearName,4)=@Year)
	SELECT TOP 1 @Note55=ISNULL(amount,0),@Note55eCode=economicCode FROM vATPayments P LEFT JOIN vATTypes T ON T.Id=P.vatTypeId 
												WHERE vatTypeid=6 AND taxYearId=(SELECT ID FROM TaxYears WHERE LEFT(taxYearName,4)=@Year)
	SELECT TOP 1 @Note56= ISNULL(amount,0),@Note56eCode=economicCode FROM vATPayments P LEFT JOIN vATTypes T ON T.Id=P.vatTypeId 
												WHERE vatTypeid=7 AND taxYearId=(SELECT ID FROM TaxYears WHERE LEFT(taxYearName,4)=@Year)
	SELECT TOP 1 @Note57=ISNULL(amount,0),@Note57eCode=economicCode FROM vATPayments P LEFT JOIN vATTypes T ON T.Id=P.vatTypeId 
												WHERE vatTypeid=3 AND taxYearId=(SELECT ID FROM TaxYears WHERE LEFT(taxYearName,4)=@Year)
	SELECT TOP 1 @Note58= ISNULL(amount,0),@Note58eCode=economicCode FROM vATPayments P LEFT JOIN vATTypes T ON T.Id=P.vatTypeId 
												WHERE vatTypeid=8 AND taxYearId=(SELECT ID FROM TaxYears WHERE LEFT(taxYearName,4)=@Year)
	SELECT TOP 1 @Note59=ISNULL(amount,0),@Note59eCode=economicCode FROM vATPayments P LEFT JOIN vATTypes T ON T.Id=P.vatTypeId 
												WHERE vatTypeid=9 AND taxYearId=(SELECT ID FROM TaxYears WHERE LEFT(taxYearName,4)=@Year)
	SELECT TOP 1 @Note60= ISNULL(amount,0),@Note60eCode=economicCode FROM vATPayments P LEFT JOIN vATTypes T ON T.Id=P.vatTypeId 
												WHERE vatTypeid=10 AND taxYearId=(SELECT ID FROM TaxYears WHERE LEFT(taxYearName,4)=@Year)
	SELECT TOP 1 @Note61=ISNULL(amount,0),@Note61eCode=economicCode FROM vATPayments P LEFT JOIN vATTypes T ON T.Id=P.vatTypeId 
												WHERE vatTypeid=11 AND taxYearId=(SELECT ID FROM TaxYears WHERE LEFT(taxYearName,4)=@Year)
						

	
	SELECT companyName,C.tinNo,C.tradeLicense,C.binNo,ownerName,phone,email,ownerDasignation designation
	,address=houseVillage+', Sector-'+blockSector+', '+thanaName+', '+districtName+', '+divisionName
	,BT.name businessType,equityType financeNature, '' taxExpiryDate, '' isPrevioustaxDone,'' taxSubmitDate
	,@SalesTotalAmount salesTotalAmount,@SalesVATAmount salesVATAmount,@SalesSDAmount salesSDAmount,@PurchaseTotalAmount purchaseTotalAmount
	,@PurchaseVATAmount purchaseVATAmount,@DebitNoteTotalAmount debitNoteTotalAmount, @DebitNoteVATAmount debitNoteVATAmount
	,@CreditNoteTotalAmount creditNoteTotalAmount,@CreditNoteVATAmount creditNoteVATAmount, @NetTaxPayableTotalVAT netTaxPayableTotalVAT
	,@Note36 note36, @Note37 note37, @Note38 note38, @Note41 note41, @Note50 note50, @Note51 note51
	,@Note52 note52, @Note53 note53,@Note54 note54 ,@Note55 note55, @Note56 note56,@Note57 note57,@Note58 note58,@Note59 note59,@Note60 note60,@Note61 note61
	,@Note52eCode note52eCode, @Note53eCode note53eCode, @Note54eCode note54eCode, @Note55eCode note55eCode, @Note56eCode note56eCode
	,@Note57eCode note57eCode,@Note58eCode note58eCode, @Note59eCode note59eCode, @Note60eCode note60eCode, @Note61eCode note61eCode
	FROM Companies C 
	LEFT JOIN Addresses A ON A.companyId=C.Id
	LEFT JOIN Divisions DIV ON DIV.Id=A.divisionId
	LEFT JOIN Districts DS ON DS.Id=A.districtId
	LEFT JOIN Thanas TH ON TH.Id=A.thanaId
	LEFT JOIN BusinessTypes BT ON BT.Id=C.businessTypeId
	
	
	
END