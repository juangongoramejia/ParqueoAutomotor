CREATE DATABASE PruebaVehiculosMotos

USE PruebaVehiculosMotos

CREATE TABLE Vehiculos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Placa VARCHAR(20) NOT NULL,
    TipoVehiculo VARCHAR(20) NOT NULL, 
    EsHibridoOElctrico INT NOT NULL, 
    HoraIngreso DATETIME NOT NULL,
    HoraSalida DATETIME NULL,
    PlazaAsignada INT NOT NULL,
);


CREATE TABLE Plazas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TipoPlaza VARCHAR(20) NOT NULL 
);

CREATE TABLE Transacciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VehiculoId INT,
    Monto INT,
    FechaTransaccion DATETIME,
);

INSERT INTO Vehiculos (Placa, TipoVehiculo, EsHibridoOElctrico, HoraIngreso, HoraSalida, PlazaAsignada)
VALUES 
('ABC123', 'Vehiculo Ligero', 1, '2025-04-01 08:00:00', '2025-04-01 18:00:00', 5),
('XYZ456', 'Moto', 0, '2025-04-01 09:00:00', NULL, 12),
('LMN789', 'Vehiculo Ligero', 0, '2025-04-01 10:00:00', '2025-04-01 15:30:00', 3);

INSERT INTO Transacciones (VehiculoId, Monto, FechaTransaccion)
VALUES 
(1, 500, '2025-04-01 08:00:00'),  
(2, 200, '2025-04-01 09:00:00'), 
(3, 400, '2025-04-01 10:00:00');