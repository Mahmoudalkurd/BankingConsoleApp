-- Read-only user
CREATE LOGIN readonly_user WITH PASSWORD = 'StrongPass1!';
CREATE USER readonly_user FOR LOGIN readonly_user;
GRANT SELECT ON SCHEMA::dbo TO readonly_user;

-- Owner user
CREATE LOGIN owner_user WITH PASSWORD = 'StrongerPass2!';
CREATE USER owner_user FOR LOGIN owner_user;
ALTER ROLE db_owner ADD MEMBER owner_user;

-- Views
CREATE VIEW TopBalances AS
SELECT TOP 3 * FROM Konton ORDER BY saldo DESC;

CREATE VIEW RecentTransactions AS
SELECT * FROM Transaktioner WHERE date >= GETDATE() - 30;

-- Encryption
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'MasterKey!';
CREATE CERTIFICATE MyCert WITH SUBJECT = 'Bank Cert';
CREATE SYMMETRIC KEY MyKey WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE MyCert;
