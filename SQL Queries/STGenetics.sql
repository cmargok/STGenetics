------------------------------------------------------------------------------
-- Script to create an Animal table
CREATE TABLE Animal (
  AnimalId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  [Name] VARCHAR(50) NOT NULL,
  Breed VARCHAR(50) NOT NULL,
  BirthDate DATE NOT NULL,
  Sex VARCHAR(6) NOT NULL,
  Price DECIMAL(10, 2) NOT NULL,
  [Status] BIT  NOT NULL
);

------------------------------------------------------------------------------
-- Script to Insert an Animal
INSERT INTO Animal ([Name], Breed, BirthDate, Sex, Price, [Status])
VALUES ('Lola', 'Limousin', '2019-05-15', 'Male', 500.00, 1);

------------------------------------------------------------------------------
--Script to Update an Animal 
UPDATE Animal
SET Price = 600.00,
Status = 0
WHERE AnimalId = 1;


------------------------------------------------------------------------------
--Script for a Store Procedure who update an animal  
CREATE PROCEDURE UpdateAnimal
    @AnimalId INT,
    @Name VARCHAR(50),
    @Breed VARCHAR(50),
    @BirthDate DATE,
    @Sex VARCHAR(10),
    @Price DECIMAL(10, 2),
    @Status BIT
AS
BEGIN
    UPDATE Animal
    SET [Name] = @Name,
        Breed = @Breed,
        BirthDate = @BirthDate,
        Sex = @Sex,
        Price = @Price,
        [Status] = @Status
    WHERE AnimalId = @AnimalId;
END;

--How to execute the SP 

EXEC dbo.UpdateAnimal
    @AnimalId = 1,
    @Name = 'Maximus',
    @Breed = 'German Shepherd',
    @BirthDate = '2019-08-10',
    @Sex = 'Male',
    @Price = 600.00,
    @Status = 1;

------------------------------------------------------------------------------
--Script to delete an Animal
DELETE FROM Animal
WHERE AnimalId = 1;


------------------------------------------------------------------------------
-- Script to insert 5000 animals into  Animal table
DECLARE @Names TABLE (
 Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
 [Name] VARCHAR(50)
 );

INSERT INTO @Names ([Name])
VALUES ('Max'), ('Bella'), ('Charlie'), ('Lucy'), ('Cooper'), ('Luna'), ('Buddy'), ('Daisy'), ('Rocky'), ('Sadie'), ('Oliver'), ('Molly'), 
('Tucker'), ('Bailey'), ('Duke'), ('Maggie'), ('Bear'), ('Sophie'), ('Jack'), ('Chloe'),  ('Teddy'), ('Lily'), ('Winston'), ('Zoe'),
('Riley'), ('Roxy'), ('Zeus'), ('Gracie'), ('Oscar'), ('Coco'), ('Harley'), ('Ruby'), ('Milo'), ('Rosie'), ('Leo'), ('Penny'), ('Sam'), 
('Zoey'), ('Rusty'), ('Stella'), ('Simba'), ('Abby'),   ('Gus'), ('Sasha'), ('Finn'), ('Cali'), ('Bentley'), ('Nala'), ('Murphy'), 
('Hazel'), ('Koda');

DECLARE @Breeds TABLE (
Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Breed VARCHAR(50)
);

INSERT INTO @Breeds (Breed)
VALUES ('Angus'), ('Hereford'), ('Holstein'), ('Charolais'), ('Limousin'), ('Brahman'), ('Simmental'), ('Highland'), ('Jersey'), ('Guernsey');

DECLARE @Counter INT = 1;
DECLARE @RandomName VARCHAR(50);
DECLARE @RandomBreed VARCHAR(50);
DECLARE @RandomPrice DECIMAL(10, 2);
WHILE @Counter <= 5000

BEGIN 
    SELECT @RandomName = [Name] FROM @Names Where Id = CAST((RAND() * 50) + 1 AS INT);   
    SELECT @RandomBreed = Breed FROM @Breeds WHERE Id = CAST((RAND() * 10) + 1 AS INT);  
    SET @RandomPrice = CAST((RAND() * 4900) + 100 AS DECIMAL(10, 2));

   INSERT INTO Animal ([Name], Breed, BirthDate, Sex, Price, [Status])
    VALUES (
	@RandomName, 
	@RandomBreed, 
	DATEADD(DAY, CAST((RAND() * 1825) + 1 AS INT), '2018-01-01'), 
	CASE CAST(RAND() * 2 AS INT) 
		WHEN 0 THEN 'Male' 
		ELSE 'Female' 
	END, 
	@RandomPrice, 
	1
	);
    SET @Counter = @Counter + 1;
END;

------------------------------------------------------------------------------
-- Create a script to list animals older than 2 years and female, sorted by name
DECLARE @Years INT = 2;
DECLARE @CurrentDate DATE = GETDATE();
SELECT *
  FROM Animal WHERE DATEDIFF(DAY, BirthDate, @CurrentDate) > (365 * @Years)
  AND Sex = 'Female'
  ORDER BY [Name];


------------------------------------------------------------------------------
-- create a script to list the quantity of animals by sex
SELECT 
	Sex AS Sex, 
	COUNT(*) AS Quantity 
  FROM Animal 
  GROUP BY Sex;




------------------------------------------------------------------------------
-- Script to create order table

CREATE TABLE [Order] (
  OrderId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  [ClientName] VARCHAR(50) NOT NULL,
  Total DECIMAL(10, 2) NOT NULL,
  PurchaseDate DATE NOT NULL,
  DiscountsApplied VARCHAR(50) NOT NULL,
  freight DECIMAL(10, 2) NOT NULL,
  [Paid] BIT  NOT NULL
);


------------------------------------------------------------------------------
-- Script to create order animal table to save relations many to many
CREATE TABLE OrdenAnimal (
    OrdenId INT NOT NULL,
    AnimalId INT NOT NULL,
    FOREIGN KEY (OrdenId) REFERENCES [Order] (OrderId),
    FOREIGN KEY (AnimalId) REFERENCES Animal (AnimalId)
);