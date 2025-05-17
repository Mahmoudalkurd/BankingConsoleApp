-- Stored Procedure: GetAccountsByCustomer
CREATE PROCEDURE GetAccountsByCustomer
  @kunder_id INT
AS
BEGIN
  SELECT * FROM Konton WHERE kunder_id = @kunder_id;
END;

-- Stored Procedure: AddTransaction
CREATE PROCEDURE AddTransaction
  @from_konto INT,
  @to_konto INT,
  @amount DECIMAL(18,2)
AS
BEGIN
  INSERT INTO Transaktioner (from_konton_id, to_konton_id, amount)
  VALUES (@from_konto, @to_konto, @amount);

  UPDATE Konton SET saldo = saldo - @amount WHERE konton_id = @from_konto;
  UPDATE Konton SET saldo = saldo + @amount WHERE konton_id = @to_konto;
END;
