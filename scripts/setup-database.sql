
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'PropertiesDB')
BEGIN
    CREATE DATABASE PropertiesDB;
END

GO

Use PropertiesDB; 
GO

CREATE TABLE Owner (
    OwnerId     UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    [Name]      VARCHAR(100) NOT NULL,
    [Address]   VARCHAR(250) NOT NULL,
    Photo       VARBINARY(MAX) NULL,
    Birthday    DATE NOT NULL,
    Active      BIT DEFAULT 1 NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NOT NULL
);
 
GO 
CREATE TABLE Property(
    PropertyId      UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    [Name]          VARCHAR(100) NOT NULL,
    OwnerId         UNIQUEIDENTIFIER  FOREIGN KEY (OwnerId) REFERENCES [OWNER] (OwnerId) ,
    [Address]       VARCHAR(250),
    Price           DECIMAL(18,2) CHECK (Price>0),
    InternalCode    VARCHAR(50) UNIQUE,
    [Year]          INT,
    Sold            BIT DEFAULT 0,
    LastUpdated     DATETIME,
    CreatedDate     DATETIME DEFAULT GETDATE()
)

GO

CREATE TABLE PropertyImage(
    IdPropertyImage UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    PropertyId      UNIQUEIDENTIFIER  FOREIGN KEY (PropertyId) REFERENCES Property (PropertyId) ,
    [File]          VARBINARY(MAX),
    [Enabled]       BIT DEFAULT 1,
    CreatedDate     DATETIME DEFAULT GETDATE()
)

GO
 
CREATE TABLE PropertyTrace(
    IdPropertyTrace UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    PropertyId      UNIQUEIDENTIFIER  FOREIGN KEY (PropertyId) REFERENCES Property (PropertyId) ,
    DateSale        DATE,
    TransactionType VARCHAR(100),
    [Value]         DECIMAL(18,2),
    Tax             DECIMAL(18,2),
    CreatedDate     DATETIME DEFAULT GETDATE()
)
GO

Insert into [Owner] (OwnerId, [name], [Address],Photo, Birthday, Active, CreatedDate) VALUES
 ('2b694f24-2e3f-4245-afd6-b3613924ddb4' ,'Elon Musk', '1 Rocket Road Hawthorne CA 0000000', null, '06-28-1971', 1, GETDATE() )
