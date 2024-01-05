
GO
USE LTTQ
GO

CREATE TABLE PRODUCT(
	ProductCode int identity(1,1) primary key,
	ProductName nvarchar(100) not null,
	ProductPrice float not null
);

INSERT INTO PRODUCT(ProductName, ProductPrice) 
		VALUES ('Tiger', 16000),
				(N'Sài Gòn', 12000),
				(N'Tiger Bạc', 20000),
				(N'Strong bow', 18000);

SELECT * FROM PRODUCT