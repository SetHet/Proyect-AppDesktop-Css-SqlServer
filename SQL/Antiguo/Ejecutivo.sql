Create table Ejecutivo(
	id_ejecutivo INT IDENTITY(1,1) PRIMARY KEY,
	usuario NVARCHAR(30) NOT NULL,
	codigo NVARCHAR(30) NOT NULL
);

INSERT INTO Ejecutivo VALUES ('admin', '123');

SELECT * FROM Ejecutivo;